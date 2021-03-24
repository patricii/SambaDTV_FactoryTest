using ModFactoryTestCore.Domain.Test;
using ModFactoryTestCore.Domain.Tool;
using System;
using System.Collections.Generic;
using System.Threading;

using System.Diagnostics;

namespace ModFactoryTestCore
{
    public class TestCoreRunner 
    {
        private Thread threadRunTests = null;
        private TestCoreController tcc;
        private TestCoreSuite ts;
        private TestCoreMessages.StationType stationType;
        private string trackId;
        private string modTrackId;

        private Dictionary<string, string> dicSambaModInfo = null;
        private static readonly string TEST_AMPS = "test_amps_";
        private static readonly string AMPS_HISTORY = "test_amps_history";

        private List<TestCaseBase> testCases;


        public TestCoreRunner(TestCoreController testCoreController, string trackId, TestCoreMessages.StationType stationType)
        {
            this.tcc = testCoreController;
            this.trackId = trackId;
            this.stationType = stationType;
            this.threadRunTests = new Thread(()=> run(testCases, this));
            this.modTrackId = string.Empty;
            
            dicSambaModInfo = new Dictionary<string, string>();

            ts = new TestCoreSuite(stationType, tcc);

            testCases = ts.GetTestCasesList(stationType);
        }
                
        private static void isClosedJig(TestCoreRunner runner)
        {
            if (!runner.tcc.Jig.IsJigClosed())
            {
                runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, TestCoreMessages.ParseMessages(TestCoreMessages.JIG_NOT_CLOSED));

                while (!runner.tcc.Jig.IsJigClosed())
                {
                    Thread.Sleep(1000);
                    runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ". ");
                }

                runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, TestCoreMessages.ParseMessages(TestCoreMessages.JIG_CLOSED));
            }
        }
              
        internal void RunTestsToStation()
        {
            tcc.MQS.SetTrackId(trackId);
            dicSambaModInfo.Clear();
            threadRunTests.Start();
        }
        
        private int PrintModInfo(string line)
        {
            try
            {
                if (line == null || line.Equals(""))
                    return 0;
                               
                if (line.Contains(TEST_AMPS))
                {
                    if (line.Contains(AMPS_HISTORY))
                    {
                        tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, line);
                        string[] ts = line.Split(' ');
                        dicSambaModInfo.Add(ts[1], ts[0]);
                        return 0;
                    }

                    tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, line);
                    string[] testResult = line.Split(' ');
                    dicSambaModInfo.Add(testResult[1], testResult[0]);

                    return 0;
                }

                tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, line);
                
                if(line.Contains(":"))
                {
                    string[] substring = line.Split(':');
                    dicSambaModInfo.Add(substring[0], substring[1]);

                    if (line.Contains("UID"))
                    {
                        string[] uid = line.Split('-');
                        modTrackId = uid[3];
                    }
                }
            }
            catch (Exception ex)
            {
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, line + "\n\t" + ex.Message);
                return -1 ;
            }

            return 0;
        }



        /// <summary>
        /// Execute the method that was passed by parameter on other thread with set timeout.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static T Execute<T>(Func<T> func, int timeout)
        {
            T result;
            TryExecute(func, timeout, out result);
            return result;
        }
        /// <summary>
        /// Try execute the method that was passed by parameter on other thread with set timeout and return on result the return of method passed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="timeout"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryExecute<T>(Func<T> func, int timeout, out T result)
        {
            var t = default(T);
            var thread = new Thread(() => t = func());
            thread.Start();
            var completed = thread.Join(timeout);
            if (!completed) thread.Abort();
            result = t;
            return completed;
        }
        


        private static void run(List<TestCaseBase> testCases, TestCoreRunner runner)
        {
            bool hasFailTests = false;
            runner.tcc.Andon.SetState(Domain.Andon.State.OFF);

            try
            {
                //AdbManager.installSambaAppIfNeeded(runner.PrintModInfo);
                //runner.tcc.Mod.GetModInfo(runner.PrintModInfo);

                //Check if have match ModTrackId with scanned trackId.
                if (!runner.modTrackId.Equals(runner.trackId))
                {
                    //runner.tcc.runTest = false;
                    //throw new Exception("No match ModTrackId with scanned trackId.");
                }
            }
            catch (Exception ex)
            {
                runner.tcc.Andon.SetState(Domain.Andon.State.FAIL);
                runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ex.Message);    
                hasFailTests = true;
                runner.tcc.SetLogNameTo(hasFailTests ? "FAIL" : "PASS");

                TestCaseBase.SaveTestResultListToFile(runner.tcc.newLogFileLocation);

                return;
            }

            TestCaseBase.TestResultList.Clear();

            int totalTestCases = (testCases.Count) * 3; // *3 because we have Prepare, Execute and Evaluate Results
            double percentByTest = 100 / totalTestCases; 
            double currentPercent = 0;
            
            foreach (TestCaseBase item in testCases)
            {
                isClosedJig(runner);

                if(! runner.tcc.runTest)
                {
                    runner.tcc.Andon.SetState(Domain.Andon.State.FAIL);
                    hasFailTests = true;
                    runner.tcc.SetLogNameTo("CANCELED");
                    TestCaseBase.SaveTestResultListToFile(runner.tcc.newLogFileLocation);
                    return;
                }

                runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, "==================================================\n");

                int result = -1;

                //Preparing...
                currentPercent = currentPercent + percentByTest;
                runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.UPDATE_TEST_PERCENTUAL, (string.Format("{0:0}", (currentPercent))));
                if (!TryExecute(item.Prepare, item.Timeout, out result))
                    runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, TestCoreMessages.ParseMessages(TestCoreMessages.REACHED_TIMEOUT) + " => " + item.Timeout + " ms.");
                if (result != TestCoreMessages.SUCCESS)
                {
                    hasFailTests = true;
                    continue;
                }
                
                //Executing...
                currentPercent = currentPercent + percentByTest;
                runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.UPDATE_TEST_PERCENTUAL, (string.Format("{0:0}",(currentPercent))));
                if (!TryExecute(item.Execute, item.Timeout, out result))
                    runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, TestCoreMessages.ParseMessages(TestCoreMessages.REACHED_TIMEOUT) + " => " + item.Timeout + " ms.");
                if (result != TestCoreMessages.SUCCESS)
                {
                    hasFailTests = true;
                    continue;
                }
                
                //Evaluating...
                currentPercent = currentPercent + percentByTest;
                runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.UPDATE_TEST_PERCENTUAL, (string.Format("{0:0}", (currentPercent))));
                if (!TryExecute(item.EvaluateResults, item.Timeout, out result))
                    runner.tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, TestCoreMessages.ParseMessages(TestCoreMessages.REACHED_TIMEOUT) + " => " + item.Timeout + " ms.");
                if (result != TestCoreMessages.SUCCESS)
                {
                    hasFailTests = true;
                    continue;
                }
                
                //Set andon state
                if (item.ResulTest == TestCaseBase.TestEvaluateResult.PASS)
                {
                    runner.tcc.Andon.SetState(Domain.Andon.State.PASS);
                }
                else 
                {
                    runner.tcc.Andon.SetState(Domain.Andon.State.FAIL);
                    hasFailTests = true;
                }
            }

            //Change log name and save it.
            runner.tcc.SetLogNameTo(hasFailTests ? "FAIL" : "PASS");
            TestCaseBase.SaveTestResultListToFile(runner.tcc.newLogFileLocation);

            //Execute LogResult
            if(Boolean.Parse(runner.tcc.GetValueConfiguration("SETTINGS", "LOG_RESULT_ENABLE").ToLower()))
                runner.tcc.MQS.LogResult(hasFailTests ? "FAIL" : "PASS");
        }
        
    }
}

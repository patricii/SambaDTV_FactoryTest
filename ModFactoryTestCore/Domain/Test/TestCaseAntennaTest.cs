using ModFactoryTestCore.Domain.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseAntennaTest : TestCaseBase
    {
        TestCoreController tcc = null;
        private AdbManager.CommandResult result = null;
        private AdbManager adb = null;
        private string errorMessage = string.Empty;
        private double measures = 0;
        
        private bool isMQSEnable = false;
        private int frequency = 0;
        private double hightLimit = 0;
        private double lowLimit = 0;
        private string units = string.Empty;
        private int recycle = 0;

        public TestCaseAntennaTest(TestCoreController tcc)
        {
            base.Code = "4C";
            base.Name = rm.GetString("tcAntennaTestName");
            base.Description = rm.GetString("tctcAntennaTestDescription");
            base.ExpectedResults = rm.GetString("tcAntennaTestExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_ANTENNA_TEST", "TIMEOUT"));
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.adb = new AdbManager();
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_ANTENNA_TEST", "MQS_ENABLE").ToLower());
            this.frequency = Int32.Parse(tcc.GetValueConfiguration("TC_ANTENNA_TEST", "FREQUENCY"));
            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_ANTENNA_TEST", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_ANTENNA_TEST", "LOW_LIMIT"));
            this.units = tcc.GetValueConfiguration("TC_ANTENNA_TEST", "UNIT");
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_ANTENNA_TEST", "RECYCLE"));
        }


        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcAntennaTestPreparing"));
            
            try
            {
                //TODO: Injetar a frequencia no celular pelo DTV Generator...

                result = AdbManager.DTV.Tune(frequency, tcc.ManageUsb);
                if (result.Result != AdbManager.CommandResult.OK)
                {
                    base.ResulTest = TestEvaluateResult.BLOCKED;
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcTunerVerificationFailTuneFrequency"));
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + result.Comments);
                    return TestCoreMessages.ERROR;
                }

                if (result.Comments.Equals("LOCK=NO"))
                {
                    base.ResulTest = TestEvaluateResult.BLOCKED;
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcTunerVerificationFrequencyHasNoSignal"));
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + result.Comments);
                    return TestCoreMessages.ERROR;
                }
            }
            catch (Exception ex)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcTunerVerificationFailPreparing"));
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + ex.Message);
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;
        }

        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcAntennaTestExecuting"));
            
            try
            {
                result = AdbManager.DTV.GetRssi(frequency, tcc.ManageUsb);

                if (result.Result != AdbManager.CommandResult.OK)
                {
                    base.ResulTest = TestEvaluateResult.BLOCKED;
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcTunerVerificationFailGetRssi"));
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + result.Comments);
                    return TestCoreMessages.ERROR;
                }

                string[] tempResult;

                tempResult = result.Comments.Split('=');

                measures += Double.Parse(tempResult[1]);

                if(recycle > 0)
                    measures = measures / recycle;
            }
            catch (Exception ex)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcTunerVerificationFailExecuting"));
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + ex.Message);
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;
        }

        private int updateLogs()
        {
            int retCode = TestCoreMessages.SUCCESS;

            //Add log to MQS
            if (isMQSEnable)
            {
                retCode = tcc.MQS.AddLogResult(
                    this.Code,
                    this.Description,
                    measures.ToString(),
                    hightLimit.ToString(),
                    lowLimit.ToString(),
                    hightLimit.ToString(),
                    lowLimit.ToString(),
                    (int)base.ResulTest,
                    units,
                    errorMessage);

                if (retCode != TestCoreMessages.SUCCESS)
                    return retCode;
            }

            //Send result to UI
            StringBuilder stb = new StringBuilder();
            stb.AppendLine(this.Code + " " + base.Description);
            stb.AppendLine("\tValue: " + measures.ToString());
            stb.AppendLine("\tHightLimit: " + hightLimit.ToString());
            stb.AppendLine("\tLowLimit: " + lowLimit.ToString());
            stb.AppendLine("\tY_HightLimit: " + hightLimit.ToString());
            stb.AppendLine("\tY_LowLimit: " + lowLimit.ToString());
            stb.AppendLine("\tResult: " + base.ResulTest.ToString());
            stb.AppendLine("\tUnits: " + units);
            stb.AppendLine("\tErrorMessage: " + errorMessage);
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, stb.ToString());

            //Add TestResult to list
            ModFactoryTestCore.Domain.TestResult tr = new ModFactoryTestCore.Domain.TestResult(
              tcc.TrackId,
              tcc.logFileLocation,
              this.Code,
              this.Description,
              result.Comments,
              hightLimit.ToString(),
              lowLimit.ToString(),
              hightLimit.ToString(),
              lowLimit.ToString(),
              base.ResulTest.ToString(),
              units,
              errorMessage);

            TestCaseBase.TestResultList.Add(tr);

            return retCode;
        }

        public override int EvaluateResults()
        {
            int retCode;
            int myRecycle = 0;

            //Recycles
            while (((measures < lowLimit) || (measures > hightLimit)) && (myRecycle < recycle))
            {
                base.ResulTest = TestEvaluateResult.FAIL;
                updateLogs();
                tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("uiRunningRecycle") + (myRecycle + 1) + @"/" + recycle);
                measures = 0;
                Execute();
                myRecycle++;

                //Todo: only for test
                if (myRecycle == 0)
                {
                    measures = -80;
                }
            }

            //Check measures
            if ((measures < lowLimit) || (measures > hightLimit))
            {
                base.ResulTest = TestEvaluateResult.FAIL;
                updateLogs();
                retCode = TestCoreMessages.ERROR;
            }
            else
            {
                base.ResulTest = TestEvaluateResult.PASS;
                updateLogs();
                retCode = TestCoreMessages.SUCCESS;
            }

            //Send log to MQS
            int ret = tcc.MQS.LogResult(base.ResulTest.ToString());
            if (ret != TestCoreMessages.SUCCESS)
                return ret;

            base.TimeStamp = DateTime.Now;

            return retCode;
        }
    }
}

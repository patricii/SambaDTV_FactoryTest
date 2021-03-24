using ModFactoryTestCore.Domain.Tool;
using System;
using System.Text;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseTunerVerification : TestCaseBase
    {
        TestCoreController tcc = null;
        private string errorMessage = string.Empty;
        private AdbManager adb = null;
        private AdbManager.CommandResult result = null;
        private int frequency = 0;
        private double hightLimit = 0;
        private double lowLimit = 0;
        private double measures = 0;
        private string units = string.Empty;
        private int recycle = 0;
        private bool isMQSEnable = false;

        public TestCaseTunerVerification(TestCoreController tcc)
        {
            base.Code = "5B";
            base.Name = rm.GetString("tcTunerVerificationName");
            base.Description = rm.GetString("tcTunerVerificationDescription");
            base.ExpectedResults = rm.GetString("tcTunerVerificationExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.TestPoints.Add("TP030");//RF
            base.TestPoints.Add("TP038");//GND
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_TUNE_VERIFICATION", "TIMEOUT"));
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.adb = new AdbManager();

            this.frequency = Int32.Parse(tcc.GetValueConfiguration("TC_TUNE_VERIFICATION", "FREQUENCY"));
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_TUNE_VERIFICATION", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_TUNE_VERIFICATION", "UNIT");
            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_TUNE_VERIFICATION", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_TUNE_VERIFICATION", "LOW_LIMIT"));
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_TUNE_VERIFICATION", "MQS_ENABLE").ToLower());
        }


        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcTunerVerificationPreparing"));

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
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcTunerVerificationExecuting"));

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
                    null,
                    null,
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

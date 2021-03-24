using ModFactoryTestCore.Domain.Tool;
using System;
using System.Text;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseLedVerification : TestCaseBase
    {
        TestCoreController tcc = null;
        private double hightLimit = 0;
        private double lowLimit = 0;
        private double idleCurrent = 0;
        private double ledsCurrent = 0;
        private double measures = 0;
        private string errorMessage = string.Empty;
        private AdbManager adb = null;
        private AdbManager.LED adbManagerLed = null;
        private AdbManager.CommandResult result = null;
        private string units = string.Empty;
        private int recycle = 0;
        private bool isMQSEnable = false;
        
        public TestCaseLedVerification(TestCoreController tcc)
        {
            base.Code = "3B";
            base.Name = rm.GetString("tcLedVerificationName");
            base.Description = rm.GetString("tcLedVerificationDescription");
            base.ExpectedResults = rm.GetString("tcLedVerificationExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_LED_VERIFICATION", "TIMEOUT"));
            base.TestPoints.Add("TP034");
            base.TestPoints.Add("TP035");
            base.TestPoints.Add("TP036");
            base.TestPoints.Add("TP037");
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.adb = new AdbManager();
            this.adbManagerLed = new AdbManager.LED();
            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_LED_VERIFICATION", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_LED_VERIFICATION", "LOW_LIMIT"));
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_LED_VERIFICATION", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_LED_VERIFICATION", "UNIT");
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_LED_VERIFICATION", "MQS_ENABLE").ToLower());
        }

        public override int Prepare()
        {
            try
            {
                tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcLedVerificationPreparing"));

                //Turn PSU2 ON
                if(tcc.PwrSupply.SetCharger5V() != TestCoreMessages.SUCCESS)
                {
                    base.ResulTest = TestEvaluateResult.BLOCKED;
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcLedVerificationFailSetCharger"));
                    return TestCoreMessages.ERROR;
                }

                //Turn off all SNAP Leds
                result = adbManagerLed.TurnAllOff();
                if (result.Result != AdbManager.CommandResult.OK)
                {
                    base.ResulTest = TestEvaluateResult.BLOCKED;
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcLedVerificationFailTurnOffAllLeds"));
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + result.Comments);
                    return TestCoreMessages.ERROR;
                }
            }
            catch (Exception ex)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcLedVerificationFailTurnOffAllLeds"));
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + ex.Message);
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;         
        }

        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcLedVerificationExecuting"));

            //Get Idle current
            for (int i = 0; i <= recycle; i++)
            {                
                idleCurrent += tcc.PwrSupply.ReadChargerCurrent();
            }

            idleCurrent = idleCurrent / recycle;
            

            //Turn on all leds
            result = adbManagerLed.TurnAllOn();            
            if (result == null || result.Result == AdbManager.CommandResult.FAIL)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcLedVerificationFailTurnOnAllLeds"));
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + result.Comments);
                return TestCoreMessages.ERROR;
            }

            //Get LEDS current
            for (int i = 0; i <= recycle; i++)
            { 
                ledsCurrent += tcc.PwrSupply.ReadChargerCurrent();
            }

            if(recycle > 0)
                ledsCurrent = ledsCurrent / recycle;

            //Turn off all SNAP Leds
            result = adbManagerLed.TurnAllOff();
            if (result.Result != AdbManager.CommandResult.OK)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcLedVerificationFailTurnOffAllLeds"));
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

            //Notify UI
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
              measures.ToString(),
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
                if (myRecycle == 2)
                {
                    measures = 2;
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

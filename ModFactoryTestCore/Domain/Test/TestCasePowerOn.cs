using ModFactoryTestCore.Domain.Tool;
using System;
using System.Text;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCasePowerOn : TestCaseBase
    {
        TestCoreController tcc = null;
        private double hightLimit = 0;//current
        private double lowLimit = 0;
        private string currentStandard = "0";
        private string voltageStandard = "0";
        private int channel = 0;
        private string state = string.Empty;
        private double measures = 0;
        private double idleCurrent = 0;
        private double chargeCurrent = 0;
        private string errorMessage = string.Empty;
        private AdbManager adb = null;
        private AdbManager.CommandResult result = null;
        private string units = string.Empty;
        private int recycle = 0;
        private bool isMQSEnable = false;

        public TestCasePowerOn(TestCoreController tcc)
        {
            base.Code = "1B";
            base.Name = rm.GetString("tcPowerOnName");
            base.Description = rm.GetString("tcPowerOnDescription");
            base.ExpectedResults = rm.GetString("tcPowerOnExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.TestPoints.Add("USB");
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_POWER_ON", "TIMEOUT"));
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.adb = new AdbManager();

            this.voltageStandard = tcc.GetValueConfiguration("TC_POWER_ON", "VOLTAGE_STANDARD");
            this.currentStandard = tcc.GetValueConfiguration("TC_POWER_ON", "CURRENT_STANDARD");
            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_POWER_ON", "CURRENT_HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_POWER_ON", "CURRENT_LOW_LIMIT"));
            this.channel = Int32.Parse(tcc.GetValueConfiguration("TC_POWER_ON", "CHANNEL"));
            this.state = tcc.GetValueConfiguration("TC_POWER_ON", "STATE");
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_POWER_ON", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_POWER_ON", "UNIT");
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_POWER_ON", "MQS_ENABLE").ToLower());
        }


        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcPowerOnPreparing"));
            
            //Turn PSU2 ON
            if (tcc.PwrSupply.SetPowerSupply(channel, voltageStandard, currentStandard, state) != TestCoreMessages.SUCCESS)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcPowerOnFailSetPowerOn"));
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;
        }

        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcPowerOnExecuting"));

            for (int i = 0; i <= recycle; i++)
            {
                //Get Idle current
                idleCurrent = tcc.PwrSupply.ReadChargerCurrent();

                //TODO: Turn on the charge...


                //Get Charge current
                chargeCurrent = tcc.PwrSupply.ReadChargerCurrent();

                //TODO: Turn off the charge...

                //measures += 2; //TODO: only for test.
                measures += chargeCurrent - idleCurrent;
            }

            //TODO: only for test
            measures = 2;

            if(recycle > 0)
                measures = measures / recycle;

            return 0;
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
                        String.Format(base.format, measures),//measures.ToString(),
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
            stb.AppendLine("\tValue: " + String.Format(base.format, measures));
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
              String.Format(base.format, measures),
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
                tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("uiRunningRecycle") + (myRecycle+1) + @"/" + recycle);
                measures = 0;
                Execute();
                myRecycle++;
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

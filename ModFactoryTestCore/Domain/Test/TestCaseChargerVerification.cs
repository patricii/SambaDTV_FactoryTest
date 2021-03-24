using System;
using System.Text;
using System.Threading;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseChargerVerification : TestCaseBase
    {
        TestCoreController tcc = null;
        private double hightLimit = 0;
        private double lowLimit =   0;
        private double measures =   0;
        private string errorMessage = string.Empty;
        private string units = string.Empty;
        private int recycle = 0;
        private bool isMQSEnable = false;
        
        public TestCaseChargerVerification(TestCoreController tcc)
        {
            base.Code = "2B";
            base.Name = rm.GetString("tcChargerVerificationName");
            base.Description = rm.GetString("tcChargerVerificationDescription");
            base.ExpectedResults = rm.GetString("tcChargerVerificationExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_CHARGER_VERIFICATION", "TIMEOUT"));            
            base.TestPoints.Add("TP004");   // BATT+
            base.TestPoints.Add("TP005");   // BATT-
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_CHARGER_VERIFICATION", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_CHARGER_VERIFICATION", "LOW_LIMIT"));
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_CHARGER_VERIFICATION", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_CHARGER_VERIFICATION", "UNIT");
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_CHARGER_VERIFICATION", "MQS_ENABLE").ToLower());
        }
        
        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcChargeVerificationPreparing"));

            //Set all TestPoint Open
            if( tcc.Mod.SetAllTestPointOpen() != TestCoreMessages.SUCCESS)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcChargeVerificationFailAllTestPointOpen"));
                return TestCoreMessages.ERROR;
            }
            
            //Turn PSU2 ON
            if (tcc.PwrSupply.SetCharger5V() != TestCoreMessages.SUCCESS)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcChargeVerificationFailSetCharger5V"));
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;
        }
        
        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcChargeVerificationExecuting"));

            //Close the testpoint
            if(tcc.Mod.SetTestPointState(Mod.TestPoint.CHARGER, Mod.TestPointState.CLOSED) != TestCoreMessages.SUCCESS)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcChargeVerificationFailCloseTestPointOpen"));
                return TestCoreMessages.ERROR;
            }

            //Get testpoint measure
            for (int i = 0; i <= recycle; i++)
            {
                measures += tcc.Mod.GetTestPointVoltage(Mod.TestPoint.CHARGER);
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
                int ret = tcc.MQS.AddLogResult(
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
                tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("uiRunningRecycle") + (myRecycle + 1) + @"/" + recycle);
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

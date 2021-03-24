using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseBatteryTest : TestCaseBase
    {
        TestCoreController tcc = null;
        private double hightLimit = 0;
        private double lowLimit = 0;
        private double measures = 0;
        private string errorMessage = string.Empty;
        private string units = string.Empty;
        private int recycle = 0;
        private bool isMQSEnable = false;

        public TestCaseBatteryTest(TestCoreController tcc)
        {
            base.Code = "3C";
            base.Name = rm.GetString("tcBatteryVerificationName");
            base.Description = rm.GetString("tcBatteryVerificationDescription");
            base.ExpectedResults = rm.GetString("tcBatteryVerificationExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_BATTERY_TEST", "TIMEOUT"));
            base.TestPoints.Add("TP004");   // BATT+
            base.TestPoints.Add("TP005");   // BATT-
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_BATTERY_TEST", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_BATTERY_TEST", "LOW_LIMIT"));
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_BATTERY_TEST", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_BATTERY_TEST", "UNIT");
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_BATTERY_TEST", "MQS_ENABLE").ToLower());
        }


        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcBatteryTestPreparing"));
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, "TEST NOT IMPLEMENTED !");
            return 0;//throw new NotImplementedException();
        }

        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcBatteryTestExecuting"));
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, "TEST NOT IMPLEMENTED !");
            return 0;
            //throw new NotImplementedException();
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
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, "TEST NOT IMPLEMENTED !");
            base.ResulTest = TestEvaluateResult.FAIL;
            updateLogs();
            return 0;
        }
    }
}

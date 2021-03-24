using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseMagneticTest : TestCaseBase
    {
        TestCoreController tcc = null;
        private string currentStandard = "0";
        private string voltageStandard = "0";
        private int channel = 0;
        private string state = string.Empty;
        private string errorMessage = string.Empty;
        private double hightLimit = 0;
        private double lowLimit = 0;
        private double measures = 0;
        private string units = string.Empty;
        private int recycle = 0;
        private bool isMQSEnable = false;

        public TestCaseMagneticTest(TestCoreController tcc)
        {
            base.Code = "1C";
            base.Name = rm.GetString("tcMagneticName");
            base.Description = rm.GetString("tcMagneticDescription");
            base.ExpectedResults = rm.GetString("tcMagneticExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.TestPoints.Add("I/O Shield Box");
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "TIMEOUT"));
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;

            this.voltageStandard = tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "VOLTAGE_STANDARD");
            this.currentStandard = tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "CURRENT_STANDARD");
            this.channel = Int32.Parse(tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "CHANNEL"));
            this.state = tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "STATE");

            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "LOW_LIMIT"));
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "UNIT");
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_MAGNETIC_TEST", "MQS_ENABLE").ToLower());
        }

        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcMagneticPreparing"));

            //Set all TestPoint Open
            if (tcc.Mod.SetAllTestPointOpen() != TestCoreMessages.SUCCESS)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcMagneticFailAllTestPointOpen"));
                return TestCoreMessages.ERROR;
            }

            //Turn PSU2 ON
            if (tcc.PwrSupply.SetPowerSupply(channel, voltageStandard, currentStandard, state) != TestCoreMessages.SUCCESS)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcMagneticFailSetPowerOn"));
                return TestCoreMessages.ERROR;
            }
            
            return TestCoreMessages.SUCCESS;
        }

        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcMagneticExecuting"));

            //Close the testpoint
            if (tcc.Mod.SetTestPointState(Mod.TestPoint.REED_SWITCH, Mod.TestPointState.CLOSED) != TestCoreMessages.SUCCESS)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcMagneticFailCloseTestPoint"));
                return TestCoreMessages.ERROR;
            }

            //Get testpoint measure
            for (int i = 0; i <= recycle; i++)
            {
                measures += tcc.Mod.GetTestPointVoltage(Mod.TestPoint.REED_SWITCH);
            }

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

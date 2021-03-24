using ModFactoryTestCore.Domain.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseMobileInterfaceCommunicationStationC : TestCaseBase
    {
        TestCoreController tcc = null;
        private int frequency = 0;
        private string errorMessage = string.Empty;
        private AdbManager adb = null;
        private AdbManager.CommandResult result = null;
        private string units = string.Empty;
        private int recycle = 0;
        private double hightLimit = 0;
        private double lowLimit = 0;
        private double measures = 0;
        private bool isMQSEnable = false;

        public TestCaseMobileInterfaceCommunicationStationC(TestCoreController tcc)
        {
            base.Code = "2C";
            base.Name = rm.GetString("tcMobIntStationCComName");
            base.Description = rm.GetString("tcMobIntStationCDescription");
            base.ExpectedResults = rm.GetString("tcMobIntStationCExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.TestPoints.Add("M001");
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_MOBILE_INTERFACE_COMMUNICATION_STATION_C", "TIMEOUT"));
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.adb = new AdbManager();

            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_MOBILE_INTERFACE_COMMUNICATION_STATION_C", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_MOBILE_INTERFACE_COMMUNICATION_STATION_C", "UNIT");
            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_MOBILE_INTERFACE_COMMUNICATION_STATION_C", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_MOBILE_INTERFACE_COMMUNICATION_STATION_C", "LOW_LIMIT"));
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_MOBILE_INTERFACE_COMMUNICATION_STATION_C", "MQS_ENABLE").ToLower());
        }

        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcMobIntStationCPreparing"));

            try
            {
                this.frequency = Int32.Parse(tcc.GetValueConfiguration("TC_MOBILE_INTERFACE_COMMUNICATION", "FREQUENCY"));

                //TODO: Injetar a frequencia no celular pelo DTV Generator...
            }
            catch (Exception ex)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcMobIntComFailPrepare"));
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + ex.Message);
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;
        }

        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcMobIntStationCExecuting"));
            
            try
            {
                for (int i = 0; i <= recycle; i++)
                {
                    result = AdbManager.DTV.Tune(frequency, tcc.ManageUsb);
                    if (result.Result != AdbManager.CommandResult.OK)
                    {
                        base.ResulTest = TestEvaluateResult.BLOCKED;
                        tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcMobIntComFailTuneFrequency"));
                        tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + result.Comments);
                        return TestCoreMessages.ERROR;
                    }
                }
            }
            catch (Exception ex)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcMobIntComFailTuneFrequency"));
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + ex.Message);
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;
        }

        private int updateLogs()
        {
            int retCode = TestCoreMessages.SUCCESS;

            if (isMQSEnable)
            {
                //Add log to MQS
                retCode = tcc.MQS.AddLogResult(
                    this.Code,
                    this.Description,
                    result.Comments,
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
            stb.AppendLine("\tValue: " + result.Comments);
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

            while ((result.Result != AdbManager.CommandResult.OK) && (myRecycle < recycle))
            {
                base.ResulTest = TestEvaluateResult.FAIL;
                updateLogs();
                tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("uiRunningRecycle") + (myRecycle + 1) + @"/" + recycle);
                measures = 0;
                Execute();
                myRecycle++;
                retCode = TestCoreMessages.ERROR;

                //Todo: only for test
                if (myRecycle == 2)
                {
                    measures = 2;
                }
            }

            //Check mesures
            if (result.Result != AdbManager.CommandResult.OK)
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

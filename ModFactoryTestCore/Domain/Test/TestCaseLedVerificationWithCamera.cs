using ModFactoryTest.VisualInspection.Tool;
using ModFactoryTestCore.Domain.Tool;
using System;
using System.Text;
using System.Threading;

namespace ModFactoryTestCore.Domain.Test
{
    public class TestCaseLedVerificationWithCamera : TestCaseBase
    {
        TestCoreController tcc = null;
        private double hightLimit = 0;
        private double lowLimit = 0;
        private int screenX = 0;
        private int screenY = 0;
        private double measures = 0;
        private string errorMessage = string.Empty;
        private AdbManager adb = null;
        private AdbManager.LED adbManagerLed = null;
        private AdbManager.CommandResult adbResult;
        private string units = "--";
        private int recycle = 0;

        private AutomatedInspection.Result autInspResultLed025;
        private AutomatedInspection.Result autInspResultLed050;
        private AutomatedInspection.Result autInspResultLed075;
        private AutomatedInspection.Result autInspResultLed100;
        private bool isMQSEnable = false;

        public TestCaseLedVerificationWithCamera(TestCoreController tcc)
	    {
            base.Code = "5C";
            base.Name = rm.GetString("tcLedVerifyWithCamName");
            base.Description = rm.GetString("tcLedVerifyWithCamDescription");
            base.ExpectedResults = rm.GetString("tcLedVerifyWithCamExpectedResults");
            base.ResulTest = TestEvaluateResult.NOT_RUN;
            base.Timeout = Int32.Parse(tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "TIMEOUT"));
            base.TestPoints.Add("PC_USB");
            base.TimeStamp = DateTime.Now;

            this.tcc = tcc;
            this.adb = new AdbManager();
            this.adbManagerLed = new AdbManager.LED();

            this.screenX = Int32.Parse(tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "SCREEN_X"));
            this.screenY = Int32.Parse(tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "SCREEN_Y"));

            this.hightLimit = Double.Parse(tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "HIGHT_LIMIT"));
            this.lowLimit = Double.Parse(tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "LOW_LIMIT"));
            this.recycle = Int32.Parse(tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "RECYCLE"));
            this.units = tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "UNIT");
            this.isMQSEnable = Boolean.Parse(tcc.GetValueConfiguration("TC_LED_VERIFY_WITH_CAM", "MQS_ENABLE").ToLower());
	    }

        public override int Prepare()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcLedVerifyWithCamPreparing"));         

            try
            {
                tcc.NotifyUI(TestCoreMessages.TypeMessage.START_CAMERA, "Starting Camera...");

                //Turn off all SNAP Leds               
                adbResult = adbManagerLed.TurnAllOff();
                if ( adbResult.Result != AdbManager.CommandResult.OK)
                {
                    base.ResulTest = TestEvaluateResult.BLOCKED;
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcLedVerifyWithCamFailTurnOffAllLeds"));
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + adbResult.Comments);
                    return TestCoreMessages.ERROR;
                }
            }
            catch (Exception ex)
            {
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ex.Message);
                tcc.NotifyUI(TestCoreMessages.TypeMessage.STOP_CAMERA, ex.Message);
                return TestCoreMessages.ERROR;
            }

            return TestCoreMessages.SUCCESS;  
        }

        public override int Execute()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("tcLedVerifyWithCamExecuting"));      

            try
            {
                //Test leds
                tcc.NotifyUI(TestCoreMessages.TypeMessage.GET_FRAME_LED_OFF, "Turning all LEDs off...");
                if (captureLedFrame(AdbManager.LED.ID.LED25) == TestCoreMessages.SUCCESS)
                {
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.PROCESS_DETECTION, "Detecting LED 25%...");
                    AutomatedInspection.waitUntilProcessingDetection();
                    autInspResultLed025 = AutomatedInspection.getResult();
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.INFORMATION, autInspResultLed025.ToString());
                }

                tcc.NotifyUI(TestCoreMessages.TypeMessage.GET_FRAME_LED_OFF, "Turning all LEDs off...");
                if (captureLedFrame(AdbManager.LED.ID.LED50) == TestCoreMessages.SUCCESS)
                {
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.PROCESS_DETECTION, "Detecting LED 50%...");
                    AutomatedInspection.waitUntilProcessingDetection();
                    autInspResultLed050 = AutomatedInspection.getResult();
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.INFORMATION, autInspResultLed050.ToString());
                }

                tcc.NotifyUI(TestCoreMessages.TypeMessage.GET_FRAME_LED_OFF, "Turning all LEDs off..."); 
                if (captureLedFrame(AdbManager.LED.ID.LED75) == TestCoreMessages.SUCCESS)
                {
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.PROCESS_DETECTION, "Detecting LED 75%...");
                    AutomatedInspection.waitUntilProcessingDetection();
                    autInspResultLed075 = AutomatedInspection.getResult();
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.INFORMATION, autInspResultLed075.ToString());
                }

                tcc.NotifyUI(TestCoreMessages.TypeMessage.GET_FRAME_LED_OFF, "Turning all LEDs off..."); 
                if (captureLedFrame(AdbManager.LED.ID.LED100) == TestCoreMessages.SUCCESS)
                {
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.PROCESS_DETECTION, "Detecting LED 100%...");
                    AutomatedInspection.waitUntilProcessingDetection();
                    autInspResultLed100 = AutomatedInspection.getResult();
                    tcc.NotifyUI(TestCoreMessages.TypeMessage.INFORMATION, autInspResultLed100.ToString());
                }
            }
            catch (Exception ex)
            {
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, ex.Message);
                return TestCoreMessages.ERROR;
            }           

            adbResult = adbManagerLed.TurnAllOff();

            return TestCoreMessages.SUCCESS;
        }

        private int captureLedFrame(AdbManager.LED.ID led)
        {
            adbResult = adbManagerLed.TurnOn(led);
            if (adbResult == null || adbResult.Result == AdbManager.CommandResult.FAIL)
            {
                base.ResulTest = TestEvaluateResult.BLOCKED;
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, rm.GetString("tcLedVerificationFailTurnOnAllLeds"));
                tcc.NotifyUI(TestCoreMessages.TypeMessage.ERROR, "\t" + adbResult.Comments);
                return TestCoreMessages.ERROR;
            }

            tcc.NotifyUI(TestCoreMessages.TypeMessage.GET_FRAME_LED_ON, "Turning LED " + led + " on...");
            
            Thread.Sleep(1000);
            while (!AutomatedInspection.hasCapturedFrameLedTurnedOn())
            {
                Thread.Sleep(1000);
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
            while( ((autInspResultLed025.Detection != AutomatedInspection.Result.LED_DETECTED) ||
                (autInspResultLed050.Detection != AutomatedInspection.Result.LED_DETECTED) ||
                (autInspResultLed075.Detection != AutomatedInspection.Result.LED_DETECTED) ||
                (autInspResultLed100.Detection != AutomatedInspection.Result.LED_DETECTED)) &&
                (myRecycle < recycle) )
            {
                base.ResulTest = TestEvaluateResult.FAIL;
                updateLogs();
                tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, rm.GetString("uiRunningRecycle") + (myRecycle + 1) + @"/" + recycle);
                measures = 0;
                Execute();
                myRecycle++;
                retCode = TestCoreMessages.ERROR;
            }

            //Check measures
            if( (autInspResultLed025.Detection != AutomatedInspection.Result.LED_DETECTED) ||
                (autInspResultLed050.Detection != AutomatedInspection.Result.LED_DETECTED) ||
                (autInspResultLed075.Detection != AutomatedInspection.Result.LED_DETECTED) ||
                (autInspResultLed100.Detection != AutomatedInspection.Result.LED_DETECTED) )
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

            tcc.NotifyUI(TestCoreMessages.TypeMessage.STOP_CAMERA, "Stopping camera...");

            //Send log to MQS
            int ret = tcc.MQS.LogResult(base.ResulTest.ToString());
            if (ret != TestCoreMessages.SUCCESS)
                return ret;

            base.TimeStamp = DateTime.Now;            

            return retCode;
        }
    }
}


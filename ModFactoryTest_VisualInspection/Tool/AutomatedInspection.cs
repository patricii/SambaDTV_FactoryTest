using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ModFactoryTest.VisualInspection;

namespace ModFactoryTest.VisualInspection.Tool
{
    public class AutomatedInspection
    {
        #region Constants

        private const int WAIT_PROCESSING_MS = 100;

        #endregion

        #region Variables

        private static bool isCameraLoaded = false;
        private static bool isProcessingDetection = false;
        private static bool isFrameLedTurnedOnCaptured = false;
        private static bool isFrameLedTurnedOffCaptured = false;
        private static string cameraId;
        private static Result result;

        #endregion

        #region Members

        private static CameraGUI cameraGUI;

        #endregion

        #region Helper Classes

        public class Result
        {
            public const int LED_UNKOWN = -1;
            public const int LED_DETECTED = 1;
            public const int LED_NOT_DETECTED = 0;

            private int ledDetected = LED_UNKOWN;
            private int detectionArea = 0;

            public int Detection
            {
                get { return this.ledDetected; }
                set { this.ledDetected = value; }
            }

            public int DetectionArea
            {
                get { return this.detectionArea; }
                set { this.detectionArea = value; }
            }

            public string ToString()
            {
                if (this.ledDetected == 0)
                    return "LED_NOT_DETECTED";

                if (this.ledDetected == 1)
                    return "LED_DETECTED";

                return "LED_UNKOWN"; 
            }


        }

        public class InspectionException: Exception
        {
            public InspectionException(string message): base(message)
            {
            }
        }

        #endregion

        #region API Methods

        public static void startCamera(int screenX, int screenY, string cameraId)
        {
            if (cameraGUI == null)
                cameraGUI = new CameraGUI();

            cameraGUI.BringToFront();
            cameraGUI.Left = screenX;
            cameraGUI.Top = screenY;
            cameraGUI.CameraId = cameraId;
            cameraGUI.Show();

            isCameraLoaded = true;
        }

        public static void stopCamera()
        {
            if (cameraGUI == null)
                return;

            cameraGUI.Close();
            cameraGUI = null;

            isCameraLoaded = false;
        }

        public static bool captureFrameWithLedTurnedOff()
        {
            isFrameLedTurnedOffCaptured = false;

            if (!isCameraLoaded)
                throw new InspectionException("Camera Viewfinder has not been started.");

            try {
                cameraGUI.captureFrameLedOff();
            } catch (Exception e) {
                throw new AutomatedInspection.InspectionException(e.Message);
            }

            isFrameLedTurnedOffCaptured = true;

            return isFrameLedTurnedOffCaptured;
        }

        public static bool captureFrameWithLedTurnedOn()
        {
            isFrameLedTurnedOnCaptured = false;

            if (!isCameraLoaded)
                throw new AutomatedInspection.InspectionException("Camera Viewfinder has not been started.");

            try {
                cameraGUI.captureFrameLedOn();
            } catch (Exception e) { 
                throw new AutomatedInspection.InspectionException(e.Message);
            }

            isFrameLedTurnedOnCaptured = true;

            return isFrameLedTurnedOnCaptured;
        }

        public static Result processLedDetection()
        {
            if (!isCameraLoaded)
                throw new AutomatedInspection.InspectionException("Camera Viewfinder has not been started.");

            if (!isFrameLedTurnedOffCaptured)
                throw new AutomatedInspection.InspectionException("Frame with LED turned off has not been captured.");

            if (!isFrameLedTurnedOnCaptured)
                throw new AutomatedInspection.InspectionException("Frame with LED turned on has not been captured.");

            isProcessingDetection = true;

            cameraGUI.processLedDetection();

            isProcessingDetection = false;

            result = new Result();
            result.DetectionArea = cameraGUI.DetectionArea;
            result.Detection = cameraGUI.DetectionArea > 100 ? Result.LED_DETECTED : Result.LED_NOT_DETECTED;

            isFrameLedTurnedOffCaptured = false;
            isFrameLedTurnedOnCaptured = false;

            return result;
        }

        public static void waitUntilProcessingDetection()
        {
            while (isProcessingDetection)
                Thread.Sleep(500);
        }

        public static bool hasCapturedFrameLedTurnedOn()
        {
            return isFrameLedTurnedOnCaptured;
        }

        public static bool hasCapturedFrameLedTurnedOff()
        {
            return isFrameLedTurnedOffCaptured;
        }

        public static Result getResult()
        {
            return result;
        }

        #endregion

    }
}

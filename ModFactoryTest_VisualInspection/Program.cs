using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ModFactoryTest.VisualInspection.Tool;

namespace ModFactoryTest.VisualInspection
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CameraGUI());

            /*
            AutomatedInspection.startCamera(0, 0);

            AutomatedInspection.captureFrameWithLedTurnedOff();

            AutomatedInspection.captureFrameWithLedTurnedOn();

            AutomatedInspection.Result result = AutomatedInspection.processLedDetection();

            Console.WriteLine("Result: {0}", result.Detection == AutomatedInspection.Result.LED_DETECTED ? "DETECTED!" : "Not detected!");

            AutomatedInspection.stopCamera();
             * */
        }
    }
}

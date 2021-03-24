﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModFactoryTest.Tool
{
    public class AmpsManager
    {
        #region Constants

        private const int PROCESS_TIMEOUT = 120 * 1000;

        private const string WORK_DIR = @"C:\prod\amps";
        private const string PYTHON_EXEC = @"C:\python27\python.exe";
        private const string PYTHON_ID_SCRIPT  = @"C:\prod\amps\dtvmod_id.py";
        private const string PYTHON_ATTACH_SCRIPT = @"C:\prod\amps\dtvmod_attach.py";
        private const string PYTHON_BATTERY_SCRIPT = @"C:\prod\amps\dtvmod_battery.py";

        #endregion

        #region Variables

        private static bool result = false;
        private static Func<string, int> callback;

        #endregion

        #region Members

        private static string exception = null;

        #endregion

        #region Helper Classes

        public class AmpsManagerException: Exception
        {
            public AmpsManagerException(string message): base(message) { }
        }

        #endregion

        #region API Methods

        public static bool ExecutePythonScript(Func<string, int> callback, string script = PYTHON_ID_SCRIPT, string serialNumber = null)
        {
            if (callback == null) 
                throw new AmpsManagerException("Invalid Argument: Callback.");

            exception = null;

            AmpsManager.callback = callback;

            Directory.SetCurrentDirectory(WORK_DIR);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.FileName  = PYTHON_EXEC;
            startInfo.Arguments = script + 
                (serialNumber == null ? "" : " " + serialNumber);
            process.StartInfo = startInfo;
            process.Start();

            process.OutputDataReceived += (sender, args) => HandleStandardOutputData(args.Data);
            process.ErrorDataReceived += (sender, args) => HandleStandardErrorData(args.Data);

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            bool exited = process.WaitForExit(PROCESS_TIMEOUT);

            if (!exited)
                exception = PYTHON_EXEC + " process timeout.";

            if (exception != null)
                throw new AmpsManagerException(exception);

            return result;
        }

        protected static void HandleStandardOutputData(string stdout)
        {
            if (AmpsManager.callback != null)
                AmpsManager.callback.Invoke(stdout);
            result = true;
        }

        protected static void HandleStandardErrorData(string stderr)
        {
            if (stderr == null || "".Equals(stderr))
                return;

            result = false;

            // verify if errors have occurred, 
            // if so, raise the proper exception

            if (stderr.Contains("No such file or directory"))
                exception = "DTV Mod script not found.";
            if (stderr.Contains("No module named android"))
                exception = "aPython Mod Factory Test Framework not found.";
            if (stderr.Contains("The system cannot find the path specified") && stderr.Contains("usb_scripts"))
                exception = "aPython Mod Factory Test Framework not found (extras\\usb_scripts).";
            if (stderr.Contains("is adb installed"))
                exception = "ADB not found.";
            if (stderr.Contains("no devices/emulators found"))
                exception = "MotoZ PCBA is not connected.";
        }

        #endregion
    }
}

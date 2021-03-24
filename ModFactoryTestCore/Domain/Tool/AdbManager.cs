using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ModFactoryTestCore.Domain.Tool
{
    public class AdbManager
    {
        #region Constants

        private const string TAG_LOG = "DTV-MOD";

        private const string ARG_NAME  = "argName";
        private const string ARG_VALUE = "argValue";

        private const string ARG_ID = "id";
        private const string ARG_COMMAND = "command";

        private const string ADB_ARGUMENT_STRING_TEMPLATE = " -e \"" + ARG_NAME + "\" \"" + ARG_VALUE + "\"";

        private const string ADB_EXEC = "adb.exe";
        private const string ADB_COMMAND = @"C:\prod\Android\" + ADB_EXEC;
        private const string ADB_LOGCAT_COMMAND = "logcat samba.test:I *:S";
        private const string ADB_SHELL_PM_RAW_COMMAND = "shell pm grant samba.app com.motorola.mod.permission.RAW_PROTOCOL";
        private const string ADB_SHELL_AM_STOP_COMMAND = "shell am force-stop samba.app";
        private const string ADB_SHELL_AM_START_COMMAND = "shell am start -n samba.app/.Test";
        private const string ADB_SERIAL_PARAM = " -s ";
        private const string ADB_ROOT_COMMAND = "root";
        private const string ADB_REBOOT_COMMAND = "reboot";
        private const string ADB_REMOUNT_COMMAND = "remount";
        private const string ADB_DEVICES_COMMAND = "devices";
        private const string ADB_DISABLE_VERITY_COMMAND = "disable-verity";
        private const string ADB_PUSH_SAMBA_APP_LOCAL_COMMAND = @"push C:\prod\amps\Samba.apk /data/local/tmp/samba.app";
        private const string ADB_PUSH_SAMBA_APP_SYSTEM_COMMAND = @"push C:\prod\amps\Samba.apk /system/app/Samba.apk";
        private const string ADB_SHELL_CHMOD_SAMBA_APP_COMMAND = "shell chmod 644 /system/app/Samba.apk";
        private const string ADB_SHELL_LS_SAMBA_APP_COMMAND = "shell ls /system/app/Samba.apk";
        private const string ADB_SHELL_PM_INSTALL_SAMBA_APP_COMMAND = "shell pm install -r /data/local/tmp/samba.app";

        private const int COMMAND_TIMEOUT_TICK = 100;
        private const int COMMAND_TIMEOUT_COUNTER = 10 * 30; // 30 seconds
        private const int UNDER_TEST_TIMEOUT_COUNTER = 10 * 10; // 10 seconds
        private const int REBOOT_TIMEOUT_COUNTER = 60; // 1 minute
        private const int REBOOT_TIMEOUT_TICK = 1000;

        #endregion

        #region Variables

        private static Thread logCatWorker;
        private static System.Diagnostics.Process logCatProcess;

        private static int currentCommand = 0;
        private static string currentSerialNumber;

        private static bool runningTest = false;
        private static bool commandProcessed = false;
        private static bool isAdbLogCatRunning = false;
        private static bool isModPermissionSent = false;

        #endregion

        #region Members

        private static CommandResult commandResult = new CommandResult();

        private static readonly Random randomGenerator = new Random();

        private static Func<int, int> usbManagementCallback;

        private static Func<string, int> logCallbackFunction;

        #endregion

        #region Helper Classes

        #region Command
        public enum Command
        {
            LED_POWER, DTV_TUNE, DTV_GET_RSSI
        }

        public static class CommandExtensions
        {
            public static string ToCommandString(Command command)
            {
                switch (command)
                {
                    case Command.LED_POWER:
                        return "led_power";
                    case Command.DTV_TUNE:
                        return "dtv_tune";
                    case Command.DTV_GET_RSSI:
                        return "dtv_get_rssi";
                    default:
                        return "?";
                }
            }
        }
        #endregion

        #region Argument
        public class Argument
        {
            private string name, value;

            public Argument(string name, string value)
            {
                this.name = name;
                this.value = value;
            }

            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }

            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

        }
        #endregion

        #region Arguments
        public class Arguments
        {
            private List<Argument> args;

            public Arguments()
            {
                this.args = new List<Argument>();
            }

            public void addProperty(string name, string value)
            {
                this.args.Add(new Argument(name, value));
            }

            public List<Argument> getList()
            {
                return this.args;
            }
        }
        #endregion

        #region CommandResult

        public class CommandResult
        {
            public const string OK = "OK";
            public const string FAIL = "FAIL";

            private string id;
            private string result;
            private string comments;

            public string Id
            {
                get { return this.id; }
                set { this.id = value; }
            }

            public string Result
            {
                get { return this.result; }
                set { this.result = value; }
            }

            public string Comments
            {
                get { return this.comments; }
                set { this.comments = value; }
            }
        }

        #endregion

        #region AdbManagerException

        public class AdbManagerException: Exception
        {
            private string message;

            public AdbManagerException(string message)
            {
                this.message = message;
            }

            public override string Message
            {
                get { return this.message; }
            }
        }

        #endregion

        #region Logger

        public static class Logger
        {
            private static bool logEnabled = false;

            public static void LogEnabled()
            {
                logEnabled = true;
            }

            public static void LogDisabled()
            {
                logEnabled = false;
            }

            public static void Log(string message)
            {
                if (logEnabled)
                    Console.WriteLine(message);
            }
        }

        #endregion

        #endregion

        #region API Classes

        #region LED

        public class LED
        {
            public enum ID
            {
                LED25 = 25, LED50 = 50, LED75 = 75, LED100 = 100
            }

            protected CommandResult ExecutePowerCommand(string state)
            {
                AdbManager.Arguments adbArgs = new AdbManager.Arguments();
                adbArgs.addProperty("state", state);

                return RunCommand(Command.LED_POWER, adbArgs, null);
            }

            public CommandResult TurnAllOff()
            {
                return ExecutePowerCommand("1111");
            }

            public CommandResult TurnAllOn()
            {
                return ExecutePowerCommand("0000");
            }

            public CommandResult TurnOn(ID id)
            {
                switch (id)
                {
                    case ID.LED25 : return ExecutePowerCommand("0111");
                    case ID.LED50 : return ExecutePowerCommand("1011");
                    case ID.LED75 : return ExecutePowerCommand("1101");
                    case ID.LED100: return ExecutePowerCommand("1110");
                }
                return null;
            }

        }

        #endregion

        #region DTV

        public class DTV
        {
            protected static CommandResult ExecuteCommand(Command command, int frequency, Func<int, int> usbManagementFunction)
            {
                AdbManager.Arguments adbArgs = new AdbManager.Arguments();
                adbArgs.addProperty("frequency", frequency.ToString());
                adbArgs.addProperty("delay", "5000");

                return RunCommand(command, adbArgs, usbManagementFunction);
            }

            public static CommandResult Tune(int frequencyInKHz, Func<int, int> usbManagementFunction)
            {
                return ExecuteCommand(Command.DTV_TUNE, frequencyInKHz, usbManagementFunction);
            }

            public static CommandResult GetRssi(int frequencyInKHz, Func<int, int> usbManagementFunction)
            {
                return ExecuteCommand(Command.DTV_GET_RSSI, frequencyInKHz, usbManagementFunction);
            }
        }

        #endregion

        #endregion

        #region API Methods

        private static string GetSerialNumberString()
        {
            return currentSerialNumber == null || "".Equals(currentSerialNumber) ? "" : ADB_SERIAL_PARAM + currentSerialNumber;
        }

        public static void ConnectTo(string serialNumber)
        {
            currentSerialNumber = serialNumber;
        }

        public static void Cleanup()
        {
            if (false)
                foreach (var process in Process.GetProcessesByName(ADB_EXEC.Replace(".exe", "")))
                    process.Kill();
        }

        private static bool AdbIsConnected()
        {
            string stderr, stdout;

            RunAdbProcess(ADB_COMMAND, ADB_SHELL_LS_SAMBA_APP_COMMAND, out stdout, out stderr, false);

            if (stderr != null && stderr.Contains("no devices"))
                return false;

            return true;
        }

        private static void LogInCallback(string message)
        {
            if (logCallbackFunction != null)
                logCallbackFunction.Invoke(message);
        }

        public static bool InstallSambaAppIfNeeded(Func<string, int> logCallback = null)
        {
            int timeout = 0;
            string stdout, stderr;

            logCallbackFunction = logCallback;

            LogInCallback("Running ADB to verify if Samba app is installed to the MotoZ...");
            RunAdbProcess(ADB_COMMAND, ADB_SHELL_LS_SAMBA_APP_COMMAND, out stdout, out stderr, false);
            // check if errors
            if (stderr != null)
            {
                if (stderr.Contains("no devices"))
                {
                    LogInCallback(stderr);
                    ThrowAdbManagerException(stderr);
                }
                if (!stderr.Contains("No such file") && stdout.Contains("Samba.apk"))
                {
                    LogInCallback("Samba app is correctly installed to the MotoZ!");
                    return true;
                }
            }
            LogInCallback("OK!");

            LogInCallback("Running ADB to grant root access...");
            RunAdbProcess(ADB_COMMAND, ADB_ROOT_COMMAND, out stdout, out stderr);
            LogInCallback("OK!");

            LogInCallback("Running ADB to disable verity...");
            RunAdbProcess(ADB_COMMAND, ADB_DISABLE_VERITY_COMMAND, out stdout, out stderr);
            LogInCallback("OK!");

            LogInCallback("Running ADB to remount filesystem...");
            RunAdbProcess(ADB_COMMAND, ADB_REMOUNT_COMMAND, out stdout, out stderr);
            if (stdout != null && !stdout.Contains("remount succeeded"))
                ThrowAdbManagerException("Error in remount. " + stderr);
            LogInCallback("OK!");

            LogInCallback("Rebooting the MotoZ (1/2)...");
            RunAdbProcess(ADB_COMMAND, ADB_REBOOT_COMMAND, out stdout, out stderr);

            timeout = 0; 
            LogInCallback("Waiting until MotoZ restarts...");
            while (!AdbIsConnected() && timeout++ < REBOOT_TIMEOUT_COUNTER)
            {
                LogInCallback(String.Format("Waiting until MotoZ restarts... ({0})", timeout));
                Thread.Sleep(REBOOT_TIMEOUT_TICK);
            }
            if (timeout >= REBOOT_TIMEOUT_COUNTER)
            {
                LogInCallback("Timed out!");
                ThrowAdbManagerException("Adb Reboot Command Timeout!");
            }
            LogInCallback("MotoZ has been up!");
            LogInCallback("OK!");

            LogInCallback("Running ADB to grant root access...");
            RunAdbProcess(ADB_COMMAND, ADB_ROOT_COMMAND, out stdout, out stderr);
            LogInCallback("OK!");
            Thread.Sleep(REBOOT_TIMEOUT_TICK);

            LogInCallback("Running ADB to remount filesystem...");
            RunAdbProcess(ADB_COMMAND, ADB_REMOUNT_COMMAND, out stdout, out stderr);
            if (stdout != null && !stdout.Contains("remount succeeded"))
                ThrowAdbManagerException("Error in remount. " + stderr);
            LogInCallback("OK!");

            LogInCallback("Running ADB to push Samba app to the MotoZ (System)...");
            RunAdbProcess(ADB_COMMAND, ADB_PUSH_SAMBA_APP_SYSTEM_COMMAND, out stdout, out stderr);
            if (stdout != null && stdout.Contains("error"))
            {
                LogInCallback("Error while installing Samba app: " + stdout);
                ThrowAdbManagerException(stdout);
            }
            LogInCallback("OK!");

            LogInCallback("Running ADB to configure Samba app for executing...");
            RunAdbProcess(ADB_COMMAND, ADB_SHELL_CHMOD_SAMBA_APP_COMMAND, out stdout, out stderr);
            if (stdout != null && stdout.Contains("error"))
            {
                LogInCallback("Error while installing Samba app: " + stdout);
                ThrowAdbManagerException(stdout);
            }
            LogInCallback("OK!");

            LogInCallback("Running ADB to push Samba app to the MotoZ (Data)...");
            RunAdbProcess(ADB_COMMAND, ADB_PUSH_SAMBA_APP_LOCAL_COMMAND, out stdout, out stderr);
            if (stdout != null && stdout.Contains("error"))
            {
                LogInCallback("Error while pushing Samba app: " + stdout);
                ThrowAdbManagerException(stdout);
            }
            LogInCallback("OK!");

            LogInCallback("Running ADB to install Samba app...");
            RunAdbProcess(ADB_COMMAND, ADB_SHELL_PM_INSTALL_SAMBA_APP_COMMAND, out stdout, out stderr);
            if (stdout != null && stdout.Contains("error"))
            {
                LogInCallback("Error while installing Samba app: " + stdout);
                ThrowAdbManagerException(stdout);
            }
            LogInCallback("OK!");

            LogInCallback("Rebooting the MotoZ (2/2)...");
            RunAdbProcess(ADB_COMMAND, ADB_REBOOT_COMMAND, out stdout, out stderr);

            timeout = 0; 
            LogInCallback("Waiting until MotoZ restarts...");
            while (!AdbIsConnected() && timeout++ < REBOOT_TIMEOUT_COUNTER)
            {
                LogInCallback(String.Format("Waiting until MotoZ restarts... ({0})", timeout));
                Thread.Sleep(REBOOT_TIMEOUT_TICK);
            }
            if (timeout >= REBOOT_TIMEOUT_COUNTER)
            {
                LogInCallback("Timed out!");
                ThrowAdbManagerException("Adb Reboot Command Timeout!");
            }
            LogInCallback("OK!");

            LogInCallback("Running ADB to verify if Samba app is installed to the MotoZ...");
            RunAdbProcess(ADB_COMMAND, ADB_SHELL_LS_SAMBA_APP_COMMAND, out stdout, out stderr, false);
            // check if errors
            if (stderr != null && stderr.Contains("No such file"))
            {
                LogInCallback("Samba app is correctly installed to the MotoZ!");
                return false;
            }
            LogInCallback("OK!");

            LogInCallback("Samba is ready!");

            return true;
        }

        public static bool GetSerialNumbersOfConnectedPhones(out string[] serialNumber)
        {
            string stdout = null, stderr = null;

            RunAdbProcess(ADB_COMMAND, ADB_DEVICES_COMMAND, out stdout, out stderr, true, false);

            if (stderr != null && !("".Equals(stderr)))
                ThrowAdbManagerException(stderr);

            serialNumber = null;

            if (stdout == null)
                return true;

            string[] tokens = Regex.Split(stdout, @"\r\n");
            serialNumber = new string[tokens.Length - 3];

            List<string> serialNumberList = new List<string>();

            foreach (string s in tokens) {
                if (s.Equals("") || s.Contains("devices"))
                    continue;
                string[] sn = s.Split('\t');
                serialNumberList.Add(sn[0]);
            }

            serialNumber = serialNumberList.ToArray();

            return true;
        }

        public static CommandResult RunCommand(Command command, Arguments args, Func<int, int> usbManagementFunction)
        {
            usbManagementCallback = usbManagementFunction;

            runningTest = true;

            string adbArguments = BuildArgumentString(command, args);

            CheckIfLogCatWorkerIsRunningAndStartIfNot();

            ExecuteShellAmCommand(adbArguments);

            commandResult = ObtainCommandResult();

            runningTest = false;

            usbManagementCallback = null;

            return commandResult;
        }

        #endregion

        #region Helper Methods

        protected static void ThrowAdbManagerException(string message)
        {
            runningTest = false;
            commandProcessed = true;
            throw new AdbManagerException(message);
        }

        protected static string BuildArgumentString(Command command, Arguments args)
        {
            string result = "";

            currentCommand = randomGenerator.Next(1, 1024 * 1024 * 1024);

            Logger.Log("Current Test ID: " + currentCommand);

            result += ADB_ARGUMENT_STRING_TEMPLATE.Replace(ARG_NAME, ARG_COMMAND).Replace(ARG_VALUE, CommandExtensions.ToCommandString(command));
            result += ADB_ARGUMENT_STRING_TEMPLATE.Replace(ARG_NAME, ARG_ID).Replace(ARG_VALUE, currentCommand.ToString());

            foreach (Argument arg in args.getList())
            {
                result += ADB_ARGUMENT_STRING_TEMPLATE.Replace(ARG_NAME, arg.Name).Replace(ARG_VALUE, arg.Value);
            }

            commandProcessed = false;

            return result;
        }

        protected static void CheckIfLogCatWorkerIsRunningAndStartIfNot()
        {
            if (isAdbLogCatRunning)
                return;

            logCatWorker = new Thread(new ThreadStart(AdbManager.AdbLogCatWorker));
            logCatWorker.Start();

            isAdbLogCatRunning = true;
        }

        protected static void AdbLogCatWorker()
        {
            while (runningTest && !commandProcessed)
            {
                logCatProcess = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = ADB_COMMAND;
                startInfo.Arguments = ADB_LOGCAT_COMMAND + GetSerialNumberString();;
                Logger.Log("> " + startInfo.Arguments);
                logCatProcess.StartInfo = startInfo;
                logCatProcess.Start();
                Logger.Log("Starting ADB LogCat...");

                while (runningTest && !commandProcessed)
                {
                    string line = logCatProcess.StandardOutput.ReadLine();

                    if (line == null) break;

                    if (line.Contains(TAG_LOG))
                    {
                        string[] tokens = Regex.Split(line, TAG_LOG);
                        if (tokens.Length != 2) continue;
                        tokens = tokens[1].Split('|');

                        if (!tokens[2].Equals(currentCommand.ToString())) continue;

                        commandResult = new CommandResult();
                        commandResult.Result = tokens[1];
                        commandResult.Id = tokens[2];
                        commandResult.Comments = tokens[3];

                        Logger.Log("Command Result: " + line);
                        commandProcessed = true;
                    }
                }

                Logger.Log("LogCat exited.");
                if (!logCatProcess.HasExited)
                    logCatProcess.Kill();
            }

            isAdbLogCatRunning = false;
        }

        protected static void ExecuteShellAmCommand(string adbArguments)
        {
            AdbShellWorker(adbArguments);
        }

        protected static void RunAdbProcess(string command, string arguments, out string stdout, out string stderr, bool raiseException = true, bool addSerialNumber = true)
        {
            System.Diagnostics.Process adbShellProcess;
            System.Diagnostics.ProcessStartInfo adbShellStartInfo;

            adbShellProcess = new System.Diagnostics.Process();
            adbShellStartInfo = new System.Diagnostics.ProcessStartInfo();
            adbShellStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            adbShellStartInfo.RedirectStandardOutput = true;
            adbShellStartInfo.RedirectStandardError = true;
            adbShellStartInfo.UseShellExecute = false;
            adbShellStartInfo.CreateNoWindow = true;
            adbShellStartInfo.FileName = command;
            adbShellStartInfo.Arguments = arguments + 
                (addSerialNumber ? GetSerialNumberString() : "");
            Logger.Log("> " + adbShellStartInfo.Arguments);
            adbShellProcess.StartInfo = adbShellStartInfo;
            adbShellProcess.Start();
            stdout = adbShellProcess.StandardOutput.ReadToEnd();
            stderr = adbShellProcess.StandardError.ReadToEnd();
            adbShellProcess.WaitForExit();
            Logger.Log("adbShellWorker: StdOut = " + stdout);
            Logger.Log("adbShellWorker: StdErr = " + stderr);
            if (raiseException && stderr != null && !stderr.Equals(""))
                ThrowAdbManagerException(stderr);
            while (!adbShellProcess.HasExited) Thread.Sleep(100);
        }

        protected static void AdbShellWorker(string args)
        {
            Logger.Log("Starting ADB Shell PM...");

            string stdout = null, stderr = null;

            if (!isModPermissionSent)
            {
                RunAdbProcess(ADB_COMMAND, ADB_SHELL_PM_RAW_COMMAND, out stdout, out stderr);

                if (stderr != null && !stderr.Equals(""))
                    ThrowAdbManagerException(stderr);

                isModPermissionSent = true;
            }

            RunAdbProcess(ADB_COMMAND, ADB_SHELL_AM_STOP_COMMAND + args, out stdout, out stderr);

            RunAdbProcess(ADB_COMMAND, ADB_SHELL_AM_START_COMMAND + args, out stdout, out stderr);

            if (usbManagementCallback != null)
                usbManagementCallback(0);
        }

        protected static CommandResult ObtainCommandResult()
        {
            // wait until command is being processed in another thread
            // raise an exception on timeout
            int timeout = 0;

            Console.Out.Flush();

            if (usbManagementCallback != null)
            {
                Logger.Log("Waiting (USB)...");
                while (!commandProcessed && timeout++ < UNDER_TEST_TIMEOUT_COUNTER)
                    Thread.Sleep(COMMAND_TIMEOUT_TICK);
                usbManagementCallback(1);
            }

            Logger.Log("Waiting (Command)...");
            while (!commandProcessed && timeout++ < COMMAND_TIMEOUT_COUNTER)
                Thread.Sleep(COMMAND_TIMEOUT_TICK);

            // check if while ended by timeout
            if (timeout >= COMMAND_TIMEOUT_COUNTER)
                ThrowAdbManagerException("Command Timeout!");

            logCatWorker.Abort();

            return commandResult;
        }

        #endregion

    }

}

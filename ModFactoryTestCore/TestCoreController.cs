using IniParser;
using IniParser.Model;
using ModFactoryTestCore.Domain;
using System;

using log4net;
using log4net.Config;
using log4net.Appender;
using System.IO;
using System.Threading;

namespace ModFactoryTestCore
{    
    public class TestCoreController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("ModFactoryTest");
        
        public string TrackId { get; set; }

        public Rack Rack { get; set; }
        public DtvGen DtvGen { get; set; }
        public FlexFlow FlexFlow { get; set; }
        public Jig Jig { get; set; }
        public Andon Andon { get; set; }
        public Mod Mod { get; set; }
        public MotoZPCBA MotoZPCBA { get; set; }
        public MQS MQS { get; set; }
        public PwrSupply PwrSupply { get; set; }

        private IniData data;
        private static readonly string CONFIG_APP_FILE = "C:/prod/Config/TestConfig.ini";
        private Func<TestCoreMessages.TypeMessage, string, int> callback;
        private bool logLocalTestSummary = true;
        private bool logLocalTestReport = true;
        public string pathTest = string.Empty;
        public string logFileLocation { get; set; }
        public bool runTest { get; set; }
        public string newLogFileLocation { get; set; }
        private bool isPostEnable = false;


        public TestCoreController(Func<TestCoreMessages.TypeMessage, string, int> callback)
        {
            this.Rack = new Rack();
            this.DtvGen = new DtvGen();
            this.Jig = new Jig(this);
            this.Andon = new Andon();
            this.Mod = new Mod(this);
            this.MotoZPCBA = new MotoZPCBA();
            this.MQS = new MQS();
            this.FlexFlow = new FlexFlow(MQS.GetLogResult());
            this.PwrSupply = new PwrSupply();

            //Access to IniParser
            var parser = new FileIniDataParser();
            data = parser.ReadFile(CONFIG_APP_FILE);

            //Callback
            this.callback = callback;

            logLocalTestSummary = Boolean.Parse(GetValueConfiguration("SETTINGS", "TEST_SUMMARY_LOCAL_RECORD").ToLower());
            logLocalTestReport  = Boolean.Parse(GetValueConfiguration("SETTINGS", "TEST_RESULT_LOCAL_RECORD").ToLower());
            pathTest = GetValueConfiguration("SETTINGS", "PATH_LOG_TEST").ToLower();
            runTest = true;
            this.isPostEnable = Boolean.Parse(GetValueConfiguration("SETTINGS", "POST_ENABLE").ToLower());
        }
        
        public string GetValueConfiguration(string session, string property)
        {
            string config = data[session][property];

            if (config == null)
                throw new Exception(TestCoreMessages.ParseMessages(TestCoreMessages.CONFIG_FILE_PARAMETER_NOT_FOUND));
                        
            return config;
        }

        public void NotifyUI(TestCoreMessages.TypeMessage typeMessage, string message = null)
        {
            callback(typeMessage, message);

            if(logLocalTestSummary)
            {
                if (typeMessage != TestCoreMessages.TypeMessage.UPDATE_TEST_PERCENTUAL)
                    log.Info(message);
            }
        }
              
        public void StartTests(string trackId, TestCoreMessages.StationType stationType)
        {
            //Do POST of this trackid            
            if(isPostEnable)
            {
                MQS.Post();
            }

            TestCoreRunner tcr = new TestCoreRunner(this, trackId, stationType);
            tcr.RunTestsToStation();
        }

        public void StartLog4Net(string trackId)
        {
            RemoveDraftFiles();

            //Creating a new log file to this trackid
            var h = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            foreach (IAppender a in h.Root.Appenders)
            {
                if (a.Name == "fileAppender")
                {
                    FileAppender fa = (FileAppender)a;
                    logFileLocation = string.Format(@pathTest +
                        "{0}.log", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "_" + trackId + "_TS_XXXX");

                    fa.File = logFileLocation;
                    fa.ActivateOptions();
                    break;
                }
            }
        }

        public void RemoveDraftFiles()
        {
            //Removing draft files
            var dir = new DirectoryInfo(@pathTest);

            foreach (var file in dir.EnumerateFiles("*XXXX.log"))
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        internal void SetLogNameTo(string logNamePassFail)
        {
            if(File.Exists(logFileLocation))
            {
                newLogFileLocation = logFileLocation.Replace("XXXX", logNamePassFail);
                NotifyUI(TestCoreMessages.TypeMessage.UPDATE_DGV_TRACKID, newLogFileLocation);
                File.Move(logFileLocation, newLogFileLocation);
            }
        }

        public int ManageUsb(int state)
        {
            Mod.TestPointState tpState = state == 0 ?
                ModFactoryTestCore.Mod.TestPointState.OPEN :
                ModFactoryTestCore.Mod.TestPointState.CLOSED;

            int retCode = this.Mod.SetTestPointState(ModFactoryTestCore.Mod.TestPoint.SWITCH_USB, tpState);

            
            //TODO: remove on release version. Only for test
            string tmp = state == 1? "Please insert the USB cable." : "Please remove the USB cable.";
            NotifyUI(TestCoreMessages.TypeMessage.REQUEST_USER_ACTION, tmp);
            Thread.Sleep(4000);


            return retCode;
        }
    
    }
       

}

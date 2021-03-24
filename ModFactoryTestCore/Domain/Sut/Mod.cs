
using I2CRack;
using ModFactoryTest.Tool;
using ModFactoryTestCore.Domain.Tool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ModFactoryTestCore
{
    public class Mod
    {
        public enum TestPoint { LED025, LED050, REED_SWITCH, SWITCH_USB, CHARGER };
        public enum TestPointState { CLOSED, OPEN };

        private Dictionary<string, string> dicSambaModInfo           = null;
        private static readonly int    TRY_READ_TIMES                = 3;
        private static readonly string TEST_AMPS                     = "test_amps_";
        //private static readonly string AMPS_ATTACHED                 = "test_amps_attached";
        //private static readonly string AMPS_DATABASE_VALUES          = "test_amps_database_values";
        //private static readonly string AMPS_DISPLAY_VALUES           = "test_amps_display_values";
        //private static readonly string AMPS_FRAMEWORK_BATTERY_VALUES = "test_amps_framework_battery_values";
        //private static readonly string AMPS_FRAMEWORK_PTP_VALUES     = "test_amps_framework_ptp_values";
        private static readonly string AMPS_HISTORY                  = "test_amps_history";
        private TestCoreController tcc;

        private Func<string, int> callbackInfoMod;



        public Mod(TestCoreController testCoreController)
        {
            dicSambaModInfo = new Dictionary<string, string>();
            this.tcc = testCoreController;
        }     
        
        public void readSambaModInfoFile(string pathOfFile)
        {
            string line;
            StreamReader file = null;
            dicSambaModInfo.Clear();

            try
            {
                file = new StreamReader(@pathOfFile);

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Equals(""))
                        continue;

                    if (line.Contains(TEST_AMPS))
                    {
                        if (line.Contains(AMPS_HISTORY))
                        {
                            string[] ts = line.Split(' ');
                            dicSambaModInfo.Add(ts[1], ts[0]);
                            return;
                        }

                        string[] testResult = line.Split(' ');
                        dicSambaModInfo.Add(testResult[1], testResult[0]);

                        continue;
                    }

                    string[] substring = line.Split(':');
                    dicSambaModInfo.Add(substring[0], substring[1]);
                }
            }
            catch (Exception)
            {
                throw new ModException(TestCoreMessages.ParseMessages(TestCoreMessages.FILE_NOT_FOUND));
            }
            finally
            {
                file.Close();
            }
        }
        
        public int SetAllTestPointOpen()
        {
            int retCode = CI2cControl.SendI2cCommand(TestPoint.LED025 + "_" + TestPointState.OPEN);
            CheckReturn(retCode);

            retCode = CI2cControl.SendI2cCommand(TestPoint.LED050 + "_" + TestPointState.OPEN);
            CheckReturn(retCode);

            retCode = CI2cControl.SendI2cCommand(TestPoint.REED_SWITCH + "_" + TestPointState.OPEN);
            CheckReturn(retCode);

            retCode = CI2cControl.SendI2cCommand(TestPoint.SWITCH_USB + "_" + TestPointState.OPEN);
            CheckReturn(retCode);

            retCode = CI2cControl.SendI2cCommand(TestPoint.CHARGER + "_" + TestPointState.OPEN);
            CheckReturn(retCode);

            return retCode;
        }
                
        public int SetTestPointState(TestPoint testPoint, TestPointState state)
        {
            int retCode = CI2cControl.SendI2cCommand(testPoint.ToString() + "_" + state);
            CheckReturn(retCode);

            return retCode;
        }
        
        public double GetTestPointVoltage(TestPoint testPoint)
        {
            double dVoltage = 0.0;
            int count = 0;
            int status = -1;

            status = SetTestPointState(testPoint, TestPointState.CLOSED);
            CheckReturn(status);

            while ( (dVoltage < 2) && (count < TRY_READ_TIMES) )
            {
                dVoltage += tcc.PwrSupply.ReadDVM1Voltage();
                Thread.Sleep(1000);
                count++;
            }

            dVoltage = dVoltage / count;
           
            status = SetTestPointState(testPoint, TestPointState.OPEN);
            CheckReturn(status);
            
            return dVoltage;
        }

        public void GetModInfo(Func<string, int> callbackInfoMod)
        {
            try
            {
                AmpsManager.ExecutePythonScript(callbackInfoMod);
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new ModException("Error " + retCode + " returned.");
        }


        /// <summary>
        /// Class that represents Mod custom exceptions
        /// </summary>
        [Serializable]
        public class ModException : Exception
        {
            public ModException()
            { }

            public ModException(string message)
                : base(message)
            { }

            public ModException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
    

    
    //Todo: check se é necessario as classes abaixo
    //Internal classses to represent sub categories from MOD information
    internal class Info
    {
        public Info(string locale, int VID, string Vendor, int PID, string Product)
        {
            this.locale  = locale;
            this.VID     = VID;
            this.Vendor  = Vendor;
            this.PID     = PID;
            this.Product = Product;
        }

        public string locale  { get; set; }
        public int VID        { get; set; }
        public string Vendor  { get; set; }
        public int PID        { get; set; }
        public string Product { get; set; }            
    } 

    internal class Events
    {
        public Events(string LastAttachTime, string LastDetachTime, int NumberOfAttaches, int NumberOfAttachesToday, int NumberOfDetachesToday)
        {
            this.LastAttachTime        = LastAttachTime;
            this.LastDetachTime        = LastDetachTime;
            this.NumberOfAttaches      = NumberOfAttaches;
            this.NumberOfAttachesToday = NumberOfAttachesToday;
            this.NumberOfDetachesToday = NumberOfDetachesToday;
        }

        public string LastAttachTime     { get; set; }      
        public string LastDetachTime     { get; set; }      
        public int  NumberOfAttaches     { get; set; }      
        public int NumberOfAttachesToday { get; set; }      
        public int NumberOfDetachesToday { get; set; }
    }

    internal class APK
    {
        public APK()
        {                       
            this.IsCertified    = IsCertified;   
            this.HasUserConsent = HasUserConsent;
            this.SetupComplete  = SetupComplete; 
        }

        public bool IsCertified    { get; set; }
        public bool HasUserConsent { get; set; }
        public bool SetupComplete  { get; set; }
    }

    internal class Firmware
    {
        public Firmware(string Version, int UpdateStatus, string UpdateVersion, string UpdateDetails, string UpdateURI,
            int UpdateSize, int UpdateBattery, int UpdateManagedOTA, string LastUpdateCheck, int UpdateFailCode,
            int UpdateRetryPhase, int AutoInstall, int Mandatory, int IgnoreRelease, int NumberOfIgnores)
        {
            this.Version          = Version;         
            this.UpdateStatus     = UpdateStatus;
            this.UpdateVersion    = UpdateVersion;   
            this.UpdateDetails    = UpdateDetails;  
            this.UpdateURI        = UpdateURI;       
            this.UpdateSize       = UpdateSize;      
            this.UpdateBattery    = UpdateBattery;   
            this.UpdateManagedOTA = UpdateManagedOTA;
            this.LastUpdateCheck  = LastUpdateCheck; 
            this.UpdateFailCode   = UpdateFailCode;  
            this.UpdateRetryPhase = UpdateRetryPhase;
            this.AutoInstall      = AutoInstall;     
            this.Mandatory        = Mandatory;       
            this.IgnoreRelease    = IgnoreRelease;
            this.NumberOfIgnores  = NumberOfIgnores;
        }

        public string Version         { get; set; } 
        public int UpdateStatus       { get; set; }
        public string UpdateVersion   { get; set; }
        public string UpdateDetails   { get; set; }
        public string UpdateURI       { get; set; }
        public int UpdateSize         { get; set; }
        public int UpdateBattery      { get; set; }
        public int UpdateManagedOTA   { get; set; }
        public string LastUpdateCheck { get; set; }
        public int UpdateFailCode     { get; set; }
        public int UpdateRetryPhase   { get; set; }
        public int AutoInstall        { get; set; }
        public int Mandatory          { get; set; } 
        public int IgnoreRelease      { get; set; }
        public int NumberOfIgnores    { get; set; }
    }

    internal class Display
    {
        public Display(string Brightness, string state)
        {
            this.Brightness = Brightness;
            this.State = State;
        }

        public string Brightness { get; set; }
        public string State      { get; set; }
    }

    internal class Battery
    {
        public Battery(int Status, int Technology, int Type, int Percentage, int Temperature, int VoltageNow, int VoltageMaxDesign,
            int ChargeFullDesign, int CurrentNow, int InternalSend, int InternalReceived, int MaxOutputCurrent, int PowerAvailable,
            int PowerRequired, int PowerSource)
        {
           this.Status            = Status;          
           this.Technology        = Technology;     
           this.Type              = Type;           
           this.Percentage        = Percentage;      
           this.Temperature       = Temperature;    
           this.VoltageNow        = VoltageNow;      
           this.VoltageMaxDesign  = VoltageMaxDesign;
           this.ChargeFullDesign  = ChargeFullDesign;
           this.CurrentNow        = CurrentNow;      
           this.InternalSend      = InternalSend;    
           this.InternalReceived  = InternalReceived;
           this.MaxOutputCurrent  = MaxOutputCurrent;
           this.PowerAvailable    = PowerAvailable;  
           this.PowerRequired     = PowerRequired;
           this.PowerSource       = PowerSource;     
        }

        public int Status           { get; set; } 
        public int Technology       { get; set; } 
        public int Type             { get; set; } 
        public int Percentage       { get; set; } 
        public int Temperature      { get; set; } 
        public int VoltageNow       { get; set; }
        public int VoltageMaxDesign { get; set; }
        public int ChargeFullDesign { get; set; }
        public int CurrentNow       { get; set; }
        public int InternalSend     { get; set; }
        public int InternalReceived { get; set; }
        public int MaxOutputCurrent { get; set; }
        public int PowerAvailable   { get; set; }
        public int PowerRequired    { get; set; }
        public int PowerSource      { get; set; }




    }
}

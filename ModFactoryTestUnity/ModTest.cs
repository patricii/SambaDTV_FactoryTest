using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModFactoryTestCore;
using System.Threading;

namespace ModFactoryTestUnity
{
    [TestClass]
    public class ModTest
    {
        TestCoreController tcc = new TestCoreController(UtilTest.WriteTestSummary);

        [TestMethod]
        public void TestModParserFile()
        {
            tcc.Mod.readSambaModInfoFile(@"C:\prod\amps\Samba_Mod_Info.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestModParserFileException()
        {
            tcc.Mod.readSambaModInfoFile(@"C:\prod\amps\Samba_Mod_Info1.txt");
        }
        

        [TestMethod]
        public void TestModSetAllPointLedOpen()
        {
            int retCode = tcc.Mod.SetAllTestPointOpen();

            Thread.Sleep(2000);
        }


        [TestMethod]
        public void TestModLed25GetVoltage()
        {
            double retCode = tcc.Mod.GetTestPointVoltage(Mod.TestPoint.LED025);

            Thread.Sleep(1000);

            Console.WriteLine("LED 025 Voltage: " + retCode);
        }

        [TestMethod]
        public void TestModLed50GetVoltage()
        {
            double retCode = tcc.Mod.GetTestPointVoltage(Mod.TestPoint.LED050);

            Thread.Sleep(1000);

            Console.WriteLine("LED 050 Voltage: " + retCode);
        }

        [TestMethod]
        public void TestModReedSwitchGetVoltage()
        {
            double retCode = tcc.Mod.GetTestPointVoltage(Mod.TestPoint.REED_SWITCH);

            Thread.Sleep(1000);

            Console.WriteLine("REED SWITCH Voltage: " + retCode);
        }

        [TestMethod]
        public void TestModSwitchUsbGetVoltage()
        {
            double retCode = tcc.Mod.GetTestPointVoltage(Mod.TestPoint.SWITCH_USB);

            Thread.Sleep(1000);

            Console.WriteLine("SWITCH_USB Voltage: " + retCode);
        }


             
    }
}

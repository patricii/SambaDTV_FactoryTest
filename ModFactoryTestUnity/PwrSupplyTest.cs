using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModFactoryTestCore;

namespace ModFactoryTestUnity
{
    [TestClass]
    public class PwrSupplyTest
    {
        TestCoreController tcc = new TestCoreController(UtilTest.WriteTestSummary);
        
        [TestMethod]
        public void TestPwrSupplySetCharger5V()
        {
            int retCode = tcc.PwrSupply.SetCharger5V();
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestPwrSupplySetBattery4V()
        {
            int retCode = tcc.PwrSupply.SetBattery4V();
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestPwrSupplyReadBattery()
        {
            double retCode = tcc.PwrSupply.ReadBattery();
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestPwrSupplyReadCharger()
        {
            double retCode = tcc.PwrSupply.ReadChargerCurrent();
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestPwrSupplySetPowerSupply1_2_2_ON()
        {
            int retCode = tcc.PwrSupply.SetPowerSupply(1, "2", "2", "ON");
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();

            double Voltage = tcc.PwrSupply.ReadDVM1Voltage();

        }
    }
}

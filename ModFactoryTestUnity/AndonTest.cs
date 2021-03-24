using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModFactoryTestCore;
using System.Threading;

namespace ModFactoryTestUnity
{
    [TestClass]
    public class AndonTest
    {

        TestCoreController tcc = new TestCoreController(UtilTest.WriteTestSummary);

        [TestMethod]
        public void TestAndonStateON()
        {
            tcc.NotifyUI(TestCoreMessages.TypeMessage.WARNING, "Iniciando...");

            int retCode = tcc.Andon.SetState(ModFactoryTestCore.Domain.Andon.State.ON);
            Thread.Sleep(2000);
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestAndonStateOFF()
        {
            int retCode = tcc.Andon.SetState(ModFactoryTestCore.Domain.Andon.State.OFF);
            Thread.Sleep(2000);
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestAndonStateFAIL()
        {
            int retCode = tcc.Andon.SetState(ModFactoryTestCore.Domain.Andon.State.FAIL);
            Thread.Sleep(2000);
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestAndonStatePASS()
        {
            int retCode = tcc.Andon.SetState(ModFactoryTestCore.Domain.Andon.State.PASS);
            Thread.Sleep(2000);
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }
    }
}

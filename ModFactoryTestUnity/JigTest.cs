using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModFactoryTestCore;
using System.Threading;

namespace ModFactoryTestUnity
{
    [TestClass]
    public class JigTest
    {
        TestCoreController tcc = new TestCoreController(UtilTest.WriteTestSummary);

        [TestMethod]
        public void TestJigOpen()
        {
            tcc.Jig.OpenJig();
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void TestJigClose()
        {
            tcc.Jig.CloseJig();
            Thread.Sleep(3000);
        }
    }
}

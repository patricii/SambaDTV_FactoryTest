using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModFactoryTestCore;

namespace ModFactoryTestUnity
{
    [TestClass]
    public class FlexFlowTest
    {
        TestCoreController tcc = new TestCoreController(UtilTest.WriteTestSummary);

        [TestMethod]
        public void TestFlexFlowPost()
        {
            tcc.FlexFlow.Post();
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModFactoryTestCore;

namespace ModFactoryTestUnity
{
    [TestClass]
    public class MQSTest
    {
        TestCoreController tcc = new TestCoreController(UtilTest.WriteTestSummary);

        [TestMethod]
        public void TestMQSSetTrackId()
        {
            int retCode = tcc.MQS.SetTrackId("1234567890");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ModFactoryTestCore.MQS.MQSException))]
        public void TestMQSSetTrackIdMoreThan10()
        {
            int retCode = tcc.MQS.SetTrackId("01234567890");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ModFactoryTestCore.MQS.MQSException))]
        public void TestMQSSetTrackIdLessThan10()
        {
            int retCode = tcc.MQS.SetTrackId("012345678");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ModFactoryTestCore.MQS.MQSException))]
        public void TestMQSSetTrackIdEmpty()
        {
            int retCode = tcc.MQS.SetTrackId("");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        public void TestMQSSetTrackIdAddLogResult()
        {
            int retCode = tcc.MQS.SetTrackId("1234567890");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();

            retCode = tcc.MQS.AddLogResult("testCode", "testDescription", "20", "10", "5", "yHightLimit", "yLowLimit", 0, "units","errorMessage");
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ModFactoryTestCore.MQS.MQSException))]
        public void TestMQSSetTrackIdAddLogResultFail()
        {
            int retCode = tcc.MQS.SetTrackId("1234567890");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();

            retCode = tcc.MQS.AddLogResult("testCode", "testDescription", "x", "10", "5", "yHightLimit", "yLowLimit", 0, "units", "errorMessage");
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();            
        }


        [TestMethod]
        public void TestMQSSetTrackIdLogResultTestPass()
        {
            int retCode = tcc.MQS.SetTrackId("1234567890");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();

            retCode = tcc.MQS.AddLogResult("testCode", "testDescription", "20", "10", "5", "yHightLimit", "yLowLimit", 0, "units", "notErrorMessage");
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();

            retCode = tcc.MQS.LogResult("PASS");
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }


        [TestMethod]
        public void TestMQSSetTrackIdLogResultTestFail()
        {
            int retCode = tcc.MQS.SetTrackId("1234567890");

            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();

            retCode = tcc.MQS.AddLogResult("testCode", "testDescription", "20", "10", "5", "yHightLimit", "yLowLimit", 0, "units", "ErrorMessage");
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();

            retCode = tcc.MQS.LogResult("FAIL");
            if (retCode != TestCoreMessages.SUCCESS)
                Assert.Fail();
        }

    }
}

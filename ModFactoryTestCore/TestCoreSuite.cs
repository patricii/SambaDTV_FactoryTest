using ModFactoryTestCore.Domain.Test;
using System.Collections.Generic;

namespace ModFactoryTestCore
{
    public class TestCoreSuite
    {
        private TestCoreController tcc;
        private TestCoreMessages.StationType stationType;
        private List<TestCaseBase> testCasesStationA;
        private List<TestCaseBase> testCasesStationB;
        private List<TestCaseBase> testCasesStationC;
        private List<TestCaseBase> testCasesStationD; //To debug Test;
        private string station;

        public TestCoreSuite(TestCoreMessages.StationType stationType, TestCoreController testCoreController)
        {
            this.stationType = stationType;
            this.tcc = testCoreController;

            loadTestCases(stationType);
        }

        private void loadTestCases(TestCoreMessages.StationType stationType)
        {
            switch (stationType)
            {
                case TestCoreMessages.StationType.A:
                    testCasesStationA = new List<TestCaseBase>();
                    break;
                case TestCoreMessages.StationType.B:
                    testCasesStationB = new List<TestCaseBase>();
                    //testCasesStationB.Add(new TestCasePowerOn(tcc));
                    //testCasesStationB.Add(new TestCaseChargerVerification(tcc));
                    //testCasesStationB.Add(new TestCaseLedVerification(tcc));
                    //testCasesStationB.Add(new TestCaseMobileInterfaceCommunication(tcc));
                    //testCasesStationB.Add(new TestCaseTunerVerification(tcc));
                    break;
                case TestCoreMessages.StationType.C:
                    testCasesStationC = new List<TestCaseBase>();
                    //testCasesStationC.Add(new TestCaseMagneticTest(tcc));
                    //testCasesStationC.Add(new TestCaseMobileInterfaceCommunicationStationC(tcc));
                    //testCasesStationC.Add(new TestCaseBatteryTest(tcc));
                    //testCasesStationC.Add(new TestCaseAntennaTest(tcc));
                    //testCasesStationC.Add(new TestCaseLedVerificationWithCamera(tcc));
                    break;

                case TestCoreMessages.StationType.D:
                    testCasesStationD = new List<TestCaseBase>();
                    //testCasesStationD.Add(new TestCasePowerOn(tcc));
                    //testCasesStationD.Add(new TestCaseChargerVerification(tcc));
                    //testCasesStationD.Add(new TestCaseLedVerification(tcc));
                    //testCasesStationD.Add(new TestCaseMobileInterfaceCommunication(tcc));
                    testCasesStationD.Add(new TestCaseTunerVerification(tcc));
                    //testCasesStationD.Add(new TestCaseMagneticTest(tcc));
                    //testCasesStationD.Add(new TestCaseMobileInterfaceCommunicationStationC(tcc));
                    //testCasesStationD.Add(new TestCaseBatteryTest(tcc));
                    //testCasesStationD.Add(new TestCaseAntennaTest(tcc));
                    //testCasesStationD.Add(new TestCaseLedVerificationWithCamera(tcc));
                    break;

                default:
                    break;
            }
        }

        public List<TestCaseBase> GetTestCasesList(TestCoreMessages.StationType stationType)
        {
            List<TestCaseBase> retList = new List<TestCaseBase>();

            switch (stationType)
            {
                case TestCoreMessages.StationType.A:
                    retList = testCasesStationA;
                    break;
                case TestCoreMessages.StationType.B:
                    retList = testCasesStationB;
                    break;
                case TestCoreMessages.StationType.C:
                    retList = testCasesStationC;
                    break;
                case TestCoreMessages.StationType.D:
                    retList = testCasesStationD;
                    break;
            }

            return retList;
        }

       
    }
}

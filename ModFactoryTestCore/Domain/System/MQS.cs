using I2CRack;
using System;
using System.Text;

namespace ModFactoryTestCore
{
    public class MQS
    {
        private string trackId = null;
        private BzLogResult logResult = null;
        private StringBuilder strErrorMessage = null;

        public MQS()
        {
            logResult = new BzLogResult();
            strErrorMessage = new StringBuilder(256);
            strErrorMessage.Insert(0, TestCoreMessages.ParseMessages(TestCoreMessages.FAIL_INITIALIZE_BZ_EQUIPAMENTS));

            int retCode = -1;

            retCode = logResult.LoadLogResult(strErrorMessage.ToString());

            if (retCode != TestCoreMessages.SUCCESS)
                CheckReturn(retCode);

            retCode = logResult.StartLogResult(strErrorMessage.ToString());
            
            if (retCode != TestCoreMessages.SUCCESS)
                CheckReturn(retCode);
        }


        public BzLogResult GetLogResult()
        {
            return logResult;
        }

        public int SetTrackId(string trackId)
        {
            if (trackId.Length != 10)
                throw new MQSException(TestCoreMessages.ParseMessages(TestCoreMessages.INVALID_TRACKID_FORMAT));

            this.trackId = trackId;
            logResult.TrackId = this.trackId;
            return TestCoreMessages.SUCCESS;
        }

        public int AddLogResult(string testCode, string testDescription, string testResult, string highLimit, string lowLimit, string yHightLimit, string yLowLimit, int passFail, string units, string errorMessage)
        {
            int retCode = -1;

            try
            {
                retCode = logResult.AddLogResult(testCode, testDescription, testResult, highLimit, lowLimit, yHightLimit, yLowLimit, passFail, units, errorMessage);
            }
            catch (Exception ex)
            {
                throw new MQSException("Error on MQS Class. Erro: " + retCode + " returned.", ex);                
            }

            if (retCode != TestCoreMessages.SUCCESS)
                CheckReturn(retCode);

            return retCode;
        }

        public int LogResult(string testStatus)
        {
            int retCode = -1;

            retCode = logResult.LogResult(testStatus, strErrorMessage.ToString());

            if (retCode != TestCoreMessages.SUCCESS)
                CheckReturn(retCode);

            return retCode;
        }

        public void Post()
        {
            logResult.POST();           
        }
        

        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new MQSException("Error on MQS Class. Erro: " + retCode + " returned.");
        }

        /// <summary>
        /// Class that represents MQS custom exceptions
        /// </summary>
        [Serializable]
        public class MQSException : Exception
        {
            public MQSException()
            { }

            public MQSException(string message)
                : base(message)
            { }

            public MQSException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}


using I2CRack;
using System;
namespace ModFactoryTestCore
{
    public class FlexFlow
    {
        private BzLogResult logResult = null;

        public FlexFlow(BzLogResult logResult)
        {
            this.logResult = logResult;
        }
        
        public void Post() 
        {
            logResult.POST();
        }
        

        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new FlexFlewException("Error " + retCode + " returned.");
        }

        /// <summary>
        /// Class that represents FlexFlow custom exceptions
        /// </summary>
        [Serializable]
        public class FlexFlewException : Exception
        {
            public FlexFlewException()
            { }

            public FlexFlewException(string message)
                : base(message)
            { }

            public FlexFlewException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}

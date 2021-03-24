
using System;
namespace ModFactoryTestCore
{
    public class MotoZPCBA
    {









        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new MotoZPCBAException("Error " + retCode + " returned.");
        }

        /// <summary>
        /// Class that represents MotoZPCBA custom exceptions
        /// </summary>
        [Serializable]
        public class MotoZPCBAException : Exception
        {
            public MotoZPCBAException()
            { }

            public MotoZPCBAException(string message)
                : base(message)
            { }

            public MotoZPCBAException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}

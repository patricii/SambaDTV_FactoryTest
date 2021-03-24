using I2CRack;
using System;

namespace ModFactoryTestCore.Domain
{
    public class Andon
    {
        public enum State { PASS, FAIL, ON, OFF };
        private State _state;

        public Andon ()
	    {
            SetState(State.OFF);
    	}

        public int SetState(State state) 
        {
            this._state = state;
            int retCode = TestCoreMessages.UNKNOW_ERROR;

            switch (state)
            {
                case State.PASS:
                    retCode = CI2cControl.SendI2cCommand("PASS_LAMP_ON");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;

                    retCode = CI2cControl.SendI2cCommand("FAIL_LAMP_OFF");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;
                    break;
          
                case State.FAIL:
                    retCode = CI2cControl.SendI2cCommand("PASS_LAMP_OFF");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;

                    retCode = CI2cControl.SendI2cCommand("FAIL_LAMP_ON");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;
                    break;

                case State.ON:
                    retCode = CI2cControl.SendI2cCommand("PASS_LAMP_ON");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;

                    retCode = CI2cControl.SendI2cCommand("FAIL_LAMP_ON");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;
                    break;

                case State.OFF:
                    retCode = CI2cControl.SendI2cCommand("PASS_LAMP_OFF");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;

                    retCode = CI2cControl.SendI2cCommand("FAIL_LAMP_OFF");
                    CheckReturn(retCode);
                    if (retCode != TestCoreMessages.SUCCESS)
                        return retCode;
                    break;
            }

            return retCode;
        }

       
        public State GetState()
        {
            return _state;
        }


        private void CheckReturn(int retCode)
        {
            if(retCode != TestCoreMessages.SUCCESS )
                throw new AndonException("Error " + retCode + " returned from \"wrapper_Handler_Bz.dll\" when try set andon lamp state.");
        }        
        
        /// <summary>
        /// Class that represents Andon custom exceptions
        /// </summary>
        [Serializable]
        public class AndonException : Exception
        {
            public AndonException()
            { }

            public AndonException(string message)
                : base(message)
            { }

            public AndonException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }





    }
}

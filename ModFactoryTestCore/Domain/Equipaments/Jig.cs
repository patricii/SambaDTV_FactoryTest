
using I2CRack;
using ModFactoryTestCore.Domain;
using System;
using System.Threading;
namespace ModFactoryTestCore
{
    public class Jig
    {
        private TestCoreController tcc = null;

        public Jig(TestCoreController testCoreController)
        {
            this.tcc = testCoreController;
        }
        
        public bool IsJigClosed()
        {
            bool retCode = false;

            if (CItemListEquip.IsOpenJigEnable() != TestCoreMessages.SUCCESS)// if == 1 that is, Jig Closed
                retCode = true;

            return retCode;
        }

        public void OpenJig()
        {
            int nStatus = 0;
            if (IsJigClosed())// if == 1 that is, Jig Closed
            {
                double dVoltage = -999;
                int Count = 0;

                nStatus = CI2cControl.SendI2cCommand("DVM1_CHARLES_CLOSE");
                CheckReturn(nStatus);
                Thread.Sleep(200);

                if (nStatus == 0)
                {
                    nStatus = tcc.Andon.SetState(Andon.State.ON);
                    CheckReturn(nStatus);
                }
                                
                if (nStatus == 0)
                {
                    while (dVoltage < 2 )//&& Count < 3)
                    {
                        dVoltage = CItemListEquip.ReadDVM1Voltage();
                        Thread.Sleep(1000);
                        Count++;
                    }
                }
                
                nStatus = CI2cControl.SendI2cCommand("DVM1_CHARLES_OPEN");

                if (nStatus == 0)
                {
                    nStatus = tcc.Andon.SetState(Andon.State.OFF);//CI2cControl.SendI2cCommand("FAIL_LAMP_OFF");
                    CheckReturn(nStatus);
                }
                
            }

        }
        
        public void CloseJig()
        {
            if (CItemListEquip.IsOpenJigEnable() == 1)
            {
                int nStatus = -1;
                double dVoltage = 999;
                int nCloseJigCount = 0;

                nStatus = CI2cControl.SendI2cCommand("DVM1_CHARLES_CLOSE");
                CheckReturn(nStatus);
                Thread.Sleep(200);

                if (nStatus == 0)
                {
                    nStatus = tcc.Andon.SetState(Andon.State.ON);
                    CheckReturn(nStatus);
                }

                if (nStatus == 0)
                {
                    while (dVoltage > 2 && nCloseJigCount < 10)
                    {
                        dVoltage = CItemListEquip.ReadDVM1Voltage();
                        nCloseJigCount++;
                    }
                }

                nStatus = CI2cControl.SendI2cCommand("DVM1_CHARLES_OPEN");
                CheckReturn(nStatus);
               
                if (nStatus == 0)
                {
                    nStatus = tcc.Andon.SetState(Andon.State.OFF);
                    CheckReturn(nStatus);
                }

                if (dVoltage > 2)
                {
                    throw new JigException("Fail to close Jig.");
                }
            }   
        }

        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new JigException("Error " + retCode + " returned from \"wrapper_Handler_Bz.dll\" when try open or close Jig.");
        }

        /// <summary>
        /// Class that represents Jig custom exceptions
        /// </summary>
        [Serializable]
        public class JigException : Exception
        {
            public JigException()
            { }

            public JigException(string message)
                : base(message)
            { }

            public JigException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}

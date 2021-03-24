using I2CRack;
using ModFactoryTestCore.Interfaces;
using System;
using System.Threading;

namespace ModFactoryTestCore
{
    public class PwrSupply  
    {

        public int SetPowerSupply(int channel, String voltage, String current, String state)
        {
            int retCode = -1;
            retCode = CJagTests.SetPowerSupply(channel, voltage, current, state);
            CheckReturn(retCode);

            return retCode;
        }

        public int SetCharger5V()
        {
            int retCode = -1;
            retCode = CJagTests.SetUSB_PS2_5V();
            CheckReturn(retCode);

            return retCode;
        }

        public int SetBattery4V()
        {
            int retCode = -1;
            retCode = CJagTests.SetBattery_PS1_4V();
            CheckReturn(retCode);

            return retCode;
        }

        public double ReadBattery()
        {
            double battery = 0;
            battery = CItemListEquip.MeasPS1Current();
            Thread.Sleep(200);

            return Convert.ToDouble(battery.ToString("N7"));
        }

        public double ReadChargerCurrent()
        {
            double charger = 0;
            charger = CItemListEquip.MeasPS2Current();
            Thread.Sleep(200);

            return Convert.ToDouble(charger.ToString("N7"));
        }

        public double ReadDVM1Voltage()
        {
            double retCode = CItemListEquip.ReadDVM1Voltage();
            return Convert.ToDouble(retCode.ToString("N7"));
        }


        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new PwrSupplyException("Error " + retCode + " returned.");
        }

        /// <summary>
        /// Class that represents PwrSupply custom exceptions
        /// </summary>
        [Serializable]
        public class PwrSupplyException : Exception
        {
            public PwrSupplyException()
            { }

            public PwrSupplyException(string message)
                : base(message)
            { }

            public PwrSupplyException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }


       
    }
}

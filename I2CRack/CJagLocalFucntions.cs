using System;
using System.Text;
using System.Threading;

namespace I2CRack
{
    public class CJagLocalFucntions
    {
        static string m_strCheckStatusResult;
        static string m_strTrackId;
        static string m_strBzModelMode;

        public static string GetPowerSupplyModel()
        {
            StringBuilder strPowerSupply = new StringBuilder(20);
            string strPowerSupplyModel;
            CItemListEquip.GetPowerSupplyModel(strPowerSupply);

            if (strPowerSupply.ToString().Contains("2306"))
                strPowerSupplyModel = "KET2306";
            else if (strPowerSupply.ToString().Contains("66319"))
                strPowerSupplyModel = "AG66319B";
            else if (strPowerSupply.ToString().Contains("2304"))
                strPowerSupplyModel = "KET2304";
            else
            {
                //TODO: need correction
                //MessageBox.Show("Power Supply model not found", "GetPowerSupplyModel", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //Application.Exit();
                strPowerSupplyModel = "NULL";
            }

            return strPowerSupplyModel;
        }


        public static string GetFisrtIniFile()
        {
            StringBuilder strData = new StringBuilder(256);

            CItemListEquip.GetFisrtIniFilefromModelFile(strData);

            return strData.ToString();
        }

        public static string GetComPortLeft()
        {
            StringBuilder strData = new StringBuilder(256);

            CItemListEquip.GetComPortLeft(strData);

            return strData.ToString();
        }

        public static string GetComPortRight()
        {
            StringBuilder strData = new StringBuilder(256);

            CItemListEquip.GetComPortRight(strData);

            return strData.ToString();
        }

        public static string GetPowerSupplyAddress()
        {
            StringBuilder strData = new StringBuilder(256);
            string strDataReturned;

            CItemListEquip.GetPS1GpibAddress(strData);

            strDataReturned = ("GPIB0::" + strData.ToString() + "::INSTR");

            return strDataReturned;
        }

        public static void SetTrackId(string strTrackId)
        {
            m_strTrackId = strTrackId;
        }

        public static String GetTrackId()
        {
            return m_strTrackId;
        }

        public static void SetCheckStatusResult(string strCheckStatusResult)
        {
            m_strCheckStatusResult = strCheckStatusResult;
        }

        public static string GetBzScanMode()
        {
            return m_strBzModelMode;
        }

        public static void SetBzScanMode(string strMode)
        {
            m_strBzModelMode = strMode;
        }

        public static int EntryHandlerSystem()
        {

            int nStatus = 0;
            nStatus = CI2cControl.SendI2cCommand("PASS_LAMP_ON");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("FAIL_LAMP_ON");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("ID_TO_VBUS_CLOSE");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("PSU2_CLOSE");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("PSU1_CLOSE");


            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("D+_D-_CLOSE");
            return nStatus;

        }

        public static int EntryHandlerTest()
        {
            int nStatus = 0;

            nStatus = CI2cControl.SendI2cCommand("PASS_LAMP_OFF");


            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("FAIL_LAMP_OFF");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("D+_D-_CLOSE");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("PSU2_CLOSE");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("PSU1_CLOSE");

            return nStatus;

        }

        public static int ExitHandlerTest(string strPassFail)
        {
            int nStatus = 0;

            if (strPassFail == "PASS")
            {
                Thread.Sleep(200);
                nStatus = CI2cControl.SendI2cCommand("PASS_LAMP_ON");
            }
            else
            {
                Thread.Sleep(200);
                nStatus = CI2cControl.SendI2cCommand("FAIL_LAMP_ON");
            }

            if (nStatus == 0)
                nStatus = CI2cControl.ReleaseGPIBMutex();
            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("D+_D-_OPEN");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("PSU2_OPEN");

            if (nStatus == 0)
                nStatus = CI2cControl.SendI2cCommand("PSU1_OPEN");
            if (nStatus == 0)
            {
                nStatus = CI2cControl.SendI2cCommand("PASS_LAMP_ON");
                if (nStatus == 0)
                    nStatus = CI2cControl.SendI2cCommand("FAIL_LAMP_ON");
            }

            return nStatus;

        }

        public static int Set_USB_PS2_5V()
        {
            int nStatus = 0;
            Thread.Sleep(500);
            nStatus = SendI2CCommand("D+_D-_CLOSE");
            nStatus = CJagTests.SetUSB_PS2_5V();

            return nStatus;
        }

        public static int ClosePowerKey()
        {
            int nStatus = 0;

            nStatus = CI2cControl.SendI2cCommand("CHLS_PW_KEY_CLOSE");

            return nStatus;
        }

        public static int OpenPowerKey()
        {
            int nStatus = 0;

            nStatus = CI2cControl.SendI2cCommand("CHLS_PW_KEY_OPEN");

            return nStatus;
        }

        public static int CloseTunerDVM()
        {
            int nStatus = 0;

            nStatus = CI2cControl.SendI2cCommand("DVM1_TUNER_CLOSE");

            return nStatus;
        }

        public static int OpenTunerDVM()
        {
            int nStatus = 0;

            nStatus = CI2cControl.SendI2cCommand("DVM1_TUNER_OPEN");

            return nStatus;
        }

        public static int SendI2CCommand(string strI2CCommand)
        {

            return CI2cControl.SendI2cCommand(strI2CCommand);
        }

        public static int SetPowerSupply(int nChannel, string strVoltage, string strCurrent, string strState)
        {

            return CJagTests.SetPowerSupply(nChannel, strVoltage, strCurrent, strState);
        }


        public static int I2CDisconnectAll()
        {
            int nStatus = 0;

            if (nStatus == 0)
                nStatus = SendI2CCommand("CHLS_PW_KEY_OPEN");

            if (nStatus == 0)
                nStatus = SendI2CCommand("PSU1_OPEN");

            if (nStatus == 0)
                nStatus = SendI2CCommand("PSU2_OPEN");

            if (nStatus == 0)
                nStatus = SendI2CCommand("D+_D-_OPEN");

            return nStatus;
        }
        public static int I2CTurnPhoneOnByVBAT()
        {
            int nStatus = 0;

            if (nStatus == 0)
                SetPowerSupply(1, "4", "2", "ON");

            if (nStatus == 0)
                nStatus = SendI2CCommand("PSU1_CLOSE");

            if (nStatus == 0)
                nStatus = SendI2CCommand("CHLS_PW_KEY_CLOSE");

            Thread.Sleep(3000);

            if (nStatus == 0)
                nStatus = SendI2CCommand("CHLS_PW_KEY_OPEN");

            return nStatus;
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Ivi.Visa.Interop;
using ModFactoryTestCore.Interfaces;
//using System.Runtime.Remoting.Messaging; // SCPI functions


namespace ModFactoryTestCore
{  
    public class DtvGen : IPowerable
    {
        private Ivi.Visa.Interop.FormattedIO488 ioTestSet;
        public bool bDTVON = false;

        public void PowerOn()
        {
            throw new NotImplementedException();
        }


        public void PowerOff()
        {
            throw new NotImplementedException();
        }

        
        public void SetCmw500DtvRssiTransmition(String frequency, String amplitude)
        {
            string strCommand = string.Empty;
            string strReturn = string.Empty;
            ioTestSet = new FormattedIO488();

            //CMW500
            try
            {
                ResourceManager grm = new ResourceManager();
                ioTestSet.IO = (IMessage)grm.Open("RS_CMW500", AccessMode.NO_LOCK, 2000, "");
            }
            catch
            {
                ioTestSet.IO = null;
            }

            if ((ioTestSet != null) && (!bDTVON))
            {
                if ((amplitude == String.Empty) || (Convert.ToDouble(amplitude) > -10))
                {
                    throw new DtvGenException("Amplitude must be between -10 to -65 dBm");
                    return;
                }

                if ((frequency == "") || (Convert.ToDouble(frequency) > 803.143) || (Convert.ToDouble(frequency) < 473.143))
                {
                    throw new DtvGenException("Frequency must be between 473.143 to 803.143 Mhz.");
                    return;    
                }

                ioTestSet.WriteString("*IDN?", true);
                Thread.Sleep(3000);
                
                //Verify if DTV wave form can be found                
                ioTestSet.WriteString("MMEM:CAT? 'D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\'", true);
                strReturn = ioTestSet.ReadString();
                
                if (strReturn.Contains("ISDB-Tb_Digital_TV_withPR.wv") == false)
                {
                    throw new DtvGenException("Wave Form Error. Wave Form: \r\n D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\ISDB-Tb_Digital_TV_withPR.wv \r\n Not found !!!");
                    return;
                }

                ioTestSet.WriteString("SOUR:GPRF:GEN:ARB:FILE 'D:\\Rohde-Schwarz\\CMW\\Data\\waveform\\ISDB-Tb_Digital_TV_withPR.wv';*OPC?", true);
                ioTestSet.WriteString("SOUR:GPRF:GEN:ARB:FILE?", true);
                ioTestSet.WriteString("SOUR:GPRF:GEN:BBM ARB;:SOUR:GPRF:GEN:ARB:REP CONT;:SOUR:GPRF:GEN:LIST OPEN;:TRIG:GPRF:GEN:ARB:RETR ON;:TRIG:GPRF:GEN:ARB:AUT ON;*OPC?", true);
                ioTestSet.WriteString("SYST:ERR?", true);
                ioTestSet.WriteString("*CLS", true);
                ioTestSet.WriteString("CONFigure:FDCorrection:USAGe? RF1C", true);

                //Set frequency
                strCommand = "SOUR:GPRF:GEN1:RFS:FREQ " + frequency + " MHz;*OPC?";
                ioTestSet.WriteString(strCommand, true);
                ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT ON;*OPC?", true);
                Thread.Sleep(5000);
                
                do
                {
                    ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT?", true);
                    strReturn = ioTestSet.ReadString();
                }
                while (strReturn.Contains("OPEN") == true);

                //Set amplitude
                strCommand = "SOUR:GPRF:GEN1:RFS:LEV " + amplitude + " dBm;*OPC?";
                ioTestSet.WriteString(strCommand, true);
                bDTVON = true;
            }
            else
            {
                ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT OPEN;*OPC?", true);
                bDTVON = false;
            }

        }
        
        public void SetAgilExmDtvRssiTransmition(String frequency, String amplitude)
        {
            string strCommand = string.Empty;
            string strReturn = string.Empty;
            ioTestSet = new FormattedIO488();

            //EXM
            try
            {
                ResourceManager grm = new ResourceManager();
                ioTestSet.IO = (IMessage)grm.Open("AGILENT_EXT", AccessMode.NO_LOCK, 2000, "");
            }
            catch
            {
                ioTestSet.IO = null;
            }


            if ((ioTestSet != null) && (!bDTVON))
            {
                if ((amplitude == String.Empty) || (Convert.ToDouble(amplitude) > -10))
                {
                    throw new DtvGenException("Amplitude must be between -10 to -65 dBm");
                    return;
                }

                if ((frequency == "") || (Convert.ToDouble(frequency) > 803.143) || (Convert.ToDouble(frequency) < 473.143))
                {
                    throw new DtvGenException("Frequency must be between 473.143 to 803.143 Mhz.");
                    return;
                }

                ioTestSet.WriteString("*IDN?", true);
                ioTestSet.WriteString("FEED:RF:PORT:OUTP RFIO2;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("FEED:RF:PORT:INPUT RFIO2;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SOUR:AM:STAT OPEN;:SOUR:FM:STAT OPEN;:SOUR:PM:STAT OPEN;:OUTP:MOD ON;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("*CLS", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SYST:ERR?", true);
                ioTestSet.WriteString("SOUR:RAD:ARB:STAT OPEN;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SOUR:RAD:ARB:CAT?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SOUR:RAD:ARB:WAV 'ISDB-Tb_Digital_TV_HD.wfm';*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SYST:ERR?", true);
                ioTestSet.WriteString("SOUR:RAD:ARB:TRIG:TYPE CONT", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SOUR:RAD:ARB:TRIG:TYPE:CONT FREE", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SOUR:RAD:ARB:STAT ON;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("SYST:ERR?", true);
                ioTestSet.WriteString("CORR:CSET4:DESC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("CORR:CSET4:DEL;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("CORR:CSET4:DESC 'Rx RF IO2'", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("CORR:CSET4:STAT ON;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("CORR:CSET4:DATA:MERGE 587142857,1.75;*OPC?", true);
                Thread.Sleep(100);

                ioTestSet.WriteString("CORR:CSET4:COMM 'UUT_RF_CONN2_TO_TESTSET_ALT1_RF';*OPC?", true);
                Thread.Sleep(100);

                //Set frequency
                strCommand = "SOUR:FREQ " + frequency + " MHz;*OPC?";
                ioTestSet.WriteString(strCommand, true);
                Thread.Sleep(100);
                
                ioTestSet.WriteString("SOUR:GPRF:GEN1:STAT ON;*OPC?", true);
                Thread.Sleep(100);

                do
                {
                    ioTestSet.WriteString("OUTP?", true);
                    strReturn = ioTestSet.ReadString();
                }
                while (strReturn.Contains("OPEN") == true);

                strCommand = "SOUR:POW " + amplitude + " dBm;*OPC?";
                ioTestSet.WriteString(strCommand, true);

                bDTVON = true;
            }
            else
            {
                ioTestSet.WriteString("OUTP OPEN;*OPC?", true);
                bDTVON = false;
            }
        }
       
        
        private void CheckReturn(int retCode)
        {
            if (retCode != TestCoreMessages.SUCCESS)
                throw new DtvGenException("Error " + retCode + " returned.");
        }

        /// <summary>
        /// Class that represents DtvGen custom exceptions
        /// </summary>
        [Serializable]
        public class DtvGenException : Exception
        {
            public DtvGenException()
            { }

            public DtvGenException(string message)
                : base(message)
            { }

            public DtvGenException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}

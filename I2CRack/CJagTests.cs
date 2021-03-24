using System.Runtime.InteropServices;

namespace I2CRack
{
    public class CJagTests
    {
        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_CloseJig")]
        public static extern int CloseJig();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_OpenJig")]
        public static extern int OpenJig();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_InitPhone")]
        public static extern int InitPhone();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_InitPhoneByCloseJig")]
        public static extern int InitPhoneByCloseJig();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetUSB_PS2_5V")]
        public static extern int SetUSB_PS2_5V();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetBattery_PS1_4V")] // Patricio
        public static extern int SetBattery_PS1_4V();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetPowerSupply")] // Patricio
        public static extern int SetPowerSupply(int nChannel,
                                                [MarshalAs(UnmanagedType.LPStr)] string strVoltage,
                                                [MarshalAs(UnmanagedType.LPStr)] string strCurrent,
                                                [MarshalAs(UnmanagedType.LPStr)] string strState);
    }
}

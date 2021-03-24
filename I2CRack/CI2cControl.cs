using System.Runtime.InteropServices;

namespace I2CRack
{
    public class CI2cControl
    {
        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SendI2cCommand")]
        public static extern int SendI2cCommand([MarshalAs(UnmanagedType.LPStr)] string strI2cCommand);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_AcquireGPIBMutex")]
        public static extern int AcquireGPIBMutex();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_ReleaseGPIBMutex")]
        public static extern int ReleaseGPIBMutex();

    }
}

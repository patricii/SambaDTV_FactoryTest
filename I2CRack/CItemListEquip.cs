using System.Runtime.InteropServices;
using System.Text;

namespace I2CRack
{
    public class CItemListEquip
    {

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_InitializeEquipments")]
        public static extern int InitItemListEquip();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_LoadBZConfig")]
        public static extern void LoadBZConfig();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetMotorolaModel")]
        public static extern void SetMotorolaModel([MarshalAs(UnmanagedType.LPStr)] string strMotorolaModel);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetMotorolaModel")]
        public static extern void GetMotorolaModel(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_IsChecStatusEnable")]
        public static extern int IsChecStatusEnable();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_IsFqaVerify")]
        public static extern int IsFqaVerify();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_IsLogData")]
        public static extern int IsLogData();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_IsFqaGoldenGenerate")]
        public static extern int IsFqaGoldenGenerate();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_CheckStatus")]
        public static extern int CheckStatus();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_CheckStatusByStation")]
        public static extern int CheckStatusByStation([MarshalAs(UnmanagedType.LPStr)] string strStationId);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetTrackId")]
        public static extern void SetTrackId([MarshalAs(UnmanagedType.LPStr)] string strTrackId);

        [DllImport(@"C:\prod\bin\wrapperItemList.dll", EntryPoint = "InitItemList")]
        public static extern int InitItemList();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetMotModelfromModelFile")]
        public static extern string GetMotModelfromModelFile([MarshalAs(UnmanagedType.LPStr)] string strFactoryID, StringBuilder retorno);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetI2cSide")]
        public static extern void GetI2cSide(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetMotName")]
        public static extern void GetMotName(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetMQSDataFeedLogPath")]
        public static extern void GetMQSDataFeedLogPath(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetMQSDataTempLogPath")]
        public static extern void GetMQSDataTempLogPath(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetStationID")]
        public static extern void GetStationID(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetStationLine")]
        public static extern void GetStationLine(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetStationID")]
        public static extern void SetStationID([MarshalAs(UnmanagedType.LPStr)] string strStationId);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_IsEngMode")]
        public static extern int IsEngMode();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_CheckUserPass")]
        public static extern int CheckUserPass([MarshalAs(UnmanagedType.LPStr)] string strUserId,
                                                  [MarshalAs(UnmanagedType.LPStr)] string strPassWord);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetSofVerfromModelFile")]
        public static extern void GetSofVerfromModelFile(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetRFBandIdfromModelFile")]
        public static extern void GetRFBandIdfromModelFile(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetHdVerfromModelFile")]
        public static extern void GetHdVerfromModelFile(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetOdmName")]
        public static extern void GetOdmName(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetProjectCode")]
        public static extern void GetProjectCode(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetStationType")]
        public static extern void GetStationType(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetComPort")]
        public static extern void GetComPort(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetPS1GpibAddress")]
        public static extern void GetPS1GpibAddress(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetPS2GpibAddress")]
        public static extern void GetPS2GpibAddress(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "GetTEST_SETGpibAddress")]
        public static extern void GetTEST_SETGpibAddress(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetTestSetIPAddress")]
        public static extern void GetTestSetIPAddress(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetTestSetInst")]
        public static extern void GetTestSetInst(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetChCableLoss")]
        public static extern void GetChCableLoss([MarshalAs(UnmanagedType.LPStr)] string strBand,
                                                   [MarshalAs(UnmanagedType.LPStr)] string strChannel,
                                                   StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_IsOpenJigEnable")]
        public static extern int IsOpenJigEnable();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetFamily")]
        public static extern void GetFamily(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetTecnology")]
        public static extern void GetTecnology(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_Get10MhzSetting")]
        public static extern void Get10MhzSetting(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetPowerSupplyModel")]
        public static extern void GetPowerSupplyModel(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetApSwfromModelFile")]
        public static extern void GetApSwfromModelFile(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetBpSwfromModelFile")]
        public static extern void GetBpSwfromModelFile(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetFisrtCfgFilefromModelFile")]
        public static extern void GetFisrtCfgFilefromModelFile(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetFisrtIniFilefromModelFile")]
        public static extern void GetFisrtIniFilefromModelFile(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetComPortRight")]
        public static extern void GetComPortRight(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetComPortLeft")]
        public static extern void GetComPortLeft(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetKpLogPath")]
        public static extern void GetKpLogPath(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_UpdateCableLossDataOnMTKConfigFile")]
        public static extern int UpdateCableLossDataOnMTKConfigFile();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_UpdateCableLossDataOnMTKConfigFile_SUMO")] // Patricio
        public static extern int UpdateCableLossDataOnMTKConfigFile_SUMO();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_FQAVerifyAteRun")]
        public static extern int FQAVerifyAteRun();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_ReadDVM1Voltage")]
        public static extern double ReadDVM1Voltage();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetFreqCableLoss")]
        public static extern void GetFreqCableLoss([MarshalAs(UnmanagedType.LPStr)] string strPath,
                                                   [MarshalAs(UnmanagedType.LPStr)] string strFrequency,
                                                   [MarshalAs(UnmanagedType.LPStr)] string strLevel,
                                                   [MarshalAs(UnmanagedType.LPStr)] string strTxRx,
                                                   StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetAddTestLogFormat")] // FF
        public static extern void GetAddTestLogFormat(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetAddTestURL")] // FF
        public static extern void GetAddTestURL(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetFlowControlQueryURL")] // FF
        public static extern void GetFlowControlQueryURL(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_Output1StateON")] //BATT ON
        public static extern int Output1StateON();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_Output1StateOFF")] //BATT OFF
        public static extern int Output1StateOFF();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_Output2StateON")] //CHARGER ON
        public static extern int Output2StateON();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_Output2StateOFF")] //CHARGER OFF
        public static extern int Output2StateOFF();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_MeasPS2Current")]
        public static extern double MeasPS2Current();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_MeasPS1Current")]
        public static extern double MeasPS1Current();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetStandbyHiSpec")] //Spec no DataBase
        public static extern void GetStandbyHiSpec(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetStandbyLoSpec")] //Spec no DataBase
        public static extern void GetStandbyLoSpec(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetChargerLoSpec")] //Spec no DataBase
        public static extern void GetChargerLoSpec(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetChargerHiSpec")] //Spec no DataBase
        public static extern void GetChargerHiSpec(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetPoweroffHiSpec")] //Spec no DataBase
        public static extern void GetPoweroffHiSpec(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetPoweroffLoSpec")] //Spec no DataBase
        public static extern void GetPoweroffLoSpec(StringBuilder strbReturnData);


        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetDoTestFromDataBase")] // TOKEN CURRENT TESTS
        public static extern int GetDoTestFromDataBase([MarshalAs(UnmanagedType.LPStr)] string MeasCode);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetTestSetFirmwareVersion")] // versão no database
        public static extern void GetTestSetFirmwareVersion(StringBuilder strbReturnData);

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetRangeMAX")] //Set Range 2304 /2306
        public static extern int SetRangeMAX();

        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_SetRangeMIN")] //Set Range 2304 /2306
        public static extern int SetRangeMIN();


        [DllImport(@"C:\prod\bin\wrapper_Handler_Bz.dll", EntryPoint = "EXPORT_GetTestSpecFromDataBase")]
        public static extern double GetTestSpecFromDataBase([MarshalAs(UnmanagedType.LPStr)] string strMeasCode, [MarshalAs(UnmanagedType.LPStr)] string strSpecType);
    }


}

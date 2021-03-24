using System;
using System.Net.NetworkInformation;
using System.Text;
using TPWrapper.CheckStatusParameters;
using TPWrapper.LogParameters;
using System.Windows.Forms;

namespace I2CRack
{
    public class BzLogResult // Class log MQS Patricio
    {
        public string  TrackId { get; set; }

        TPWrapper.LogDataAcquisition logProcess = new TPWrapper.LogDataAcquisition();

        public StringBuilder strFinalResult = new StringBuilder();

        public int LoadLogResult(string strErrorMessage)  //Padrão MQS de log
        {
            strErrorMessage = string.Empty;
            int nStatus = 0;
            string strStationCode;
            string strStnId;
            string strStnLine;
            StringBuilder strStationId = new StringBuilder(56);
            StringBuilder strStationLine = new StringBuilder(56);
            StringBuilder strStationType = new StringBuilder(56);
            StringBuilder strMQSDataFeedLogPath = new StringBuilder(56);
            StringBuilder strMQSDataTempLogPath = new StringBuilder(56);
            StringBuilder strLogFormat = new StringBuilder(256);
            StringBuilder strServerURL = new StringBuilder(256);

            CItemListEquip.GetStationLine(strStationLine);
            CItemListEquip.GetStationType(strStationType);
            CItemListEquip.GetStationID(strStationId);
            CItemListEquip.GetMQSDataFeedLogPath(strMQSDataFeedLogPath);
            CItemListEquip.GetMQSDataTempLogPath(strMQSDataTempLogPath);
            CItemListEquip.GetAddTestLogFormat(strLogFormat);
            CItemListEquip.GetAddTestURL(strServerURL);

            strStnId = strStationId.ToString();
            strStnLine = strStationLine.ToString();
            strStationCode = strStnLine + "-" + strStnId;


            InitParameters parameters = new InitParameters();
            parameters.computerMacAddress = GetMyMACAddress();
            parameters.fixtureId = "*";
            parameters.processCode = strStationType.ToString();
            parameters.siteCode = "JAG";
            parameters.softwareReleaseVersion = "V1.0";
            parameters.stationCode = strStationCode;
            parameters.headerVersion = "TH4";
            parameters.testHeaderVersion = "TR1";
            parameters.logFormat = LogFileFormat.MQS;
            parameters.feedPath = strMQSDataFeedLogPath.ToString();
            parameters.temporaryPath = strMQSDataTempLogPath.ToString();

            if (CItemListEquip.IsLogData() == 0)  // Para salvar o log LOcal
                parameters.feedPath = @"C:SambaDtv\Prod\temp";

            if (strLogFormat.ToString().Equals("MQS"))   // Tipos de lOG ...FF
                parameters.logFormat = LogFileFormat.MQS;
            else if (strLogFormat.ToString().Equals("MQS_AND_HTTP_POST"))
                parameters.logFormat = LogFileFormat.MQS_AND_HTTP_POST;
            else if (strLogFormat.ToString().Equals("HTTP_POST"))
                parameters.logFormat = LogFileFormat.HTTP_POST;
            else
            {
                // TODO: need correction
                //MessageBox.Show("FUNCTION LoadLogResult() Error:\n\rLog Format not valid-->" + strLogFormat.ToString());
                //Application.Exit();
            }

            if (strLogFormat.ToString().Contains("HTTP"))
            {
                if (strServerURL.Length == 0)
                {
                    // TODO: need correction
                    //MessageBox.Show("FUNCTION LoadLogResult() Error:\n\rHTTP POST URL Not Valid-->" + strServerURL.ToString());
                    //Application.Exit();
                }

                parameters.serverUri = @strServerURL.ToString();  //HTTP JAG
                //parameters.serverUri = @"http://jagnt001.americas.ad.flextronics.com/FF_Http_AutoTester/default.aspx";
            }
            nStatus = logProcess.Init(parameters, out strErrorMessage);
            return nStatus;
        }

        public int StartLogResult(string strErrorMessage)
        {
            strErrorMessage = string.Empty;
            int nStatus = 0;

            StringBuilder strMotorolaModel = new StringBuilder(16);
            StringBuilder strMotorolaName = new StringBuilder(16);
            StringBuilder strTecnology = new StringBuilder(16);
            StringBuilder strFamily = new StringBuilder(16);

            CItemListEquip.GetMotName(strMotorolaName);
            CItemListEquip.GetMotorolaModel(strMotorolaModel);
            CItemListEquip.GetFamily(strFamily);
            CItemListEquip.GetTecnology(strTecnology);

            string strRecipe = strMotorolaName + "_BRD";

            StartRecipeParameters parameters = new StartRecipeParameters();
            parameters.equipmentId = "";
            parameters.family = strFamily.ToString();
            parameters.motorolaModel = strMotorolaModel.ToString();
            parameters.recipeName = strRecipe;
            parameters.shopOrder = "";
            parameters.softwareId = "";
            parameters.technology = strTecnology.ToString();
            parameters.XcvrNumber = "P1B";

            nStatus = logProcess.StartRecipe(parameters, out strErrorMessage);
            return nStatus;
        }

        public int AddLogResult(string strMeasCode, string strMeasDescription, string strTestResult,
                                         string strHighLimit, string strLowLimit, string strYHighLimit,
                                         string strYLowLimit, int nPassFail, string strUnitsstring, string strErrorMessage)
        {
            strErrorMessage = string.Empty;
            int nStatus = 0;

            EvalTestParameters parameters = new EvalTestParameters();
            parameters.attempts = 1;
            parameters.testErrorMessage = "";
            parameters.testFailCode = "";
            if (nPassFail == 0)
                parameters.testResult = TestResult.Pass;
            else
                parameters.testResult = TestResult.Fail;
            //parameters.testResult = TestResult.Pass;
            parameters.testValue = Convert.ToDouble(strTestResult);
            parameters.highLimit = Convert.ToDouble(strHighLimit);
            parameters.lowLimit = Convert.ToDouble(strLowLimit);
            parameters.testCode = "";//strMeasCode;
            parameters.testGroup = "Data_No_Stat_Analysis";
            parameters.testName = strMeasDescription;
            parameters.testSpecName = "";
            parameters.testSubGroup = "";
            parameters.testTextValue = "";
            parameters.retestFlag = 0;

            nStatus = logProcess.EvalTest(parameters, out strErrorMessage);
            return nStatus;
        }
        public int LogResult(string strPassFail, string strErrorMessage)
        {
            strErrorMessage = string.Empty;
            int nStatus = 0;
            EndRecipeParameters parameters = new EndRecipeParameters();
            //parameters.trackId = "N123456789";  // Trackid da unidade para log
            parameters.trackId = TrackId;
            if (strPassFail == "FAIL")
                parameters.masterTestResult = TestResult.Fail;
            else
                parameters.masterTestResult = TestResult.Pass;

            nStatus = logProcess.EndRecipe(parameters, out strErrorMessage);
            return nStatus;
        }


        public int AddTestResult(string strTestResult, String ErrorMessage)
        {
            int nPassFail = -1; //Pass = 0, Fail = 1
            int nStatus = -1;

            try
            {
                //Set Pass/Fail flag
                if (strTestResult.Contains("PASS"))
                    nPassFail = 0;
                else if (strTestResult.Contains("FAIL"))
                    nPassFail = 1;

                nStatus = AddLogResult("TESTE_1", "TESTE_1", "10", "20", "5", "20", "5", nPassFail, "--", ErrorMessage);
                nStatus = AddLogResult("TESTE_2", "TESTE_2", "500", "800", "200", "20", "5", nPassFail, "--", ErrorMessage);
                nStatus = AddLogResult("TESTE_3", "TESTE_3", "23", "30", "10", "20", "5", nPassFail, "--", ErrorMessage);
            }
            catch (Exception exc)
            {
                string str = exc.Message;
                nStatus = -1;
            }

            return nStatus;
        }

        // GetUnitInfo GPS Function Patricio

        public void POST()
        {

            if (CItemListEquip.IsChecStatusEnable() == 1)
            {
                int nStatus = -1;
                string errorMessage = string.Empty;

                string strStationCode;
                string strStnId;
                string strStnLine;
                StringBuilder strStationId = new StringBuilder(16);
                StringBuilder strStationLine = new StringBuilder(16);
                StringBuilder strStationType = new StringBuilder(16);
                StringBuilder strServerURL = new StringBuilder(256);

                CItemListEquip.GetStationLine(strStationLine);
                CItemListEquip.GetStationType(strStationType);
                CItemListEquip.GetStationID(strStationId);
                CItemListEquip.GetFlowControlQueryURL(strServerURL);

                strStnId = strStationId.ToString();
                strStnLine = strStationLine.ToString();
                strStationCode = strStnLine + "-" + strStnId;

                RequestParameters request = new RequestParameters();
                request.URI = @strServerURL.ToString();
                //request.URI = @"http://jagnt001.americas.ad.flextronics.com/FF_Http_AutoTester/default.aspx"; // FLEXFLOW
                request.orderNumber = "";
                request.stationCode = strStationCode;
                request.stationType = strStationType.ToString();
                request.testMachineId = "";
                //request.unitTrackId = "N123456789";  //10 numeros
                request.unitTrackId = TrackId;

                string responseString = string.Empty;

                nStatus = TPWrapper.GpsAcquisition.Request(request, out responseString, out errorMessage);

               // MessageBox.Show(request.ToString()); // to debug

                if (nStatus != 0)
                {
                    //MessageBox.Show(errorMessage);
                    throw new Exception(errorMessage);
                    //return;
                }
                else
                {
                    ResponseParameters responseParameters = TPWrapper.GpsAcquisition.GetParametersValuesFromStringResponse(responseString);

                    CJagLocalFucntions.SetCheckStatusResult("Init");
                    // Response
                    string batteryPartNumber = responseParameters.batteryPartNumber;
                    string boardKitNumber = responseParameters.boardKitNumber;
                    string cfcSvn = responseParameters.cfcSvn;
                    string endoPartNumber = responseParameters.endoPartNumber;
                    string errorMsg = responseParameters.errorMessage;
                    string facSvn = responseParameters.facSvn;
                    string fvcNumber = responseParameters.fvcNumber;
                    string orderNumber = responseParameters.orderNumber;
                    string permission = responseParameters.permission;
                    string salesModel = responseParameters.salesModel;
                    string transceiverNumber = responseParameters.transceiverNumber;
                    string unitTrackId = responseParameters.unitTrackId;

                    if (permission.Equals("0")) // OK
                    {
                        nStatus = 0;
                        CJagLocalFucntions.SetCheckStatusResult("PASS");
                    }
                    else if (permission.Equals("1"))
                    {
                        nStatus = -1;
                        //MessageBox.Show(errorMsg);
                        throw new Exception(errorMsg);
                        //return;

                    }
                    else
                    {
                        nStatus = -1;
                        //MessageBox.Show("Application does not know if the phone is OK to test");
                        throw new Exception("Application does not know if the phone is OK to test");
                        //return;
                    }
                }

                if (nStatus != 0)
                {
                    CJagLocalFucntions.SetCheckStatusResult("FAIL");
                    return;
                }
            }
            else  //Check status disable OK
            {
                CJagLocalFucntions.SetCheckStatusResult("PASS");
            }
        }


        private string GetMyMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String myMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (myMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    myMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return myMacAddress;
        }

    }       
}

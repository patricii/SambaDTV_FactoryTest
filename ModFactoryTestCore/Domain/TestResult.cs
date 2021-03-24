//using Newtonsoft.Json;
namespace ModFactoryTestCore.Domain
{
    public class TestResult
    {
        private string PathToFile;

        public string TrackIdNumber { get; set; }
        public string LogFileLocation { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string HightLimit { get; set; }
        public string LowLimit { get; set; }
        public string Y_HightLimit { get; set; }
        public string Y_LowLimit { get; set; }
        public string Result { get; set; }
        public string Units { get; set; }
        public string ErrorMessage { get; set; }

        public TestResult(string trackId, string logFileLocation,  string code, string description, string value, string hightLimit, string lowLimit, string y_hightLimit, string y_lowLimit, string result, string units, string error_message)
        {
            this.TrackIdNumber = trackId;
            this.LogFileLocation = logFileLocation;
            this.Code = code;
            this.Description = description;
            this.Value = value;
            this.HightLimit = hightLimit;
            this.LowLimit = lowLimit;
            this.Y_HightLimit = y_hightLimit;
            this.Y_LowLimit = y_lowLimit;
            this.Result = result;
            this.Units = units;
            this.ErrorMessage = error_message;
        }

        public TestResult()
        {

        }
        
        //public static TestResult ToObject(string json)
        //{
        //    return JsonConvert.DeserializeObject<TestResult>(json);
        //}

        //private string ToJson()
        //{
        //    return JsonConvert.SerializeObject(this);
        //}


        //public bool SaveTestResultToFile()
        //{
        //    bool retCode = false;
        //    string tmp = string.Empty;

        //    try
        //    {
        //        if (this.LogFileLocation.Contains("TS_XXXX.log"))
        //        {
        //            tmp = this.LogFileLocation.Replace("TS_XXXX", ("TR_" + this.Result));
        //            this.LogFileLocation = tmp;
        //            System.IO.File.WriteAllText(this.LogFileLocation, this.ToJson());
        //            retCode = true;
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        retCode = false;
        //        return retCode;
        //    }         
            
        //    return retCode;
        //}
    }
}

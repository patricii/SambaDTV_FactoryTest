using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;

namespace ModFactoryTestCore.Domain.Test
{
    public abstract class TestCaseBase
    {
        public enum TestEvaluateResult 
        {
            PASS = 0, FAIL = 1, BLOCKED, NOT_RUN 
        }

        public string format = "{0:0.0000000}";
        public static List<TestResult> TestResultList { get; set; }
        protected ResourceManager rm;
                
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpectedResults { get; set; }
        public TestEvaluateResult ResulTest { get; set; }
        public List<string> TestPoints { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Timeout { get; set; }
        
        //Abstract methods
        public abstract int Prepare();
        public abstract int Execute();
        public abstract int EvaluateResults();

        //Constructor
        public TestCaseBase()
        {
            rm = new ResourceManager("ModFactoryTestCore.en-US", Assembly.Load("ModFactoryTestCore"));
            TestPoints = new List<string>();
            TestResultList = new List<TestResult>();
        }


        internal static void SaveTestResultListToFile(string logName)
        {
            try
            {
                string myLogName = logName.Replace("TS", "TR");
                System.IO.File.WriteAllText(myLogName, JsonConvert.SerializeObject(TestResultList));
            }
            catch (Exception)
            {                
                throw;
            }
        }
        

        public static List<TestResult> ToObject(string json)
        {
            List<TestResult> listResult = null;

            try
            {
                listResult = JsonConvert.DeserializeObject<List<TestResult>>(json);
            }
            catch (Exception)
            {
                return null;
            }

            return listResult;
        }

    }
}

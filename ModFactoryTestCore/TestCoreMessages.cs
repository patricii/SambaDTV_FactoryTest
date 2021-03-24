using System.Collections.Generic;

namespace ModFactoryTestCore
{
    public static class TestCoreMessages
    {
        public enum TypeMessage
        {
            SUCCESS = 0,
            ERROR,
            WARNING,
            INFORMATION,
            UPDATE_DGV_TRACKID,
            CANCELED_BY_USER,
            REQUEST_USER_ACTION,
            START_CAMERA,
            GET_FRAME_LED_ON,
            GET_FRAME_LED_OFF,
            PROCESS_DETECTION,
            UPDATE_TEST_PERCENTUAL,
            STOP_CAMERA,
        }

        public enum StatusTest
        {
            PASS, 
            FAIL
        }

        public enum StationType
        {
            A, B, C, D //D is to debug test...
        }

        public static readonly int UNKNOW_ERROR = -1;
        public static readonly int SUCCESS = 0;
        public static readonly int ERROR = 1;
        public static readonly int INVALID_TRACKID_FORMAT = 2;
        public static readonly int FAIL_INITIALIZE_BZ_EQUIPAMENTS = 3;
        public static readonly int FILE_NOT_FOUND = 4;
        public static readonly int JIG_NOT_CLOSED = 5;
        public static readonly int JIG_CLOSED = 6;
        public static readonly int CANCELED_BY_USER = 7;
        public static readonly int CONFIG_FILE_PARAMETER_NOT_FOUND = 8;
        public static readonly int REACHED_TIMEOUT = 9;

        private static readonly Dictionary<int, string> mapErrorCode = new Dictionary<int, string>
        {
            {SUCCESS, "Operation executed with success!"},
            {ERROR, "Error."},
            {UNKNOW_ERROR, "Unknow error."},
            {INVALID_TRACKID_FORMAT, "Invalid trackId format."},
            {FAIL_INITIALIZE_BZ_EQUIPAMENTS, "Fail to initialize BZ Equipaments."},
            {FILE_NOT_FOUND, "File not found."},
            {JIG_NOT_CLOSED, "Jig not closed."},
            {JIG_CLOSED, "Jig closed."},
            {CANCELED_BY_USER, "Test canceled by user"},
            {CONFIG_FILE_PARAMETER_NOT_FOUND, "Config file parameter not found."},
            {REACHED_TIMEOUT, "Reached timeout."}
        };


        public static string ParseMessages(int codeMessage)
        {
            string retCode;

            if (!mapErrorCode.TryGetValue(codeMessage, out retCode))
                return "Error! Message not found !";

            return retCode;
        }

    }
}

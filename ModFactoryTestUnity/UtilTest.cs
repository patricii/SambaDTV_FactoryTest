using ModFactoryTestCore;
using System;
using System.Diagnostics;

namespace ModFactoryTestUnity
{
    public class UtilTest
    {
        public static int WriteTestSummary(TestCoreMessages.TypeMessage type, string message)
        {
            switch (type)
            {
                case TestCoreMessages.TypeMessage.ERROR:
                    //rTxtBox.BackColor = Color.DarkRed;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case TestCoreMessages.TypeMessage.WARNING:
                    //rTxtBox.BackColor = Color.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case TestCoreMessages.TypeMessage.SUCCESS:
                    //rTxtBox.BackColor = Color.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    break;
            }

            //rTxtBox.AppendText("\n" + "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] - " + message);
            Debug.WriteLine("\n" + "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] - " + message);
            return 0;
        }
    }
}

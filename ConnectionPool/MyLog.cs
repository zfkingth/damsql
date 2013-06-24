using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hammergo.ConnectionPool
{
    public class MyLog
    {
        private static StringBuilder sb = new StringBuilder(10000);

        private MyLog()
        {

        }

        public static void Log(string info)
        {
            sb.Append(info).Append(Environment.NewLine);
        }

        public static void NewLine()
        {
            sb.Append(Environment.NewLine);
        }

        public static void ClearLog()
        {
            sb.Clear();
        }

        public static string LogMessage
        {
            get
            {
                return sb.ToString();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonKit.Utilities
{
    public static class LogHelper
    {
        public static void WriteLog(string message)
        {
            System.IO.File.AppendAllText("output.log", $"{DateTime.Now} {message}\n");
        }
    }
}

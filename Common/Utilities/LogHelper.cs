using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class LogHelper
    {
        public static void WriteLog(string message)
        {
            var outputPath = AppSettings.GetSetting("OutputPath");

            System.IO.File.AppendAllText(outputPath, $"{DateTime.Now} {message}\n");
        }
    }
}

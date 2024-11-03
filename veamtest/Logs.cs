using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veamtest
{
    static class Logs
    {
        public static void FileWrite(string message, string logFilePath)
        {
            string formattedMessage = $"{DateTime.UtcNow.ToString(Config.TimeFormat)}: {message}";
            File.AppendAllText(logFilePath, formattedMessage + Environment.NewLine);
            Console.WriteLine(formattedMessage); // Logs to console as well
        }
    }
}

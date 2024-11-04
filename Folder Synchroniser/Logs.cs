using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchroniser
{
    static class Logs
    {
        /// <summary>
        /// Method responsible for writing to the log file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logFilePath"></param>
        public static void FileWrite(string message, string logFilePath)
        {
            string formattedMessage = $"{DateTime.UtcNow.ToString(Config.TimeFormat)}: {message}";
            File.AppendAllText(logFilePath, formattedMessage + Environment.NewLine);
        }
    }

}

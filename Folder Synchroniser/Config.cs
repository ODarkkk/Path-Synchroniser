using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchroniser
{
    /// <summary>
    /// Static configuration class containing constants used for validation and formatting in the application.
    /// </summary>
    public static class Config
    {
        public static readonly string[] ValidExtensions = { ".json", ".log", ".csv", ".txt", ".cef", ".syslog" };
        public static readonly string TimeFormat = "yyyy-MM-dd HH:mm:ss"; // 2024-04-25 05:07:08
    }

}

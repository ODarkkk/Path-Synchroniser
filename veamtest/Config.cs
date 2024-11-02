using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veamtest
{
    public static class Config
    {
       public static string[] ValidExtensions => [".json", ".log", ".csv", ".txt", ".cef", ".syslog"];
       public static string TimeFormat => "yyyy-MM-dd HH:mm:ss"; // 2024-04-25 05:07:08
    }
}

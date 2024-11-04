using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchroniser
{
    public static class VerifyInput
    {
        /// <summary>
        /// Verify if the input is valid, ensuring there are exactly 4 arguments,
        /// the correct argument types, send the arguments values to AppConfig and valid directories
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="ArgumentException"></exception>

        public static AppConfig ParseArguments(string[] args)
        {
            if (args.Length != 4)
                throw new ArgumentException("Usage: <sourcePath> <replicaPath> <syncIntervalSeconds> <logFilePath>");

            if (!int.TryParse(args[2], out int syncIntervalSeconds))
                throw new ArgumentException("The synchronization interval must be an integer.");

            return new AppConfig(args[0], args[1], syncIntervalSeconds, args[3]);
        }
    }

}

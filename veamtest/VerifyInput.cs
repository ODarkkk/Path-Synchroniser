using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace veamtest
{
    public static class VerifyInput
    {
        /// <summary>
        /// Verify if the input is valid, ensuring there are exactly 4 arguments,
        /// the correct argument types, and valid directories.
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="ArgumentException"></exception>

        public static void Check(string[] args)
        {
            try
            {
                Verify(args);
            }
            catch (DirectoryNotFoundException dirEx)
            {
                // Handle specific directory not found exception
                Console.WriteLine($"Directory error: {dirEx.Message}");
                Environment.Exit(1);
            }
            catch (ArgumentException argEx)
            {
                // Handle argument exceptions specifically
                Console.WriteLine($"Argument error: {argEx.Message}");
                Environment.Exit(1);          }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Environment.Exit(1);
            }
        }
        public static void Verify(string[] args)
        {
            if (args.Length != 4)
                throw new ArgumentException($"Usage: {Process.GetCurrentProcess().ProcessName} <sourcePath> <replicaPath> <syncIntervalSeconds> <logFilePath>");
            if (!Directory.Exists(args[0]))
                throw new DirectoryNotFoundException($"The first argument '{args[0]}' must be a valid directory path.");
            if (!Directory.Exists(args[1]))
                throw new DirectoryNotFoundException($"The second argument '{args[1]}' must be a valid directory path.");
            if (!int.TryParse(args[2], out _))
                throw new ArgumentException("The third argument must be an integer.");
            if (!File.Exists(args[3]) || !Config.ValidExtensions.Contains(Path.GetExtension(args[3])))
                throw new ArgumentException("The fourth argument must be a valid log file path with a supported extension.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchroniser
{
    /// <summary>
    /// Configuration class containing settings for folder synchronization.
    /// </summary>
    public class AppConfig
    {
        public string SourcePath { get; }
        public string ReplicaPath { get; }
        public int SyncIntervalSeconds { get; }
        public string LogFilePath { get; }

        /// <summary>
        /// Initializes a new instance of the AppConfig class with the specified settings.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="replicaPath"></param>
        /// <param name="syncIntervalSeconds"></param>
        /// <param name="logFilePath"></param>
        public AppConfig(string sourcePath, string replicaPath, int syncIntervalSeconds, string logFilePath)
        {
            SourcePath = sourcePath;
            ReplicaPath = replicaPath;
            SyncIntervalSeconds = syncIntervalSeconds;
            LogFilePath = logFilePath;

            // Validates the provided configuration settings
            Validate();
        }

        /// <summary>
        /// Validates the configuration settings to ensure all paths are valid and accessible.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException">
        /// Thrown when the source or replica directory does not exist.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the log file is invalid or has an unsupported extension.
        /// </exception>
        private void Validate()
        {
            if (!Directory.Exists(SourcePath))
                throw new DirectoryNotFoundException($"Source directory '{SourcePath}' does not exist.");

            if (!Directory.Exists(ReplicaPath))
                if (!Config.createReplicaFolder)
                    throw new DirectoryNotFoundException($"Replica directory '{ReplicaPath}' does not exist.");
                else
                {
                    // Creates the replica directory if it does not exist
                    Directory.CreateDirectory(ReplicaPath);
                    Logs.FileWrite($"Replica directory '{ReplicaPath}' created.", ReplicaPath);
                    Console.WriteLine("Replica path didn't existes. Creating a replica folder\t" + ReplicaPath);
                }

            if (!File.Exists(LogFilePath) || !Config.ValidExtensions.Contains(Path.GetExtension(LogFilePath)))
                if (!Config.createLogFile)
                    throw new ArgumentException($"Log file '{LogFilePath}' is invalid or has an unsupported extension.");
                else
                {
                    // Creates the log file if it does not exist
                    var logFilePath = Path.Combine(LogFilePath, LogFilePath) + Config.logFileExt;
                    File.Create(logFilePath);
                    Console.WriteLine("Log File didn't exits. Creating a log file\t" + LogFilePath);
                    Logs.FileWrite($"Log file '{logFilePath}' created.", LogFilePath);
                }
        }
    }
}

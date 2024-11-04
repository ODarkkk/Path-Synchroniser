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
                throw new DirectoryNotFoundException($"Replica directory '{ReplicaPath}' does not exist.");

            if (!File.Exists(LogFilePath) || !Config.ValidExtensions.Contains(Path.GetExtension(LogFilePath)))
                throw new ArgumentException($"Log file '{LogFilePath}' is invalid or has an unsupported extension.");
        }
    }
}

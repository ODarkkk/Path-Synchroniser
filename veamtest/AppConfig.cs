using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veamtest
{
    public class AppConfig
    {
        public string SourcePath { get; }
        public string ReplicaPath { get; }
        public int SyncIntervalSeconds { get; }
        public string LogFilePath { get; }
        public AppConfig(string sourcePath, string replicaPath, int syncIntervalSeconds, string logFilePath)
        {
            SourcePath = sourcePath;
            ReplicaPath = replicaPath;
            SyncIntervalSeconds = syncIntervalSeconds;
            LogFilePath = logFilePath;
            Validate();
        }

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

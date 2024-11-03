using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace veamtest
{
    class FolderSynchronizer
    {
        private readonly AppConfig appConfig;

        public FolderSynchronizer(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Synchronizing from {appConfig.SourcePath} to {appConfig.ReplicaPath}...");
                    SynchronizeFolders();
                    Thread.Sleep(appConfig.SyncIntervalSeconds * 1000);
                }
                catch (Exception ex)
                {
                    Logs.FileWrite($"Error during synchronization: {ex.Message}", appConfig.LogFilePath);
                }
            }
        }

        private void SynchronizeFolders()
        {
            var sourceFiles = Directory.GetFiles(appConfig.SourcePath, "*.*", SearchOption.AllDirectories);
            var sourceFilesSet = new HashSet<string>(sourceFiles.Select(f => Path.GetRelativePath(appConfig.SourcePath, f)));

            foreach (var sourceFile in sourceFiles)
            {
                // Compute the relative path to keep folder structure
                string relativePath = Path.GetRelativePath(appConfig.SourcePath, sourceFile);
                string replicaFile = Path.Combine(appConfig.ReplicaPath, relativePath);
                bool fileNeedsUpdate = !File.Exists(replicaFile) ||
                                 File.GetLastWriteTimeUtc(sourceFile) > File.GetLastWriteTimeUtc(replicaFile);
                if (fileNeedsUpdate)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(replicaFile));
                    File.Copy(sourceFile, replicaFile, true);

                    string action = File.Exists(replicaFile) ? "updated" : "copied";
                    Logs.FileWrite($"File '{relativePath}' was {action} in the replica directory.", appConfig.LogFilePath);
                }
            }
            var replicaFiles = Directory.GetFiles(appConfig.ReplicaPath, "*.*", SearchOption.AllDirectories);
            foreach (var replicaFile in replicaFiles)
            {
                string relativePath = Path.GetRelativePath(appConfig.ReplicaPath, replicaFile);
                if (!sourceFilesSet.Contains(relativePath))
                {
                    File.Delete(replicaFile);
                    Logs.FileWrite($"File '{relativePath}' was deleted from replica directory.", appConfig.LogFilePath);
                }
            }

        }

        private static bool FilesAreEqual(string filePath1, string filePath2)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream1 = File.OpenRead(filePath1))
                using (var stream2 = File.OpenRead(filePath2))
                {
                    byte[] hash1 = md5.ComputeHash(stream1);
                    byte[] hash2 = md5.ComputeHash(stream2);

                    return hash1.SequenceEqual(hash2);
                }
            }
        }
    }
}

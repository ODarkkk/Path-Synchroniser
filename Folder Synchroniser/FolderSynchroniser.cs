using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Synchroniser
{
    /// <summary>
    /// Class responsible for synchronizing files between source and replica folders.
    /// </summary>
    class FolderSynchroniser
    {
        /// <summary>
        /// Application configuration containing paths and sync settings.
        /// </summary>

        private readonly AppConfig appConfig;

        public FolderSynchroniser(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }

        /// <summary>
        /// Starts the folder synchronization process in an infinite loop with a defined interval.
        /// </summary>
        public void Start()
        {
            while (true)
            {
                try
                {
                    // Logs the start of synchronization and calls the main sync function.
                    Console.WriteLine($"Synchronizing from {appConfig.SourcePath} to {appConfig.ReplicaPath}...");
                    SynchroniseFolders();

                    // Waits for the defined interval before starting the next synchronization.
                    Thread.Sleep(appConfig.SyncIntervalSeconds * 1000);
                }
                catch (Exception ex)
                {
                    // Logs any errors encountered during synchronization.
                    Logs.FileWrite($"Error during synchronization: {ex.Message}", appConfig.LogFilePath);
                }
            }
        }

        /// <summary>
        /// Synchronizes files from the source folder to the replica folder.
        /// Copies new/updated files and deletes files not present in the source.
        /// </summary>
        private void SynchroniseFolders()
        {
            // Retrieves all files in the source folder and creates a set of their relative paths.
            var sourceFiles = Directory.GetFiles(appConfig.SourcePath, "*.*", SearchOption.AllDirectories);
            var sourceFilesSet = new HashSet<string>(sourceFiles.Select(f => Path.GetRelativePath(appConfig.SourcePath, f)));

            // Copies or updates files in the replica folder based on source folder contents.
            foreach (var sourceFile in sourceFiles)
            {
                // Computes the relative path to keep folder structure
                string relativePath = Path.GetRelativePath(appConfig.SourcePath, sourceFile);
                string replicaFile = Path.Combine(appConfig.ReplicaPath, relativePath);

                // Checks if the file needs to be copied or updated in the replica.
                bool fileNeedsUpdate = !File.Exists(replicaFile) || !FileVerificator.AreEqual(sourceFile, replicaFile);
                if (fileNeedsUpdate)
                {
                    // Ensures the directory structure exists and copies the file.
                    Directory.CreateDirectory(Path.GetDirectoryName(replicaFile));
                    File.Copy(sourceFile, replicaFile, true);

                    // Logs the copy/update action performed on the file.
                    string action = File.Exists(replicaFile) ? "updated" : "copied";
                    Logs.FileWrite($"File '{relativePath}' was {action} in the replica directory.", appConfig.LogFilePath);
                }
            }
            // Retrieves all files in the replica folder.
            var replicaFiles = Directory.GetFiles(appConfig.ReplicaPath, "*.*", SearchOption.AllDirectories);
            // Deletes files from the replica that are no longer present in the source folder.
            foreach (var replicaFile in replicaFiles)
            {
                string relativePath = Path.GetRelativePath(appConfig.ReplicaPath, replicaFile);
                if (!sourceFilesSet.Contains(relativePath))
                {
                    File.Delete(replicaFile);

                    // Logs the deletion of files that are no longer in the source folder.
                    Logs.FileWrite($"File '{relativePath}' was deleted from replica directory.", appConfig.LogFilePath);
                }
            }

        }
    }

}

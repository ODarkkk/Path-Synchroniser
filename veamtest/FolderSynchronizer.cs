using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veamtest
{
    class FolderSynchronizer
    {
        private readonly string sourceFolder;
        private readonly string replicaFolder;
        private readonly string logFilePath;
        private readonly int syncInterval;

        public string SourceFolder => sourceFolder;
        public string ReplicaFolder => replicaFolder;
        public string LogFilePath => logFilePath;
        public int SyncInterval => syncInterval;

        public FolderSynchronizer(string[] args)
        {
            sourceFolder = args[0];
            replicaFolder = args[1];
            logFilePath = args[3];
            syncInterval = int.Parse(args[2]);
        }


        public void Start()
        {
            while (true)
            {
                Console.WriteLine($"Synchronizing from {SourceFolder} to {ReplicaFolder}...");
                Thread.Sleep(SyncInterval);
            }
        }

        private void SynchronizeFolders()
        {
            // Get all files and directories in the source folder
            var sourceFiles = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories);
            var sourceDirs = Directory.GetDirectories(sourceFolder, "*", SearchOption.AllDirectories);

            foreach ( var sourceDir in sourceDirs ) 
            {
                var relative = Path.Get
        }
    }
}

using System.Linq.Expressions;

namespace veamtest
{
    static class Program
    {
        static void Main(string[] args)
        {
            VerifyInput.Check(args);
            //Console.WriteLine("Works");
            //Environment.Exit(0);
            //string sourcePath = args[0];
            //string targetPath = args[1];
            //int syncInterval = int.Parse(args[2]);
            //string logFilePath = args[3];
            FolderSynchronizer synchronizer = new FolderSynchronizer(args);
            synchronizer.Start();

        }
    }
}

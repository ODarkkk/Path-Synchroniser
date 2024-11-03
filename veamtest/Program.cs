using System.Linq.Expressions;

namespace veeamtest
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var appConfig = VerifyInput.ParseArguments(args);
                FolderSynchronizer synchronizer = new FolderSynchronizer(appConfig);
                synchronizer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

        }
    }
}

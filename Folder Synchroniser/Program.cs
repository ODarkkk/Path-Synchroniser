namespace Folder_Synchroniser
{
    static class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// The main method, which initializes configuration, creates the synchronizer, and starts synchronization.
            /// </summary>
            /// <param name="args">Command-line arguments for configuring the application.</param
            try
            {
                var appConfig = VerifyInput.ParseArguments(args);
                FolderSynchroniser synchronizer = new FolderSynchroniser(appConfig);
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
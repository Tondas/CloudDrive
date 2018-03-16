using CloudDrive.Common;
using CloudDrive.Core;
using CloudDrive.FileSystem.Windows;
using CloudDrive.Storage.AzureBlob;
using System;
using System.Threading.Tasks;

namespace CloudDrive.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi, stranger!");

            TempRun().Wait();
   
            Console.ReadLine();
        }

        public static async Task TempRun()
        {
            // TODO: Use something sophisticated
            var fileSystem = new WindowsFileSystem(AppSettings.Instance.RootDirectory);
            var cloudDrive = new AzureBlobStorage(AppSettings.Instance.AzureBlobConnectionString);
            var engine = new Engine(fileSystem, cloudDrive);
            await engine.Run();
        }


    }
}

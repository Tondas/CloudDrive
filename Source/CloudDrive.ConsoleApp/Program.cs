using CloudDrive.Common;
using CloudDrive.Core;
using CloudDrive.FileSystem.Windows;
using CloudDrive.Storage.AzureBlob;
using System;
using System.Threading.Tasks;

namespace CloudDrive.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hi, stranger!");

            TempRun().Wait();

            Console.ReadLine();
        }

        public static async Task TempRun()
        {
            // TODO: Use something sophisticated
            var fileSystem = new WindowsFileSystemClient(AppSettings.Instance.RootDirectory);
            var cloudDrive = new AzureBlobStorageClient(AppSettings.Instance.AzureBlobConnectionString);
            var engine = new Engine(fileSystem, cloudDrive);
            await engine.Run();
        }
    }
}
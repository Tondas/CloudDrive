using CloudDrive.Common;
using CloudDrive.Storage.AzureBlob;
using CloudDrive.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using CloudDrive.FileSystem.Windows;
using System;

namespace CloudDrive.Test
{
    [TestClass]
    public class WindowsFileSystemUT : BaseUT
    {
        private WindowsFileSystem fs { get; set; }

        [TestInitialize]
        public void Init()
        {
            fs = new WindowsFileSystem(AppSettings.Instance.RootDirectory);
        }

        // 

        [TestMethod]
        public void ReadRecursive()
        {
            var files = fs.ReadRecursive();
            //Console.WriteLine(files.ToString())

            Assert.IsTrue(true);
        }
    }
}

using CloudDrive.Common;
using CloudDrive.FileSystem.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudDrive.Test
{
    [TestClass]
    public class WindowsFileSystemUT : BaseUT
    {
        private WindowsFileSystemClient fs { get; set; }

        [TestInitialize]
        public void Init()
        {
            fs = new WindowsFileSystemClient(AppSettings.Instance.RootDirectory);
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
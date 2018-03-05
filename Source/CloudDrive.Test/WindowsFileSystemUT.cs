using CloudDrive.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudDrive.FileSystem.Windows;

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

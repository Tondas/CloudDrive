using CloudDrive.Common;
using CloudDrive.Storage.AzureBlob;
using CloudDrive.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace CloudDrive.Test
{
    [TestClass]
    public class AzureBlobStorageUT : BaseUT
    {
        [TestInitialize]
        public async Task Init()
        {
            await AzureBlobStorage.Instance.InitAsync(AppSettings.Instance.AzureBlobConnectionString);
        }

        // 

        [TestMethod]
        public async Task RootContainerCreationTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task SyncSpecificFolderTest()
        {
            string folderPath = "C:\\Temp\\Sync";

            if (Directory.Exists(folderPath))
            {
                foreach (var file in Directory.GetFiles(folderPath))
                {
                    var fileName = Path.GetFileName(file);
                    var fileData = File.ReadAllBytes(file);
                    await AzureBlobStorage.Instance.UploadAsync(fileData, fileName);
                }

                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }


    }
}

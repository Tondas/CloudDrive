using CloudDrive.Common;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudDrive.Provider.AzureStorage
{
    public class AzureBlobStorage
    {
        #region Members + Properties

        public string ConnectionString { get; set; }
        public string ContainerName => "CloudDrive";

        #endregion

        #region Singleton Constructor

        private static readonly Lazy<AzureBlobStorage> _instance = new Lazy<AzureBlobStorage>(() => new AzureBlobStorage(), true);
        public static AzureBlobStorage Instance { get { return _instance.Value; } }

        protected AzureBlobStorage() { }

        #endregion

        // Public Methods

        public void Init(string connectionString)
        {
            ConnectionString = connectionString;
        }


        private async Task<CloudFile> Upload(byte[] fileData, string fileName)
        {
            var blob = GetBlob(ContainerName, fileName);

            await blob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);

            return blob.ToCloudFile(fileData);
        }

        private async Task<CloudFile> Download(string fileName)
        {
            var blob = GetBlob(ContainerName, fileName);

            if (blob != null)
            {
                return blob.ToCloudFile();
            }
            return null;
        }

        private async Task<bool> ExistsAsync(string fileName)
        {
            return await GetBlob(ContainerName, fileName).ExistsAsync();
        }

        public async Task DeleteAsync(string fileName)
        {
            var blob = GetBlob(ContainerName, fileName);

            if (blob != null)
            {
                await blob.DeleteAsync();
            }

            return;
        }

        // Private Methods

        private CloudBlockBlob GetBlob(string containerName, string blobName)
        {
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            return container.GetBlockBlobReference(blobName);
        }
    }

    public static class AzureBlobStorageExtensions
    {
        public static CloudFile ToCloudFile(this CloudBlockBlob blob, byte[] fileData = null)
        {
            var file = new CloudFile()
            {
                Name = blob.Name,
                Uri = blob.Uri,
                Data = fileData
            };

            //using (var ms = new MemoryStream())
            //{
            //    await blob.DownloadToStreamAsync(ms);
            //    file.Data = ms.ToByteArray();
            //}

            return file;
        }
    }
}

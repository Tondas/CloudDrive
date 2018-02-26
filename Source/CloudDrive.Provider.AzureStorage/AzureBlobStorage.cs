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
        public string ContainerName => Constants.ContainerName;

        #endregion

        #region Singleton Constructor

        private static readonly Lazy<AzureBlobStorage> _instance = new Lazy<AzureBlobStorage>(() => new AzureBlobStorage(), true);
        public static AzureBlobStorage Instance { get { return _instance.Value; } }

        protected AzureBlobStorage() { }

        #endregion

        // Public Methods

        public async Task InitAsync(string connectionString)
        {
            ConnectionString = connectionString;
            await CreateIfNotExistsAsync(ContainerName);
        }

        public async Task<CloudFile> UploadAsync(byte[] fileData, string fileName)
        {
            var blob = GetBlob(ContainerName, fileName);

            await blob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);

            return blob.ToCloudFile(fileData);
        }

        public async Task<CloudFile> DownloadAsync(string fileName)
        {
            var blob = GetBlob(ContainerName, fileName);

            if (blob != null)
            {
                return await blob.ToCloudFile();
            }
            return null;
        }

        public async Task<bool> ExistsAsync(string fileName)
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
            var container = GetContainer(containerName);
            return container.GetBlockBlobReference(blobName);
        }

        private CloudBlobContainer GetContainer(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }

        private async Task<bool> CreateIfNotExistsAsync(string containerName)
        {
            var container = GetContainer(containerName);
            return await container.CreateIfNotExistsAsync();
        }   
    }

    public static class AzureBlobStorageExtensions
    {
        public static async Task<CloudFile> ToCloudFile(this CloudBlockBlob blob)
        {
            byte[] fileData;
            using (var ms = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(ms);
                fileData = ms.ToArray();
            }

            return blob.ToCloudFile(fileData);
        }

        public static CloudFile ToCloudFile(this CloudBlockBlob blob, byte[] fileData)
        {
            return new CloudFile()
            {
                Name = blob.Name,
                Uri = blob.Uri,
                Data = fileData
            };
        }
    }
}

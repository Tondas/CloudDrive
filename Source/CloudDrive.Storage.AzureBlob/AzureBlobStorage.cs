using CloudDrive.Common;
using CloudDrive.Common.Files;
using CloudDrive.Common.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CloudDrive.Storage.AzureBlob
{
    public class AzureBlobStorage : IStorage
    {
        #region Members + Properties

        public string ConnectionString { get; set; }
        public string ContainerName => Constants.ContainerName;

        #endregion

        public AzureBlobStorage() { }

        public AzureBlobStorage(string connectionString)
        {
            ConnectionString = connectionString;
        }

        // Public Methods

        public async Task InitAsync()
        {
            try
            {
                await CreateIfNotExistsAsync(ContainerName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Storage container '{ContainerName}' hasn't been initialized!", ex);
            }
        }

        //public async Task<List<CloudFile>> ReadRecursive()
        //{
        //    var result = new List<CloudFile>();
        //    var container = GetContainer(ContainerName);

        //    BlobContinuationToken blobContinuationToken = null;
        //    do
        //    {
        //        var results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);
        //        // Get the value of the continuation token returned by the listing call.
        //        blobContinuationToken = results.ContinuationToken;
        //        foreach (IListBlobItem item in results.Results)
        //        {
        //            Console.WriteLine(item.Uri);
        //        }
        //        blobContinuationToken = results.ContinuationToken;
        //    }
        //    while (blobContinuationToken != null);

        //    return result;
        //}

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
                byte[] fileData;
                using (var ms = new MemoryStream())
                {
                    await blob.DownloadToStreamAsync(ms);
                    fileData = ms.ToArray();
                }

                return blob.ToCloudFile(fileData);
            }
            return null;
        }

        public async Task<CloudFile> GetAttributesAsync(string fileName)
        {
            var blob = GetBlob(ContainerName, fileName);
            await blob.FetchAttributesAsync();
            return blob.ToCloudFile(null);
        }

        public async Task<bool> ExistsAsync(string fileName)
        {
            return await GetBlob(ContainerName, fileName).ExistsAsync();
        }

        public async Task<bool> DeleteAsync(string fileName)
        {
            var blob = GetBlob(ContainerName, fileName);

            if (blob != null)
            {
                await blob.DeleteAsync();
                return true;
            }

            return false;
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
        public static CloudFile ToCloudFile(this CloudBlockBlob blob, byte[] fileData)
        {
            return new CloudFile()
            {
                Name = blob.Name,
                Uri = blob.Uri,
                Data = fileData,
                ContentMD5 = blob.Properties.ContentMD5,
                ModifiedOn = blob.Properties.LastModified?.DateTime
            };
        }
    }
}

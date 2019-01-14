using CloudDrive.Domain.Model;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CloudDrive.Storage.AzureBlob
{
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
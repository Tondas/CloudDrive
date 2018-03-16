using CloudDrive.Common;
using CloudDrive.Common.Files;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDrive.Common.Interface
{
    public interface IStorage
    {
        Task InitAsync();


        //Task<List<CloudFile>> ReadRecursive();

        Task<CloudFile> UploadAsync(byte[] fileData, string fileName);

        Task<CloudFile> DownloadAsync(string fileName);

        Task<CloudFile> GetAttributesAsync(string fileName);


        Task<bool> ExistsAsync(string fileName);

        Task<bool> DeleteAsync(string fileName);
       
    }
}

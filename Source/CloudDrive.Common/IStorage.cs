using CloudDrive.Common;
using System.Threading.Tasks;

namespace CloudDrive.Common
{
    public interface IStorage
    {
        Task InitAsync(string connectionString);


        Task<CloudFile> UploadAsync(byte[] fileData, string fileName);

        Task<CloudFile> DownloadAsync(string fileName);

        Task<bool> ExistsAsync(string fileName);

        Task<bool> DeleteAsync(string fileName);
    }
}

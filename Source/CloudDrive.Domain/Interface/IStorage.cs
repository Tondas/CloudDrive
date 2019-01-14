using CloudDrive.Domain.Model;
using System.Threading.Tasks;

namespace CloudDrive.Domain.Interface
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
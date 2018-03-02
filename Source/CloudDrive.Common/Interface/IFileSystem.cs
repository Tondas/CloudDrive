using CloudDrive.Common;
using CloudDrive.Common.Files;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDrive.Common.Interface
{
    public interface IFileSystem
    {
        LocalFile Read(LocalFile file);

        List<LocalFile> ReadRecursive();

        bool Exists(LocalFile file);

        void Save(LocalFile file);

        void Delete(LocalFile file);

    }
}

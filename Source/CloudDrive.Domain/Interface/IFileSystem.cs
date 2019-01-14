using CloudDrive.Domain.Model;
using System.Collections.Generic;

namespace CloudDrive.Domain.Interface
{
    public interface IFileSystem
    {
        void Init();

        LocalFile Read(LocalFile file);

        List<LocalFile> ReadRecursive();

        bool Exists(LocalFile file);

        void Save(LocalFile file);

        void Delete(LocalFile file);
    }
}
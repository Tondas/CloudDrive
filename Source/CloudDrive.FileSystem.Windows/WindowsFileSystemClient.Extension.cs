using CloudDrive.Domain.Model;
using System.IO;

namespace CloudDrive.FileSystem.Windows
{
    public static class WindowsFileSystemExtension
    {
        public static LocalFile ToLocalFile(this string filePath)
        {
            var file = new LocalFile();
            file.Name = Path.GetFileName(filePath);
            file.NameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            if (Path.HasExtension(filePath))
            {
                file.Extension = Path.GetExtension(filePath);
            }
            file.Path = filePath;
            file.DirectoryPath = Path.GetDirectoryName(filePath);
            file.Directory = Path.GetFileName(file.DirectoryPath);

            // Times
            file.CreationTimeUtc = File.GetCreationTimeUtc(filePath);
            file.LastAccessTimeUtc = File.GetLastAccessTimeUtc(filePath);
            file.WriteTimeUtc = File.GetLastWriteTimeUtc(filePath);

            return file;
        }
    }
}
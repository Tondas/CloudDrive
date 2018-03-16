using CloudDrive.Common;
using CloudDrive.Common.Files;
using CloudDrive.Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace CloudDrive.FileSystem.Windows
{
    public class WindowsFileSystem : IFileSystem
    {
        #region Members + Properties

        private string RootDir { get; set; }

        #endregion

        public WindowsFileSystem() { }

        public WindowsFileSystem(string rootDirectory)
        {
            RootDir = rootDirectory;
        }

        // Public Methods

        public void Init()
        {
            if (!Directory.Exists(RootDir))
            {
                throw new Exception($"Directory '{RootDir}' doesn't exists!");
            }
        }

        public LocalFile Read(LocalFile file)
        {
            file.Data = File.ReadAllBytes(file.Path);
            return file;
        }

        public List<LocalFile> ReadRecursive()
        {         
            return ReadStructureRecursive(RootDir);
        }

        public bool Exists(LocalFile file)
        {
            return File.Exists(file.Path);
        }

        public void Save(LocalFile file)
        {
            if (File.Exists(file.Path))
            {
                File.Delete(file.Path);
            }

            File.WriteAllBytes(file.Path, file.Data);
        }

        public void Delete(LocalFile file)
        {
            File.Delete(file.Path);
        }

        // Private Methods

        /// <summary>
        /// Standard 'Tail' recursive functions
        /// </summary>
        /// <param name="dir">Current directory</param>
        private List<LocalFile> ReadStructureRecursive(string dir)
        {
            var files = new List<LocalFile>();

            // Read and process directory files
            var filePaths = Directory.GetFiles(dir);
            if (filePaths != null && filePaths.Length > 0)
            {
                foreach (var filePath in filePaths)
                {
                    files.Add(filePath.ToLocalFile());
                }
            }

            var dirPaths = Directory.GetDirectories(dir);
            if (dirPaths != null && dirPaths.Length > 0)
            {
                foreach (var dirPath in dirPaths)
                {
                    var innerFiles = ReadStructureRecursive(dirPath);
                    files.AddRange(innerFiles);
                }
            }
            return files;
        }
    }

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

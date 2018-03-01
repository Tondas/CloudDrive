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

        #region Singleton Constructor


        public WindowsFileSystem(string rootDirectory)
        {
            RootDir = rootDirectory;
        }

        #endregion

        // Public Methods

        public List<LocalFile> Read()
        {
            if (Directory.Exists(RootDir))
            {
                throw new Exception($"Directory '{RootDir}' doesn't exists!");
            }

            return ReadStructureRecursive(RootDir);
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
                    var file = new LocalFile();
                    file.Name = Path.GetFileName(filePath);
                    file.NameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                    if (Path.HasExtension(filePath))
                    {
                        file.Extension = Path.GetExtension(filePath);
                    }
                    file.Directory = dir;
                    file.Path = filePath;

                    // Times
                    file.CreationTimeUtc = File.GetCreationTimeUtc(filePath);
                    file.LastAccessTimeUtc = File.GetLastAccessTimeUtc(filePath);
                    file.WriteTimeUtc = File.GetLastWriteTimeUtc(filePath);

                    files.Add(file);
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
}

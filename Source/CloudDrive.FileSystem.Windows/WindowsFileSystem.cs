using CloudDrive.Common;
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

        public CloudFiles Read()
        {
            return ReadStructure();
        }


        // Private Methods

        private CloudFiles ReadStructure()
        {
            if (Directory.Exists(RootDir))
            {
                throw new Exception($"Directory '{RootDir}' doesn't exists!");
            }

            var result = new CloudFiles();

            // Read tree structure
            result.Files = ReadStructureRecursive(RootDir);

            return result;
        }

        /// <summary>
        /// Standard 'Tail' recursive functions
        /// </summary>
        /// <param name="dir">Current directory</param>
        private List<CloudFile> ReadStructureRecursive(string dir)
        {
            List<CloudFile> files = new List<CloudFile>(); 

            // Read and process directory files
            var filePaths = Directory.GetFiles(dir);
            if (filePaths != null && filePaths.Length > 0)
            {
                foreach (var filePath in filePaths)
                {
                    var file = new CloudFile();
                    file.Name = Path.GetFileName(filePath);
                    file.Directory = dir;
                    file.LocalPath = filePath;

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

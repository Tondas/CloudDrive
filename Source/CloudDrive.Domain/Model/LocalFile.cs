using CloudDrive.Common;
using System;

namespace CloudDrive.Domain.Model
{
    public class LocalFile
    {
        private static object filePath;

        public string Name { get; set; }
        public string NameWithoutExtension { get; set; }
        public string Extension { get; set; }

        public string Directory { get; set; }
        public string DirectoryPath { get; set; }
        public string Path { get; set; }
        public string StoragePath => Path.Replace(AppSettings.Instance.RootDirectoryInternal, "");

        public byte[] Data { get; set; }

        // Times

        public DateTime CreationTimeUtc { get; set; }
        public DateTime LastAccessTimeUtc { get; set; }
        public DateTime WriteTimeUtc { get; set; }
    }
}
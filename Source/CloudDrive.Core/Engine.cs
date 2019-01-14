using CloudDrive.Domain.Interface;
using System.Threading.Tasks;

namespace CloudDrive.Core
{
    public class Engine
    {
        #region Fields + Properties

        private IFileSystem FileSystem { get; set; }
        private IStorage Storage { get; set; }

        #endregion Fields + Properties

        // Ctor

        public Engine(IFileSystem fileSystem, IStorage storage)
        {
            FileSystem = fileSystem;
            Storage = storage;
        }

        // Public Methods

        public async Task Run()
        {
            // Init file system + storage container
            FileSystem.Init();
            await Storage.InitAsync();

            // Init state of engine
            // TODO

            // Scan all changes in drive folder
            await Sync();

            //var cloudFiles = await Storage.ReadRecursive();
            // TODO: compare

            // Attach file changes events
            // TODO
        }

        // Private Methods

        private async Task Sync()
        {
            // Load local files
            var localFiles = FileSystem.ReadRecursive();

            foreach (var localFile in localFiles)
            {
                var exist = await Storage.ExistsAsync(localFile.StoragePath);
                if (exist)
                {
                    // Download information about file from cloud
                    var cloudFile = await Storage.GetAttributesAsync(localFile.StoragePath);

                    // TODO: replace local or storage copy
                    FileSystem.Read(localFile);
                    cloudFile = await Storage.UploadAsync(localFile.Data, cloudFile.Name);
                }
                // Upload
                else
                {
                    FileSystem.Read(localFile);
                    var cloudFile = await Storage.UploadAsync(localFile.Data, localFile.StoragePath);
                }
            }
        }
    }
}
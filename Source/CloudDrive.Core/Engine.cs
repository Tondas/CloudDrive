using CloudDrive.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudDrive.Core
{
    public class Engine
    {
        #region Fields + Properties
        private IFileSystem FileSystem { get; set; }
        private IStorage Storage { get; set; }

        #endregion

        // Ctor

        public Engine(IFileSystem fileSystem, IStorage storage)
        {

        }

        // Public Methods

        public void Run()
        {
            // Init state of engine

            // Scan all changes in drive folder

            // Attache file changes events
        }

        // Private Methods

        private void ReadLocalDrive()
        {

        }
    }
}

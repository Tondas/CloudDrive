using System;
using System.Collections.Generic;
using System.Text;

namespace CloudDrive.Common
{
    public class CloudFile
    {
        // General Properties

        public string Name { get; set; }
        public string Directory { get; set; }
        public byte[] Data { get; set; }

        // Local Properties

        public string LocalPath { get; set; }


        // Remote Properties

        public Uri Uri { get; set; }
        

        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

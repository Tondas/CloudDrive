using System;
using System.Collections.Generic;
using System.Text;

namespace CloudDrive.Common.Files
{
    public class CloudFile
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public byte[] Data { get; set; }

        public Uri Uri { get; set; }
        

        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

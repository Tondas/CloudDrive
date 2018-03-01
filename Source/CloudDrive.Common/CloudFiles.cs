using System;
using System.Collections.Generic;
using System.Text;

namespace CloudDrive.Common
{
    public class CloudFiles
    {
        #region Members + Properties

        public List<CloudFile> Files { get; set; }

        #endregion

        public CloudFiles()
        {
            Files = new List<CloudFile>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(1024);
            foreach(var file in Files)
            {
                sb.AppendLine(file.LocalPath);
            }
            return sb.ToString();
        }
    }
}

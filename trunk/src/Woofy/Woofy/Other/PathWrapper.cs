using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Woofy.Other
{
    public class PathWrapper
    {
        public virtual string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public virtual string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public virtual string GetTempFileName()
        {
            return Path.GetTempFileName();
        }

        public virtual string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}

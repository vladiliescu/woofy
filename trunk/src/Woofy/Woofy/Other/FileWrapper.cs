using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Woofy.Other
{
    public class FileWrapper
    {
        /// <summary>
        /// Determines if the specified file exists.
        /// </summary>
        public virtual bool Exists(string path)
        {
            return File.Exists(path);
        }

        public virtual void Delete(string path)
        {
            File.Delete(path);
        }

        public virtual bool TryDelete(string path)
        {
            //yes, Delete doesn't throw when the file doesn't exist, but does throw when the directory doesn't exist
            if (!File.Exists(path))
                return false;

            File.Delete(path);
            return true;
        }

        public virtual void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }
    }
}

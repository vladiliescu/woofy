using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    /// <summary>
    /// Provides several utility methods for web paths, similar to the ones provided by the <see cref="System.IO.Path"/> class.
    /// </summary>
    public static class WebPath
    {
        private const string DirectorySeparator = "/";
        private static readonly int HttpLength = "http://".Length;
        private static readonly int HttpsLength = "https://".Length;

        /// <summary>
        /// Returns the directory part of a given web path.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="webPath">The web path from which to extract the directory.</param>
        /// <returns></returns>
        public static string GetDirectory(string webPath)
        {
            if (!webPath.StartsWith("http://") && !webPath.StartsWith("https://"))
                throw new ArgumentException("The path has to start with either http:// or https://.", "webPath");

            int lastSeparatorIndex = webPath.LastIndexOf(DirectorySeparator);
            if (lastSeparatorIndex == HttpLength - 1 || lastSeparatorIndex == HttpsLength - 1)
                return webPath;                

            return webPath.Substring(0, lastSeparatorIndex);
        }

        /// <summary>
        /// Combines two web paths.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="path1">The first path. Has to be a directory.</param>
        /// <param name="path2">The second path.</param>
        /// <returns></returns>
        public static string Combine(string path1, string path2)
        {
            string path1AsDirectory = GetDirectory(path1);
            if (!path1AsDirectory.Equals(path1) && !path1AsDirectory.Equals(path1.Substring(0, path1.Length - 1)))
                throw new ArgumentException("The first path must be a directory", "path1");

            if (path1.EndsWith(DirectorySeparator))
                path1 = path1.Substring(0, path1.Length - 1);

            if (path2.StartsWith(DirectorySeparator))
                path2 = path2.Substring(1);

            return string.Concat(path1, DirectorySeparator, path2);
        }
    }
}

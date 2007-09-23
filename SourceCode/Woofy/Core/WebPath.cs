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
            if (!IsAbsolute(webPath))
                throw new ArgumentException("The path has to start with either http:// or https://.", "webPath");

            int lastSeparatorIndex = webPath.LastIndexOf(DirectorySeparator);
            if (lastSeparatorIndex == HttpLength - 1 || lastSeparatorIndex == HttpsLength - 1)
                return webPath;

            int lastDotIndex = webPath.LastIndexOf(".");
            if (lastDotIndex > lastSeparatorIndex)      //if it points to a file
                return webPath.Substring(0, lastSeparatorIndex);

            if (lastSeparatorIndex == webPath.Length - 1)
                return webPath.Substring(0, lastSeparatorIndex);

            return webPath;
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

        /// <summary>
        /// Returns true if the sent path is absolute, false if it's relative.
        /// </summary>
        /// <param name="path">The path to be checked.</param>
        /// <returns></returns>
        public static bool IsAbsolute(string path)
        {
            string uppercasePath = path.ToUpper();
            return uppercasePath.StartsWith("HTTP://") || uppercasePath.StartsWith("HTTPS://");
        }

        /// <summary>
        /// Returns the root path of a web address.
        /// </summary>
        /// <param name="webPath"></param>
        /// <returns></returns>
        public static string GetRootPath(string webPath)
        {
            if (!IsAbsolute(webPath))
                throw new ArgumentException("The path has to start with either http:// or https://.", "webPath");
                
            int lastDotIndex = webPath.LastIndexOf(".");
            int separatorIndex = webPath.IndexOf(DirectorySeparator, lastDotIndex);

            if (separatorIndex == -1)
                return webPath;

            return webPath.Substring(0, separatorIndex);
        }
    }
}

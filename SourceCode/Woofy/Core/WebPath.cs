using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public static class WebPath
    {
        private const string DirectorySeparator = "/";

        //TODO: de testat (unit) + documentat
        public static string GetDirectory(string webPath)
        {
            //http://ttg.comicgenesis.com/1-main.html => http://ttg.comicgenesis.com/

            if (!webPath.StartsWith("http://"))
                throw new Exception();

            int lastSeparatorIndex = webPath.LastIndexOf(DirectorySeparator);
            if (lastSeparatorIndex == "http://".Length - 1)
                return webPath;                

            return webPath.Substring(0, lastSeparatorIndex);
        }

        //TODO: ar trebui sa arunce o exceptie daca incerc sa concatenez http://ttg.comicgenesis.com/1-main.html cu 1-main.html
        public static string Combine(string path1, string path2)
        {
            if (path1.EndsWith(DirectorySeparator))
                path1 = path1.Substring(0, path1.Length - 1);

            if (path2.StartsWith(DirectorySeparator))
                path2 = path2.Substring(1);

            return string.Concat(path1, DirectorySeparator, path2);
        }
    }
}

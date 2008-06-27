using System.IO;
using Woofy.Core;

namespace ComicDefListGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter writer = new StreamWriter("defsList.html"))
            {
                foreach (ComicDefinition comicDefinition in ComicDefinition.GetAvailableComicDefinitions())
                {
                    writer.WriteLine(string.Format("<li><a target=\"_blank\" rel=\"nofollow\" href=\"{0}\">{1}</a></li>",
                                                    comicDefinition.StartUrl, comicDefinition.FriendlyName));
                }
            }
        }
    }
}

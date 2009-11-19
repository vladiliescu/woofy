using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Entities
{
    public class ComicStrip
    {
        public long Id { get; set; }
        public Uri SourcePageAddress { get; set; }
        //TODO: ar trebui redenumit in PathOnDisk. Sau LocalPath
        public string FilePath { get; set; }
        public Comic Comic { get; private set; }

        public long ComicId { get; set; }

        public ComicStrip(Comic comic)
        {
            Comic = comic;
            ComicId = comic.Id;
        }

        //TODO: de sters
        public ComicStrip()
        {
        }
    }
}

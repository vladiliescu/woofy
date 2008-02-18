using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Entities
{
    public class ComicStrip
    {
        public long Id { get; set; }
        public Uri SourcePageAddress { get; set; }
        public string FilePath { get; set; }
        public Comic Comic { get; private set; }

        public ComicStrip(Comic comic)
        {
            Comic = comic;
        }
    }
}

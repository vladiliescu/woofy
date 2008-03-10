using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Woofy.Entities
{
    public class ComicStripCollection : Collection<ComicStrip>
    {
        public void CopyTo(ComicStripCollection stripCollection)
        {
            foreach (ComicStrip strip in this)
            {
                stripCollection.Add(strip);
            }
        }
    }
}

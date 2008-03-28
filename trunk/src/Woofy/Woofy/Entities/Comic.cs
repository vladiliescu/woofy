using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Woofy.Entities
{
    [DebuggerDisplay("{Name}")]
    public class Comic
    {

        public long Id { get; set; }
        
        /// <summary>
        /// The comic's name.
        /// </summary>
        public string Name { get; set; }        

        /// <summary>
        /// Whether the comic should be downloaded or not.
        /// </summary>
        public bool IsActive { get; set; }

        public int Priority { get; set; }

        /// <summary>
        /// Path to the comic's favicon.
        /// </summary>
        public string FaviconPath { get; set; }

        public ComicDefinition Definition { get; private set; }

        public void AssociateWithDefinition(ComicDefinition definition)
        {
            if (Definition == definition)
                return;

            Definition = definition;
            definition.AssociateWithComic(this);
        }
    }
}

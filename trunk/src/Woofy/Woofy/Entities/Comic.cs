using System;
using System.IO;
using System.Xml;

namespace Woofy.Entities
{
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

        /// <summary>
        /// Path to the comic's favicon.
        /// </summary>
        public string FaviconPath { get; set; }

        public ComicDefinition Definition { get; set; }
               

        #region Constructors

        public Comic()
        {
        }

        
        #endregion
    }
}

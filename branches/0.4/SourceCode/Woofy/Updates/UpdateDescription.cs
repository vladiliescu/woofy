using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Woofy.Updates
{
    /// <summary>
    /// Contains data about the available updates for Woofy and ComicPack.
    /// </summary>
    public class UpdateDescription
    {
        private ReleaseCollection woofy;
        /// <summary>
        /// Releases for the Woofy package.
        /// </summary>
        public ReleaseCollection Woofy
        {
            get { return this.woofy; }
        }

        private ReleaseCollection comicPack;
        /// <summary>
        /// Releases for the ComicPack package.
        /// </summary>
        public ReleaseCollection ComicPack
        {
            get { return this.comicPack; }
        }

        #region .ctor
        /// <summary>
        /// Initializes a new instance of <see cref="UpdateDescription"/> based on the given update file.
        /// </summary>
        /// <param name="updateFile">An update file containing the description.</param>
        public UpdateDescription(string updateFile)
            : this(new FileStream(updateFile, FileMode.Open))
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateDescription"/> based on the given stream.
        /// </summary>
        /// <param name="stream">A stream from which to load the update description.</param>
        public UpdateDescription(Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                reader.ReadToFollowing("woofy");
                this.woofy = new ReleaseCollection(reader.ReadSubtree());

                reader.ReadToFollowing("comicPack");
                this.comicPack = new ReleaseCollection(reader.ReadSubtree());
            }
        }        
        #endregion        
    }
}

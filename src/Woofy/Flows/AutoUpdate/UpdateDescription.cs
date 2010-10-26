using System.IO;
using System.Xml;

namespace Woofy.Flows.AutoUpdate
{
    /// <summary>
    /// Contains data about the available updates for Woofy.
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
            }
        }        
        #endregion        
    }
}

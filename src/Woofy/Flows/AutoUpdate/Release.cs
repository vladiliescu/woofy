using System;
using System.Xml;

namespace Woofy.Flows.AutoUpdate
{
    /// <summary>
    /// Contains data about a given release.
    /// </summary>
    public class Release
    {
        private string versionNumber;
        /// <summary>
        /// The version number of the release.
        /// </summary>
        public string VersionNumber
        {
            get { return versionNumber; }
        }

        private string downloadAddress;
        /// <summary>
        /// Gets the download address for this version.
        /// </summary>
        public string DownloadAddress
        {
            get { return downloadAddress; }
        }

        private DateTime? releaseDate;
        /// <summary>
        /// Gets the release date for this version.
        /// </summary>
        public DateTime? ReleaseDate
        {
            get { return releaseDate; }
        }

        private int size;
        public int Size
        {
            get { return size; }
        }


        private ChangeCollection changes;
        /// <summary>
        /// A list of changes in this release.
        /// </summary>
        public ChangeCollection Changes
        {
            get { return this.changes; }
        }

        #region .ctor
        /// <summary>
        /// Initializes a new instance of <see cref="Release"/>, based on a given <see cref="XmlReader"/>.
        /// </summary>
        public Release(XmlReader reader)
        {
            reader.Read();
            this.versionNumber = reader.GetAttribute("versionNumber");
            this.downloadAddress = reader.GetAttribute("downloadAddress");
            this.size = int.Parse(reader.GetAttribute("size"));
            string releaseDateString = reader.GetAttribute("releaseDate");            
            DateTime tempReleaseDate;
            if (DateTime.TryParseExact(releaseDateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out tempReleaseDate))
                this.releaseDate = tempReleaseDate;
            else
                this.releaseDate = null;

            this.changes = new ChangeCollection(reader.ReadSubtree());
        }
        #endregion

        #region Public Methods
        public bool IsNewerThanVersion(string versionNumber)
        {
            if (string.Compare(this.versionNumber, versionNumber, StringComparison.OrdinalIgnoreCase) > 0)
                return true;
            else
                return false;
        }
        #endregion
    }    

}

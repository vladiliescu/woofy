using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Woofy.Updates
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
            this.changes = new ChangeCollection(reader.ReadSubtree());
        }
        #endregion
    }    

}

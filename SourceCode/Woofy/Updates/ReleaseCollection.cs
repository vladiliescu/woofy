using System;
using System.Collections;
using System.Xml;

namespace Woofy.Updates
{
    /// <summary>
    /// Contains a collection of releases for a given package.
    /// </summary>
    public class ReleaseCollection : ReadOnlyCollectionBase
    {
        #region .ctor

        /// <summary>
        /// Initializes a new instance of <see cref="ReleaseCollection"/>, based on a given <see cref="XmlReader"/>.
        /// </summary>
        public ReleaseCollection(XmlReader reader)
        {
            while (reader.ReadToFollowing("release"))
            {
                base.InnerList.Add(new Release(reader.ReadSubtree()));
            }
        }

        #endregion

        #region Indexers
        /// <summary>
        /// Gets the element with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        public Release this[int index]
        {
            get { return (Release)base.InnerList[index]; }
        }

        public Release this[string versionNumber]
        {
            get
            {
                foreach (Release release in base.InnerList)
                {
                    if (release.VersionNumber.Equals(versionNumber, StringComparison.OrdinalIgnoreCase))
                        return release;
                }

                return null;
            }
        }
        #endregion
    }
}

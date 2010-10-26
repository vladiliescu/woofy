using System.Collections;
using System.Xml;

namespace Woofy.Flows.AutoUpdate
{
    /// <summary>
    /// Contains a list of changes for a given release.
    /// </summary>
    public class ChangeCollection : ReadOnlyCollectionBase
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of <see cref="Changes"/>, based on a given <see cref="XmlReader"/>.
        /// </summary>
        public ChangeCollection(XmlReader reader)
        {
            reader.ReadToFollowing("change");
            while (reader.Name == "change")
            {
                base.InnerList.Add(reader.ReadElementContentAsString());
            }
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets the element with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        public string this[int index]
        {
            get { return (string)base.InnerList[index]; }
        }
        #endregion
    }    
}

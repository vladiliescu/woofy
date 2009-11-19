using System.ComponentModel;
using System.Diagnostics;
using Woofy.Lookups;
using Woofy.Other;

namespace Woofy.Entities
{
    [DebuggerDisplay("{Name}")]
    public class Comic
    {
        private readonly PathWrapper _path = new PathWrapper();

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
        /// Gets the path to the comic's icon.
        /// </summary>
        public string IconPath { get; private set; }

        private DownloadState _downloadState = DownloadState.Pending;
        public DownloadState DownloadState
        {
            get
            {
                return _downloadState;
            }
            set
            {
                switch (value)
                {
                    case DownloadState.Pending:
                        IconPath = _path.GetFaviconPath("pending.png");
                        break;
                    case DownloadState.Downloading:
                        IconPath = _path.GetFaviconPath("downloading.png");
                        break;
                    case DownloadState.Finished:
                        IconPath = _path.GetFaviconPath("blank.png");
                        break;
                    default:
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(DownloadState));
                }
                _downloadState = value;
            }
        }

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

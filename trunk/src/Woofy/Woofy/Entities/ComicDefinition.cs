using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Woofy.Entities
{
    [DebuggerDisplay("{Comic.Name}, {HomePageAddress}")]
    public class ComicDefinition
    {
        /// <summary>
        /// Gets the comic's home page.
        /// </summary>
        public Uri HomePageAddress { get; set; }
        /// <summary>
        /// Gets the url of the comic's first issue.
        /// </summary>
        public Uri FirstStripAddress { get; set; }
        /// <summary>
        /// Gets the regular expression that will find the strip in a given page.
        /// </summary>
        public string StripRegex { get; set; }
        /// <summary>
        /// Gets the regular expression finding the url for the next page.
        /// </summary>
        public string NextIssueRegex { get; set; }
        /// <summary>
        /// Gets the regular expression for finding the url to the newest page in the home page. Can be null.
        /// </summary>
        public string LatestIssueRegex { get; set; }
        /// <summary>
        /// Gets the comic definition's author.
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Gets the comic definition's author email.
        /// </summary>
        public string AuthorEmail { get; set; }
        /// <summary>
        /// Specifies whether the comic definition allows missing strips in a page or not.
        /// </summary>
        public bool AllowMissingStrips { get; set; }
        /// <summary>
        /// Specifies whether the comic definition allows multiple strips in a single page or not.
        /// </summary>
        [Obsolete("I should allow the use of multiple strips by default. This means that I shouldn't use the strip's name to know when to stop downloading, but the page's url.")]
        public bool AllowMultipleStrips { get; set; }
        /// <summary>
        /// Gets the name of the definition file describing the comic.
        /// </summary>
        public string SourceFileName { get; set; }

        public bool HasSourceFile
        {
            get { return !string.IsNullOrEmpty(SourceFileName); }
        }

        public Comic Comic { get; private set; }

        public void AssociateWithComic(Comic comic)
        {
            if (Comic == comic)
                return;

            Comic = comic;
            comic.AssociateWithDefinition(this);
        }
    }
}

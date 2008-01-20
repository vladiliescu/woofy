using System;
using System.IO;
using System.Xml;

namespace Woofy.Entities
{
    public class Comic
    {
        /// <summary>
        /// The comic's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the comic's home page.
        /// </summary>
        public Uri HomePageUrl { get; set; }
        /// <summary>
        /// Gets the url of the comic's first issue.
        /// </summary>
        public Uri FirstStripUrl { get; set; }
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
        public string DefinitionAuthor { get; set; }
        /// <summary>
        /// Gets the comic definition's author email.
        /// </summary>
        public string DefinitionAuthorEmail { get; set; }
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
        public string DefinitionFileName { get; set; }

        /// <summary>
        /// Whether the comic should be downloaded or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Path to the comic's favicon.
        /// </summary>
        public string FaviconPath { get; set; }
               

        #region Constructors

        public Comic()
        {
        }

        
        #endregion
    }
}

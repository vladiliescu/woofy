using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;
using Woofy.Settings;

namespace Woofy.Core
{
    public class ComicDefinition
    {
        #region Public Properties

        /// <summary>
        /// Gets the comic's base url.
        /// </summary>
        public string StartUrl { get; private set; }

        public bool FailedToInitialize { get; private set; }

        /// <summary>
        /// Gets the url of the comic's first issue.
        /// </summary>
        public string FirstIssue { get; private set; }

        /// <summary>
        /// Gets the regular expression that will find the comic.
        /// </summary>
        public string ComicRegex { get; private set; }

        /// <summary>
        /// Gets the regular expression that will find the back button.
        /// </summary>
        public string BackButtonRegex { get; private set; }

        /// <summary>
        /// Gets the comic info's friendly name.
        /// </summary>
        public string FriendlyName { get; private set; }

        /// <summary>
        /// Gets the regular expression that matches the link to the latest comic page. Can be null.
        /// </summary>
        public string LatestPageRegex { get; private set; }

        /// <summary>
        /// Gets the comic definition's author email.
        /// </summary>
        public string AuthorEmail { get; private set; }

        /// <summary>
        /// Gets the comic definition's author.
        /// </summary>
        public string Author { get; private set; }

        private bool _allowMissingStrips;
        /// <summary>
        /// Specifies whether the comic definition allows missing strips in a page or not.
        /// </summary>
        public bool AllowMissingStrips
        {
            get { return _allowMissingStrips; }
        }

        private bool _allowMultipleStrips;
        /// <summary>
        /// Specifies whether the comic definition allows multiple strips in a single page or not.
        /// </summary>
        public bool AllowMultipleStrips
        {
            get { return _allowMultipleStrips; }
        }

        /// <summary>
        /// Returns the root path that should be combined with relative content.
        /// </summary>
        public string RootUrl { get; private set; }

        public string ComicInfoFile { get; private set; }

        public Collection<Capture> Captures { get; private set; }

        public string RenamePattern { get; private set; }

        #endregion

        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ComicDefinition"/> class.
        /// </summary>
        /// <param name="comicInfoStream">Stream containing the data necessary to create a new instance.</param>
        public ComicDefinition(Stream comicInfoStream)
        {
            var doc = new XmlDocument();
            doc.Load(comicInfoStream);
            var comicInfo = doc.SelectSingleNode("comicInfo");

            FriendlyName = comicInfo.Attributes["friendlyName"].Value;
            Author = comicInfo.Attributes["author"] == null ? null : comicInfo.Attributes["author"].Value;
            AuthorEmail = comicInfo.Attributes["authorEmail"] == null ? null : comicInfo.Attributes["authorEmail"].Value;
            var allowMissingStrips = comicInfo.Attributes["allowMissingStrips"] == null ? null : comicInfo.Attributes["allowMissingStrips"].Value;
            var allowMultipleStrips = comicInfo.Attributes["allowMultipleStrips"] == null ? null : comicInfo.Attributes["allowMultipleStrips"].Value;
            bool.TryParse(allowMissingStrips, out _allowMissingStrips);
            bool.TryParse(allowMultipleStrips, out _allowMultipleStrips);
            StartUrl = GetInnerText(comicInfo, "startUrl");
            ComicRegex = GetInnerText(comicInfo, "comicRegex");
            BackButtonRegex = GetInnerText(comicInfo, "backButtonRegex");
            FirstIssue = GetInnerText(comicInfo, "firstIssue");
            LatestPageRegex = GetInnerText(comicInfo, "latestPageRegex");
            RootUrl = GetInnerText(comicInfo, "rootUrl");
            RenamePattern = GetInnerText(comicInfo, "renamePattern");

            Captures = new Collection<Capture>();

			foreach (XmlNode captureNode in comicInfo.SelectNodes("captures/capture"))
            {
                Captures.Add(BuildCapture(captureNode));
            }
        }

    	private Capture BuildCapture(XmlNode captureNode)
    	{
			if (captureNode.Attributes["target"] == null)
				return new Capture(captureNode.Attributes["name"].Value, captureNode.InnerText);

			var target = captureNode.Attributes["target"].Value;
			if (target.Trim().ToUpper().Equals("URL"))
				return new Capture(captureNode.Attributes["name"].Value, captureNode.InnerText, CaptureTarget.Url);
    		
			return new Capture(captureNode.Attributes["name"].Value, captureNode.InnerText, CaptureTarget.Body);
    	}

    	private string GetInnerText(XmlNode comicInfo, string xpath)
        {
            var node = comicInfo.SelectSingleNode(xpath);
            return node == null ? null : node.InnerText;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicDefinition"/> class.
        /// </summary>
        /// <param name="comicInfoFile">Path to an xml file containing the data necessary to create a new instance.</param>
        public ComicDefinition(string comicInfoFile)
            : this (new FileStream(comicInfoFile, FileMode.Open, FileAccess.Read))
        {
            ComicInfoFile = comicInfoFile;
        }

        public ComicDefinition()
        {
        }

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Returns the available comic info files.
        /// </summary>
        public static ComicDefinition[] GetAvailableComicDefinitions()
        {
            var availableComicInfos = new List<ComicDefinition>();

            foreach (var comicInfoFile in Directory.GetFiles(AppSettings.ComicDefinitionsFolder, "*.xml"))
            {
                ComicDefinition definition;
                try
                { 
                    definition = new ComicDefinition(comicInfoFile);
                }
                catch (Exception ex)
                {
                    Logger.LogException("Error initializing definition " + comicInfoFile, ex);

                    definition = new ComicDefinition {
                        ComicInfoFile = comicInfoFile,
                        FriendlyName = string.Format("--- Failed to initialize definition {0} ---", Path.GetFileName(comicInfoFile)),
                        FailedToInitialize = true
                    };
                }

                availableComicInfos.Add(definition);
            }

            return availableComicInfos.ToArray();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return FriendlyName;
        }
        #endregion
    }
}

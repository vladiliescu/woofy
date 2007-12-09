using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.IO;

using Woofy.Exceptions;
using Woofy.Settings;

namespace Woofy.Core
{
    public class ComicDefinition
    {
        #region Public Properties
        private string _startUrl;
        /// <summary>
        /// Gets the comic's base url.
        /// </summary>
        public string StartUrl
        {
            get { return _startUrl; }
        }

        private string _firstIssue;
        /// <summary>
        /// Gets the url of the comic's first issue.
        /// </summary>
        public string FirstIssue
        {
            get { return _firstIssue; }
        }

        private string _comicRegex;
        /// <summary>
        /// Gets the regular expression that will find the comic.
        /// </summary>
        public string ComicRegex
        {
            get { return _comicRegex; }
        }

        private string _backButtonRegex;
        /// <summary>
        /// Gets the regular expression that will find the back button.
        /// </summary>
        public string BackButtonRegex
        {
            get { return _backButtonRegex; }
        }

        private string _friendlyName;
        /// <summary>
        /// Gets the comic info's friendly name.
        /// </summary>
        public string FriendlyName
        {
            get { return _friendlyName; }
        }

        private string _latestPageRegex;
        /// <summary>
        /// Gets the regular expression that matches the link to the latest comic page. Can be null.
        /// </summary>
        public string LatestPageRegex
        {
            get { return _latestPageRegex; }
        }

        private string _authorEmail;
        /// <summary>
        /// Gets the comic definition's author email.
        /// </summary>
        public string AuthorEmail
        {
            get { return _authorEmail; }
        }

        private string _author;
        /// <summary>
        /// Gets the comic definition's author.
        /// </summary>
        public string Author
        {
            get { return _author; }
        }

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

        private string rootUrl;
        /// <summary>
        /// Returns the root path that should be combined with relative content.
        /// </summary>
        public string RootUrl
        {
            get { return rootUrl; }
        }


        private string _comicInfoFile;
        public string ComicInfoFile
        {
            get { return _comicInfoFile; }
        }

        #endregion

        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ComicInfo"/> class.
        /// </summary>
        /// <param name="comicInfoStream">Stream containing the data necessary to create a new instance.</param>
        public ComicDefinition(Stream comicInfoStream)
        {
            //XmlReaderSettings readerSettings = new XmlReaderSettings();
            //readerSettings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(comicInfoStream))
            {
                reader.Read();  //<?xml..
                reader.Read();  //Whitespace..
                reader.Read();  //<comicInfo..
                _friendlyName = reader.GetAttribute("friendlyName");
                _author = reader.GetAttribute("author");
                _authorEmail = reader.GetAttribute("authorEmail");

                string allowMissingStrips = reader.GetAttribute("allowMissingStrips");
                string allowMultipleStrips = reader.GetAttribute("allowMultipleStrips");
                bool.TryParse(allowMissingStrips, out _allowMissingStrips);
                bool.TryParse(allowMultipleStrips, out _allowMultipleStrips);

                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "startUrl":
                            _startUrl = reader.ReadElementContentAsString();
                            break;
                        case "comicRegex":
                            _comicRegex = reader.ReadElementContentAsString();
                            break;
                        case "backButtonRegex":
                            _backButtonRegex = reader.ReadElementContentAsString();
                            break;
                        case "firstIssue":
                            _firstIssue = reader.ReadElementContentAsString();
                            break;
                        case "latestPageRegex":
                            _latestPageRegex = reader.ReadElementContentAsString();
                            break;
                        case "rootUrl":
                            this.rootUrl = reader.ReadElementContentAsString();
                            break;

                    }
                }
            }

            if (string.IsNullOrEmpty(_friendlyName))
                throw new MissingFriendlyNameException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicDefinition"/> class.
        /// </summary>
        /// <param name="comicInfoFile">Path to an xml file containing the data necessary to create a new instance.</param>
        public ComicDefinition(string comicInfoFile)
            : this (new FileStream(comicInfoFile, FileMode.Open, FileAccess.Read))
        {
            _comicInfoFile = comicInfoFile;
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Returns the available comic info files.
        /// </summary>
        public static ComicDefinition[] GetAvailableComicDefinitions()
        {
            List<ComicDefinition> availableComicInfos = new List<ComicDefinition>();

            foreach (string comicInfoFile in Directory.GetFiles(ApplicationSettings.ComicDefinitionsFolder, "*.xml"))
            {
                availableComicInfos.Add(new ComicDefinition(comicInfoFile));
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

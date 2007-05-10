using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using Woofy.Exceptions;
using Woofy.Properties;

namespace Woofy.Core
{
    public class ComicInfo
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
        /// <param name="comicInfoFile">Xml file containing the data necessary to create a new ComicInfo instance.</param>
        public ComicInfo(string comicInfoFile)
        {
            _comicInfoFile = comicInfoFile;

            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(comicInfoFile, readerSettings))
            {
                reader.Read();  //<?xml..
                reader.Read();  //<comicInfo..
                _friendlyName = reader.GetAttribute("friendlyName");
                reader.Read();  //<startUrl..

                while (reader.NodeType != XmlNodeType.EndElement)
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
                    }
                }
            }

            if (string.IsNullOrEmpty(_friendlyName))
                throw new MissingFriendlyNameException();
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Returns the available comic info files.
        /// </summary>
        public static ComicInfo[] GetAvailableComicInfos()
        {
            List<ComicInfo> availableComicInfos = new List<ComicInfo>();

            string comicInfosFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Settings.Default.ComicInfosFolderName);
            foreach (string comicInfoFile in Directory.GetFiles(comicInfosFolder, "*.xml"))
            {
                availableComicInfos.Add(new ComicInfo(comicInfoFile));
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

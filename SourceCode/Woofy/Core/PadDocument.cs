using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Woofy.Core
{
    /// <summary>
    /// Represents a PAD document.
    /// </summary>
    public class PadDocument
    {
        #region Properties
        private string _padFilePath;
        /// <summary>
        /// Path to the wrapped PAD file.
        /// </summary>
        public string PadFilePath
        {
            get { return _padFilePath; }
        }

        private Version _version;
        /// <summary>
        /// Program's version.
        /// </summary>
        public Version Version
        {
            get { return _version; }
        }

        private DateTime _programReleaseDate;
        /// <summary>
        /// Program's release date.
        /// </summary>
        public DateTime ProgramReleaseDate
        {
            get { return _programReleaseDate; }
        }
        
        private string _primaryDownloadUrl;
        /// <summary>
        /// Gets the application's primary download url.
        /// </summary>
        public string PrimaryDownloadUrl
        {
            get { return _primaryDownloadUrl; }
        }

        private int _fileSizeInBytes;
        /// <summary>
        /// Gets the application's size. In bytes.
        /// </summary>
        public int FileSizeInBytes
        {
            get { return _fileSizeInBytes; }
        }


        #endregion

        #region .ctor
        /// <summary>
        /// Creates a new instance of <see cref="PadDocument"/>.
        /// </summary>
        /// <param name="padFilePath">Path to the PAD file to wrap.</param>
        public PadDocument(string padFilePath)
        {
            _padFilePath = padFilePath;

            int year = -1,
                month = -1,
                day = -1;
            using (XmlReader reader = XmlReader.Create(_padFilePath))
            {
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "Program_Version":
                            _version = new Version(reader.ReadElementContentAsString());
                            break;
                        case "Program_Release_Year":
                            year = int.Parse(reader.ReadElementContentAsString());
                            break;
                        case "Program_Release_Month":
                            month = int.Parse(reader.ReadElementContentAsString());
                            break;
                        case "Program_Release_Day":
                            day = int.Parse(reader.ReadElementContentAsString());
                            break;
                        case "Primary_Download_URL":
                            _primaryDownloadUrl = reader.ReadElementContentAsString();
                            break;
                        case "File_Size_Bytes":
                            _fileSizeInBytes = int.Parse(reader.ReadElementContentAsString());
                            break;
                    }
                }
            }

            _programReleaseDate = new DateTime(year, month, day);
        } 
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Woofy.Core
{
    /// <summary>
    /// Downloads the desired comics.
    /// </summary>
    public class ComicsDownloader : IComicsDownloader
    {
        #region .ctor
        /// <summary>
        /// Creates a new instance of the <see cref="ComicsHandler"/>.
        /// </summary>
        public ComicsDownloader()
        {
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Downloads the comics to the specified directory, creating it if it doesn't exist.
        /// </summary>
        /// <param name="comics">List of comics to handle.</param>
        /// <param name="downloadDirectory">A string representing the name of the directory to which to download the comics. If it doesn't exist, it will be created.</param>
        /// <returns>Returns the number of comics actually downloaded (in case some comics were already downloaded).</returns>
        public int DownloadComics(string[] comicLinks, string downloadDirectory)
        {
            int downloadedComicsCount = 0;
            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;

                foreach (string comicLink in comicLinks)
                {
                    if (DownloadComic(comicLink, downloadDirectory))
                        downloadedComicsCount++;
                }
            }

            return downloadedComicsCount;
        }

        /// <summary>
        /// Downloads the comics to the specified directory, creating it if it doesn't exist.
        /// </summary>
        /// <param name="comics">List of comics to handle.</param>
        /// <param name="downloadDirectory">A string representing the name of the directory to which to download the comics. If it doesn't exist, it will be created.</param>
        /// <returns>True if the comic was downloaded, false otherwise. A comic may not be downloaded when a file with the same name exists in the same location.</returns>
        public bool DownloadComic(string comicLink, string downloadDirectory)
        {
            return DownloadComic(comicLink, downloadDirectory, null);
        }

        /// <summary>
        /// Downloads the comics to the specified directory, creating it if it doesn't exist.
        /// </summary>
        /// <param name="comics">List of comics to handle.</param>
        /// <param name="downloadDirectory">A string representing the name of the directory to which to download the comics. If it doesn't exist, it will be created.</param>
        /// <returns>True if the comic was downloaded, false otherwise. A comic may not be downloaded when a file with the same name exists in the same location.</returns>
        public bool DownloadComic(string comicLink, string downloadDirectory, WebProxy proxy)
        {
            if (string.IsNullOrEmpty(downloadDirectory))
                throw new ArgumentNullException("downloadDirectory", "The <downloadDirectory> parameter must be used to specify the name of the directory to which to download the comics.");

            if (!Directory.Exists(downloadDirectory))
                Directory.CreateDirectory(downloadDirectory);
            
            string fullFileName = GetFullFileName(comicLink, downloadDirectory);
            string tempFileName = fullFileName + ".!wf";
            
            if (File.Exists(fullFileName))
                return false;

            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Proxy = proxy;                
                
                try
                {
                    client.DownloadFile(comicLink, tempFileName);

                    File.Move(tempFileName, fullFileName);
                }
                catch
                {
                    File.Delete(tempFileName);
                    throw;
                }

                return true;
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Returns the full file name for a given comic link, and the directory to which the comic must be downloaded.
        /// </summary>
        /// <param name="comicLink"></param>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        private string GetFullFileName(string comicLink, string directoryName)
        {
            string comicName = comicLink.Substring(comicLink.LastIndexOf('/') + 1);
            return Path.Combine(directoryName, comicName);
        }
        #endregion
    }
}

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
        #region Instance Members
        private string _downloadDirectory;
        /// <summary>
        /// Gets the directory in which the comics will be downloaded.
        /// </summary>
        public string DownloadDirectory
        {
            get { return _downloadDirectory; }
        }

        private WebProxy _proxy;
        /// <summary>
        /// Gets or sets the proxy to be used by the downloader.
        /// </summary>
        public WebProxy Proxy
        {
            get { return _proxy; }
            set { _proxy = value; }
        }
        #endregion

        #region Constants
        /// <summary>
        /// Specifies the maximum size, in bytes, of the download buffer.
        /// </summary>
        private const int MaxBufferSize = 16384;
        #endregion

        #region .ctor
        /// <summary>
        /// Creates a new instance of the <see cref="ComicsDownloader"/>.
        /// </summary>
        /// <param name="downloadDirectory">The directory in which the comics will be downloaded.</param>
        public ComicsDownloader(string downloadDirectory)
            : this(downloadDirectory, null)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ComicsDownloader"/>.
        /// </summary>
        /// <param name="downloadDirectory">The directory in which the comics will be downloaded. If it doesn't exist, it is created.</param>
        /// <param name="proxy">The proxy to be used by the downloader.</param>
        public ComicsDownloader(string downloadDirectory, WebProxy proxy)
        {
            if (string.IsNullOrEmpty(downloadDirectory))
                throw new ArgumentNullException("downloadDirectory", "The <downloadDirectory> parameter must be used to specify the name of the directory to which to download the comics.");

            if (!Directory.Exists(downloadDirectory))
                Directory.CreateDirectory(downloadDirectory);

            _downloadDirectory = downloadDirectory;
            _proxy = proxy;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Downloads the specified comic. If the comic was already downloaded, then it is not downloaded again.
        /// </summary>
        /// <param name="comicLink">Link to the comic to be downloaded.</param>
        /// <param name="comicAlreadyDownloaded">True if the comic was already downloaded, false otherwise.</param>
        public void DownloadComic(string comicLink, out bool comicAlreadyDownloaded)
        {
            string filePath = GetFilePath(comicLink, _downloadDirectory);
            if (File.Exists(filePath))
            {
                comicAlreadyDownloaded = true;
                return;
            }

            WebRequest request = GetWebRequest(comicLink);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            string tempFilePath = filePath + ".!wf";
            BinaryWriter writer = new BinaryWriter(File.Create(tempFilePath));
            byte[] buffer = new byte[MaxBufferSize];

            try
            {
                try
                {
                    int bytesRead;
                    do
                    {
                        bytesRead = stream.Read(buffer, 0, MaxBufferSize);

                        writer.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead > 0);
                }
                finally
                {
                    stream.Close();
                    writer.Close();
                }

                File.Move(tempFilePath, filePath);
            }
            catch
            {
                File.Delete(tempFilePath);
                throw;
            }

            comicAlreadyDownloaded = false;
        }

        /// <summary>
        /// Downloads the specified comic asynchronously. If the comic was already downloaded, then it is not downloaded again.
        /// </summary>
        /// <remarks>Use the <see cref="ComicsDownloader.DownloadComicCompleted"/> event to know when the download completes.</remarks>
        /// <seealso cref="ComicsDownloader.DownloadComicCompleted"/>
        /// <param name="comicLink">Link to the comic to be downloaded.</param>
        public void DownloadComicAsync(string comicLink)
        {
            string filePath = GetFilePath(comicLink, _downloadDirectory);
            if (File.Exists(filePath))
            {
                OnDownloadComicCompleted(new DownloadComicCompletedEventArgs(true));
                return;
            }

            WebRequest request = GetWebRequest(comicLink);

            request.BeginGetResponse(
                delegate(IAsyncResult result)
                {
                    GetResponseCallback(result, filePath);
                }, request);
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Called when the application receives the response for the comic request.
        /// </summary>
        /// <param name="result">The standard <see cref="IAsyncResult"/>.</param>
        /// <param name="filePath">Path to the file where the comic will be downloaded.</param>
        private void GetResponseCallback(IAsyncResult result, string filePath)
        {
            WebRequest request = (WebRequest)result.AsyncState;
            WebResponse response = request.EndGetResponse(result);
            Stream stream = response.GetResponseStream();

            string tempFilePath = filePath + ".!wf";
            BinaryWriter writer = new BinaryWriter(File.Create(tempFilePath));
            byte[] buffer = new byte[MaxBufferSize];


            if (IsDownloadCancelled())
            {
                File.Delete(tempFilePath);
                return;
            }            
            
            stream.BeginRead(buffer, 0, MaxBufferSize,
                delegate(IAsyncResult innerResult)
                {
                    ReadBytesCallback(innerResult, buffer, writer, filePath, tempFilePath);
                }, stream);
        }        

        /// <summary>
        /// Called when the application receives a series of bytes from the comic.
        /// </summary>
        /// <param name="result">The standard <see cref="IAsyncResult"/>.</param>
        /// <param name="buffer">The buffer in which the bytes are read into.</param>
        /// <param name="writer">The <see cref="BinaryWriter"/> used to create the comic file on disk.</param>
        /// <param name="filePath">Path to the file where the comic will be downloaded.</param>
        /// <param name="tempFilePath"></param>
        private void ReadBytesCallback(IAsyncResult result, byte[] buffer, BinaryWriter writer, string filePath, string tempFilePath)
        {
            Stream stream = (Stream)result.AsyncState;
            try
            {
                int bytesRead = stream.EndRead(result);

                if (bytesRead == 0)
                {
                    writer.Close();
                    stream.Close();

                    File.Move(tempFilePath, filePath);

                    OnDownloadComicCompleted(new DownloadComicCompletedEventArgs(false));
                    return;
                }

                writer.Write(buffer, 0, bytesRead);

                if (IsDownloadCancelled())
                {
                    writer.Close();
                    stream.Close();

                    File.Delete(tempFilePath);
                    return;
                }

                stream.BeginRead(buffer, 0, MaxBufferSize,
                        delegate(IAsyncResult innerResult)
                        {
                            ReadBytesCallback(innerResult, buffer, writer, filePath, tempFilePath);
                        }, stream);
            }
            catch
            {
                stream.Close();
                writer.Close();

                File.Delete(tempFilePath);

                throw;
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Returns the full file path for a given comic link, and the directory to which the comic must be downloaded.
        /// </summary>
        /// <param name="comicLink">A link to the comic to be downloaded.</param>
        /// <param name="directoryName">The directory in which the comic should be downloaded.</param>
        /// <returns>The full path of the file to be downloaded.</returns>
        private string GetFilePath(string comicLink, string directoryName)
        {
            string comicName = comicLink.Substring(comicLink.LastIndexOf('/') + 1);
            return Path.Combine(directoryName, comicName);
        }

        /// <summary>
        /// Builds a web request based on the specified comic link.
        /// </summary>
        /// <param name="comicLink">Link to the comic to be downloaded.</param>
        /// <returns>A <see cref="WebRequest"/> for the specified comic link.</returns>
        private WebRequest GetWebRequest(string comicLink)
        {
            WebRequest request = WebRequest.Create(comicLink);
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Proxy = _proxy;
            return request;
        }

        /// <summary>
        /// Determines whether the user decided to stop the download.
        /// </summary>
        /// <returns>True if the user has decided to stop the download, false otherwise.</returns>
        private bool IsDownloadCancelled()
        {
            DownloadedComicChunkEventArgs e = new DownloadedComicChunkEventArgs();
            OnDownloadedComicChunk(e);
            return e.Cancel;
        }
        #endregion

        #region DownloadComicCompleted Event

        private event EventHandler<DownloadComicCompletedEventArgs> _downloadComicCompleted;
        /// <summary>
        /// Occurs when an asynchronous download operation completes.
        /// </summary>
        public event EventHandler<DownloadComicCompletedEventArgs> DownloadComicCompleted
        {
            add
            {
                _downloadComicCompleted += value;
            }
            remove
            {
                _downloadComicCompleted -= value;
            }
        }

        protected virtual void OnDownloadComicCompleted(DownloadComicCompletedEventArgs e)
        {
            EventHandler<DownloadComicCompletedEventArgs> eventReference = _downloadComicCompleted;

            if (eventReference != null)
                eventReference(this, e);
        }
        #endregion

        #region DownloadedComicChunk Event

        private event EventHandler<DownloadedComicChunkEventArgs> _downloadedComicChunk;
        /// <summary>
        /// Occurs when a comic chunk is downloaded. Can be used to cancel the download of the current strip.
        /// </summary>
        public event EventHandler<DownloadedComicChunkEventArgs> DownloadedComicChunk
        {
            add
            {
                _downloadedComicChunk += value;
            }
            remove
            {
                _downloadedComicChunk -= value;
            }
        }

        protected virtual void OnDownloadedComicChunk(DownloadedComicChunkEventArgs e)
        {
            EventHandler<DownloadedComicChunkEventArgs> eventReference = _downloadedComicChunk;

            if (eventReference != null)
                eventReference(this, e);
        }

        #endregion
    }
}
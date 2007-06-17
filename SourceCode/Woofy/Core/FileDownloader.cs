using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Woofy.Core
{
    /// <summary>
    /// Downloads one or more files to a specified directory.
    /// </summary>
    public class FileDownloader : IFileDownloader
    {
        #region Instance Members
        private string _downloadDirectory;
        /// <summary>
        /// Gets the directory in which the files will be downloaded.
        /// </summary>
        public string DownloadDirectory
        {
            get { return _downloadDirectory; }
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
        /// Creates a new instance of the <see cref="FileDownloader"/>.
        /// </summary>
        /// <param name="downloadDirectory">The directory in which the files will be downloaded. If it doesn't exist, it is created.</param>
        public FileDownloader(string downloadDirectory)
        {
            if (string.IsNullOrEmpty(downloadDirectory))
                throw new ArgumentNullException("downloadDirectory", "The <downloadDirectory> parameter must be used to specify the name of the directory to which to download the files.");

            if (!Directory.Exists(downloadDirectory))
                Directory.CreateDirectory(downloadDirectory);

            _downloadDirectory = downloadDirectory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Downloads the specified file. If the file exists, then it is not downloaded again.
        /// </summary>
        /// <param name="fileLink">Link to the file to be downloaded.</param>
        /// <param name="fileAlreadyDownloaded">True if the file was already downloaded, false otherwise.</param>
        public void DownloadFile(string fileLink, out bool fileAlreadyDownloaded)
        {
            string filePath = GetFilePath(fileLink, _downloadDirectory);
            if (File.Exists(filePath))
            {
                fileAlreadyDownloaded = true;
                return;
            }

            WebRequest request = WebConnectionFactory.GetNewWebRequestInstance(fileLink);
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

            fileAlreadyDownloaded = false;
        }

        /// <summary>
        /// Downloads the specified file asynchronously. If the file exists, then it is not downloaded again.
        /// </summary>
        /// <remarks>Use the <see cref="FileDownloader.DownloadFileCompleted"/> event to know when the download completes.</remarks>
        /// <seealso cref="FileDownloader.DownloadFileCompleted"/>
        /// <param name="link">Link to the file to be downloaded.</param>
        public void DownloadFileAsync(string fileLink)
        {
            string filePath = GetFilePath(fileLink, _downloadDirectory);
            if (File.Exists(filePath))
            {
                OnDownloadFileCompleted(new DownloadFileCompletedEventArgs(true));
                return;
            }

            WebRequest request = WebConnectionFactory.GetNewWebRequestInstance(fileLink);

            request.BeginGetResponse(
                delegate(IAsyncResult result)
                {
                    GetResponseCallback(result, filePath);
                }, request);
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Called when the application receives the response for the file request.
        /// </summary>
        /// <param name="result">The standard <see cref="IAsyncResult"/>.</param>
        /// <param name="filePath">Path where the file will be downloaded.</param>
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
        /// Called when the application receives a series of bytes from the file download.
        /// </summary>
        /// <param name="result">The standard <see cref="IAsyncResult"/>.</param>
        /// <param name="buffer">The buffer in which the bytes are read into.</param>
        /// <param name="writer">The <see cref="BinaryWriter"/> used to create the file on disk.</param>
        /// <param name="filePath">Path where the file will be downloaded.</param>
        /// <param name="tempFilePath">Path to the temporary file.</param>
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

                    OnDownloadFileCompleted(new DownloadFileCompletedEventArgs(false));
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
        /// Returns the full file path for a given file link, and the directory to which the file must be downloaded.
        /// </summary>
        /// <param name="fileLink">A link to the file to be downloaded.</param>
        /// <param name="directoryName">The directory in which the file should be downloaded.</param>
        /// <returns>The full path of the file to be downloaded.</returns>
        private string GetFilePath(string fileLink, string directoryName)
        {
            string fileName = fileLink.Substring(fileLink.LastIndexOf('/') + 1);
            return Path.Combine(directoryName, fileName);
        }

        /// <summary>
        /// Determines whether the user decided to stop the download.
        /// </summary>
        /// <returns>True if the user has decided to stop the download, false otherwise.</returns>
        private bool IsDownloadCancelled()
        {
            DownloadedFileChunkEventArgs e = new DownloadedFileChunkEventArgs();
            OnDownloadedFileChunk(e);
            return e.Cancel;
        }
        #endregion

        #region DownloadFileCompleted Event

        private event EventHandler<DownloadFileCompletedEventArgs> _downloadFileCompleted;
        /// <summary>
        /// Occurs when an asynchronous download operation completes.
        /// </summary>
        public event EventHandler<DownloadFileCompletedEventArgs> DownloadFileCompleted
        {
            add
            {
                _downloadFileCompleted += value;
            }
            remove
            {
                _downloadFileCompleted -= value;
            }
        }

        protected virtual void OnDownloadFileCompleted(DownloadFileCompletedEventArgs e)
        {
            EventHandler<DownloadFileCompletedEventArgs> eventReference = _downloadFileCompleted;

            if (eventReference != null)
                eventReference(this, e);
        }
        #endregion

        #region DownloadedFileChunk Event

        private event EventHandler<DownloadedFileChunkEventArgs> _downloadedFileChunk;
        /// <summary>
        /// Occurs when a comic chunk is downloaded. Can be used to cancel the download of the current strip.
        /// </summary>
        public event EventHandler<DownloadedFileChunkEventArgs> DownloadedFileChunk
        {
            add
            {
                _downloadedFileChunk += value;
            }
            remove
            {
                _downloadedFileChunk -= value;
            }
        }

        protected virtual void OnDownloadedFileChunk(DownloadedFileChunkEventArgs e)
        {
            EventHandler<DownloadedFileChunkEventArgs> eventReference = _downloadedFileChunk;

            if (eventReference != null)
                eventReference(this, e);
        }

        #endregion
    }
}
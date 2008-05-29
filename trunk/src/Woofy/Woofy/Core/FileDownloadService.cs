using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;

using Woofy.Other;
using Woofy.Exceptions;
using Woofy.Entities;

namespace Woofy.Core
{
    public class FileDownloadService
    {
        private readonly WebClientWrapper _webClient;
        private readonly FileWrapper _fileWrapper;
        private readonly PathWrapper _pathWrapper;

        #region Constructors
        public FileDownloadService()
            : this(new WebClientWrapper(), new FileWrapper(), new PathWrapper())
        {
        }

        public FileDownloadService(WebClientWrapper webClient, FileWrapper fileWrapper, PathWrapper pathWrapper)
        {
            _fileWrapper = fileWrapper;
            _pathWrapper = pathWrapper;

            _webClient = webClient;
            _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompletedCallback);
            _webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChangedCallback);
        }
        #endregion

        //public string DownloadFile(Uri fileUri)
        //{
        //    return DownloadFile(fileUri, null);
        //}

        //public string DownloadFile(Uri fileUri, string downloadFolder)
        //{
        //    return DownloadFile(fileUri, downloadFolder, false);
        //}

        public void DownloadFile(Uri fileUri, string downloadPath, Uri referrerUri)
        {
            try
            {
                _webClient.Headers.Clear();
                if (referrerUri != null)
                    _webClient.Headers.Add("Referer", referrerUri.AbsoluteUri);

                _webClient.DownloadFile(fileUri, downloadPath);
            }
            catch
            {
                _fileWrapper.Delete(downloadPath);
                throw;
            }
        }

        public void DownloadFileAsync(Uri fileUri)
        {
            DownloadFileAsync(fileUri, null);
        }

        public void DownloadFileAsync(Uri fileUri, string downloadFolder)
        {
            DownloadFileAsync(fileUri, downloadFolder, null);
        }

        public void DownloadFileAsync(Uri fileUri, string downloadFolder, Uri referrerUri)
        {
            string filePath = GetPathToDownloadFileTo(fileUri, downloadFolder);
            string tempFilePath = filePath + ".!wf";

            if (_fileWrapper.Exists(filePath))
            {
                OnDownloadFileCompleted(new FileAlreadyPresentException(), false);
                return;
            }

            _webClient.Headers.Clear();
            if (referrerUri != null)
                _webClient.Headers.Add("Referer", referrerUri.AbsoluteUri);
            _webClient.DownloadFileAsync(fileUri, tempFilePath);
        }

        private void DownloadProgressChangedCallback(object sender, DownloadProgressChangedEventArgs e)
        {

        }

        private void DownloadFileCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            OnDownloadFileCompleted(e);
        }

        public string GetPathToDownloadFileTo(Uri uri, string downloadFolder)
        {
            string fileName = uri.AbsoluteUri.Substring(uri.AbsoluteUri.LastIndexOf('/') + 1);
            string filePath;
            if (string.IsNullOrEmpty(downloadFolder))
                filePath = _pathWrapper.Combine(ApplicationSettings.DefaultDownloadFolder, fileName);
            else
                filePath = _pathWrapper.Combine(downloadFolder, fileName);

            return filePath;
        }

        #region Events - DownloadFileCompleted
        private event EventHandler<AsyncCompletedEventArgs> _downloadFileCompleted;
        public event EventHandler<AsyncCompletedEventArgs> DownloadFileCompleted
        {
            add { _downloadFileCompleted += value; }
            remove { _downloadFileCompleted -= value; }
        }

        protected virtual void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
        {
            EventHandler<AsyncCompletedEventArgs> reference = _downloadFileCompleted;

            if (reference != null)
                reference(this, e);
        }

        private void OnDownloadFileCompleted(Exception error, bool cancelled)
        {
            AsyncCompletedEventArgs e = new AsyncCompletedEventArgs(error, cancelled, null);
            OnDownloadFileCompleted(e);
        }
        #endregion
    }
}
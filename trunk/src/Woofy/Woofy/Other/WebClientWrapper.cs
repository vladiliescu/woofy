﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.ComponentModel;

namespace Woofy.Other
{
    public class WebClientWrapper
    {
        private readonly WebClient _webClient;
        public WebClientWrapper()
        {
            _webClient = new WebClient();
        }

        public virtual void DownloadFile(Uri address, string fileName)
        {
            _webClient.DownloadFile(address, fileName);
        }

        public virtual void DownloadFileAsync(Uri address, string fileName)
        {
            _webClient.DownloadFileAsync(address, fileName);
        }

        public event AsyncCompletedEventHandler DownloadFileCompleted
        {
            add { _webClient.DownloadFileCompleted += value; }
            remove { _webClient.DownloadFileCompleted -= value; }
        }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add { _webClient.DownloadProgressChanged += value; }
            remove { _webClient.DownloadProgressChanged -= value; }
        }

        public WebHeaderCollection Headers
        {
            get { return _webClient.Headers; }
            set { _webClient.Headers = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using Woofy.Properties;
using Woofy.Settings;

namespace Woofy.Core
{
    /// <summary>
    /// Provides static methods for building web requests, web clients, etc.
    /// </summary>
    public static class WebConnectionFactory
    {
        private static WebProxy _proxy = null;
        /// <summary>
        /// Gets the proxy used for building the web connections. Can be null.
        /// </summary>
        public static WebProxy Proxy
        {
            get { return _proxy; }
        }

        /// <summary>
        /// Builds the default proxy instance.
        /// </summary>
        static WebConnectionFactory()
        {
            if (string.IsNullOrEmpty(UsrSettings.ProxyAddress)) 
                return;

            if (UsrSettings.ProxyPort.HasValue)
                _proxy = new WebProxy(UsrSettings.ProxyAddress, UsrSettings.ProxyPort.Value);
            else 
                _proxy = new WebProxy(UsrSettings.ProxyAddress);

            if (!string.IsNullOrEmpty(UsrSettings.ProxyUsername))
                _proxy.Credentials = new NetworkCredential(UsrSettings.ProxyUsername, UsrSettings.ProxyPassword);
            else
                _proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
        }

        /// <summary>
        /// Builds a new <see cref="WebClient"/> instance.
        /// </summary>
        /// <returns>A new <see cref="WebClient"/> instance.</returns>
        public static WebClient GetNewWebClientInstance()
        {
            WebClient client = new WebClient();
            
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.Proxy = Proxy;

            return client;
        }

        /// <summary>
        /// Builds a web request for a given uri string.
        /// </summary>
        /// <param name="requestUriString">Uri of the request.</param>
        /// <returns>A <see cref="WebRequest"/> for the specified uri string.</returns>
        public static WebRequest GetNewWebRequestInstance(string requestUriString)
        {
            WebRequest request = WebRequest.Create(requestUriString);
            
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Proxy = Proxy;

            return request;
        }

    }
}

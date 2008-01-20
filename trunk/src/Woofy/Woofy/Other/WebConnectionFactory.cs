using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Woofy.Other
{
    /// <summary>
    /// Provides static methods for building web requests, web clients, etc.
    /// </summary>
    public static class WebConnectionFactory
    {
        private static WebProxy _proxy = null;        

        /// <summary>
        /// Builds the default proxy instance.
        /// </summary>
        static WebConnectionFactory()
        {
            //if (!string.IsNullOrEmpty(UserSettings.ProxyAddress))
            //{
            //    if (UserSettings.ProxyPort.HasValue)
            //        _proxy = new WebProxy(UserSettings.ProxyAddress, UserSettings.ProxyPort.Value);
            //    else
            //        _proxy = new WebProxy(UserSettings.ProxyAddress);
            //    _proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            //}
        }

        /// <summary>
        /// Builds a new <see cref="WebClient"/> instance.
        /// </summary>
        /// <returns>A new <see cref="WebClient"/> instance.</returns>
        public static WebClient CreateWebClient()
        {
            WebClient client = new WebClient();

            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.Proxy = _proxy;

            return client;
        }

        /// <summary>
        /// Builds a web request for a given uri string.
        /// </summary>
        /// <param name="requestUriString">Uri of the request.</param>
        /// <returns>A <see cref="WebRequest"/> for the specified uri string.</returns>
        public static WebRequest CreateWebRequest(Uri requestUri)
        {
            WebRequest request = WebRequest.Create(requestUri);

            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Proxy = _proxy;

            return request;
        }

    }
}

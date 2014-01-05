using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uri = Mono.System.Uri;

namespace Woofy.Core
{
    public static class UriEx
    {
        public static Uri From(string argument, Action reportFormatException)
        {
            Uri uri = null;
            try
            {
                uri = new Uri(argument);
            }
            catch (UriFormatException)
            {
                reportFormatException();
            }

            return uri;
        }
    }
}

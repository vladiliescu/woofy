using System;
using Woofy.Core.Infrastructure;
using Uri = Mono.System.Uri;

namespace Woofy.Core.Engine
{
    public class CurrentPageChanged : IEvent
    {
        public string ComicId { get; private set; }
        public Uri Url { get; private set; }

        public CurrentPageChanged(string comicId, Uri url)
        {
            ComicId = comicId;
            Url = url;
        }
    }
}
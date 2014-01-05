using System;
using System.Collections.Generic;
using Uri = Mono.System.Uri;

namespace Woofy.Core.Engine
{
	public class Context
	{
	    public string Comic { get; private set; }
        public string ComicId { get; private set; }
		public Dictionary<string, string> Metadata { get; private set; }

		public Uri CurrentAddress { get; set; }
	    public string PageContent { get; set; }

	    public Context(string id, string comic, Uri startAt)
        {
            ComicId = id;
            Comic = comic;
            CurrentAddress = startAt;
	        Metadata = new Dictionary<string, string>();
        }
	}
}
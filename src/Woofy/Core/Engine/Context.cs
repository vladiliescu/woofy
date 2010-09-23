using System;
using System.Collections.Generic;

namespace Woofy.Core.Engine
{
	public class Context
	{
		/// <summary>
		/// Should I keep these properties in the Session too? And maybe provide some strong-typed accessors?
		/// </summary>
		public Uri CurrentAddress { get; set; }
		public string PageContent { get; set; }
		public Dictionary<string, object> Session { get; set; }
	}
}
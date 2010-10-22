using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;

namespace Woofy.Core.ComicManagement
{
	public class ComicDefinition : IEquatable<ComicDefinition>
	{
		/// <summary>
		/// The definition's filename. It uniquely identifies a comic/definition.
		/// </summary>
		public string Filename { get; private set; }

		public string Name { get; private set; }
		public string HomePage { get; private set; }

		public string StartPage { get; private set; }

		public string ComicRegex { get; private set; }
		public string NextPageRegex { get; private set; }
		
		
		public bool AllowMissingStrips { get; private set; }
		public bool AllowMultipleStrips { get; private set; }
		public string RootUrl { get; private set; }
		public Collection<Capture> Captures { get; private set; }
		public string RenamePattern { get; private set; }

		public string Author { get; private set; }

		/// <summary>
		/// Used for the json deserialization
		/// </summary>
		public ComicDefinition()
		{
		}

#warning another service should be responsible for parsing the definition - this way it would be much easier to add alternative definition formats (e.g. json)
		/// <summary>
		/// Initializes a new instance of the <see cref="ComicDefinition"/> class.
		/// </summary>
		/// <param name="definitionStream">Stream containing the data necessary to create a new instance.</param>
		public ComicDefinition(string filename, Stream definitionStream)
		{
			Filename = filename;
			var doc = new XmlDocument();
			doc.Load(definitionStream);
			var definition = doc.SelectSingleNode("comicDefinition");

			Name = definition.Attributes["name"].Value;
			Author = definition.Attributes["definitionAuthor"] == null ? null : definition.Attributes["definitionAuthor"].Value;
			var allowMissingStrips = definition.Attributes["allowMissingStrips"] == null ? "" : definition.Attributes["allowMissingStrips"].Value;
			AllowMissingStrips = allowMissingStrips.ParseAsSafe<bool>();
			var allowMultipleStrips = definition.Attributes["allowMultipleStrips"] == null ? "" : definition.Attributes["allowMultipleStrips"].Value;
			AllowMultipleStrips = allowMultipleStrips.ParseAsSafe<bool>();
			HomePage = ExtractInnerText(definition, "homePage");
			StartPage = ExtractInnerText(definition, "startPage");
			ComicRegex = ExtractInnerText(definition, "comicRegex");
			NextPageRegex = ExtractInnerText(definition, "nextPageRegex");
			RootUrl = ExtractInnerText(definition, "rootUrl");
			RenamePattern = ExtractInnerText(definition, "renamePattern");

			Captures = new Collection<Capture>();

			foreach (XmlNode captureNode in definition.SelectNodes("captures/capture"))
			{
				Captures.Add(BuildCapture(captureNode));
			}
		}

		private Capture BuildCapture(XmlNode captureNode)
		{
			if (captureNode.Attributes["target"] == null)
				return new Capture(captureNode.Attributes["name"].Value, captureNode.InnerText);

			var target = captureNode.Attributes["target"].Value;
			if (target.Trim().ToUpper().Equals("URL"))
				return new Capture(captureNode.Attributes["name"].Value, captureNode.InnerText, CaptureTarget.Url);

			return new Capture(captureNode.Attributes["name"].Value, captureNode.InnerText, CaptureTarget.Body);
		}

		private static string ExtractInnerText(XmlNode comicInfo, string xpath)
		{
			var node = comicInfo.SelectSingleNode(xpath);
			return node == null ? null : node.InnerText;
		}

		public override string ToString()
		{
			return Name;
		}

        #region Equality
        public bool Equals(ComicDefinition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Filename, Filename);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ComicDefinition)) return false;
            return Equals((ComicDefinition)obj);
        }

        public override int GetHashCode()
        {
            return (Filename != null ? Filename.GetHashCode() : 0);
        } 
        #endregion
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;

namespace Woofy.Core
{
	public class ComicDefinition
	{
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
		
		public bool FailedToInitialize { get; private set; }
		public string ComicInfoFile { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ComicDefinition"/> class.
		/// </summary>
		/// <param name="comicInfoStream">Stream containing the data necessary to create a new instance.</param>
		public ComicDefinition(Stream comicInfoStream)
		{
			var doc = new XmlDocument();
			doc.Load(comicInfoStream);
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

		/// <summary>
		/// Initializes a new instance of the <see cref="ComicDefinition"/> class.
		/// </summary>
		/// <param name="comicInfoFile">Path to an xml file containing the data necessary to create a new instance.</param>
		public ComicDefinition(string comicInfoFile)
			: this(new FileStream(comicInfoFile, FileMode.Open, FileAccess.Read))
		{
			ComicInfoFile = comicInfoFile;
		}

		public ComicDefinition()
		{
		}

		/// <summary>
		/// Returns the available comic info files.
		/// </summary>
		public static ComicDefinition[] GetAvailableComicDefinitions()
		{
			var availableComicInfos = new List<ComicDefinition>();

			if (!Directory.Exists(AppSettingsOld.ComicDefinitionsFolder))
				return new ComicDefinition[0];

			foreach (var comicInfoFile in Directory.GetFiles(AppSettingsOld.ComicDefinitionsFolder, "*.xml"))
			{
				ComicDefinition definition;
				try
				{
					definition = new ComicDefinition(comicInfoFile);
				}
				catch (Exception ex)
				{
					Logger.LogException("Error initializing definition " + comicInfoFile, ex);

					definition = new ComicDefinition
					{
						ComicInfoFile = comicInfoFile,
						Name = string.Format("--- Failed to initialize definition {0} ---", Path.GetFileName(comicInfoFile)),
						FailedToInitialize = true
					};
				}

				availableComicInfos.Add(definition);
			}

			return availableComicInfos.ToArray();
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

		private string ExtractInnerText(XmlNode comicInfo, string xpath)
		{
			var node = comicInfo.SelectSingleNode(xpath);
			return node == null ? null : node.InnerText;
		}

		public override string ToString()
		{
			return Name;
		}

		public bool Equals(ComicDefinition other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.ComicInfoFile, ComicInfoFile);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (ComicDefinition)) return false;
			return Equals((ComicDefinition) obj);
		}

		public override int GetHashCode()
		{
			return (ComicInfoFile != null ? ComicInfoFile.GetHashCode() : 0);
		}

		public static bool operator ==(ComicDefinition left, ComicDefinition right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ComicDefinition left, ComicDefinition right)
		{
			return !Equals(left, right);
		}
	}
}

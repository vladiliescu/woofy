using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Woofy.Entities;
using System.Xml;

namespace Woofy.Services
{
    public class ComicDefinitionsService
    {
        public ComicDefinitionCollection BuildComicDefinitionsFromFiles()
        {
            return BuildComicDefinitionsFromFiles(Directory.GetFiles(ApplicationSettings.ComicDefinitionsFolder));
        }

        public ComicDefinitionCollection BuildComicDefinitionsFromFiles(string[] definitionFiles)
        {
            ComicDefinitionCollection definitions = new ComicDefinitionCollection();

            foreach (string definitionFile in definitionFiles)
                definitions.Add(BuildDefinitionFromFile(definitionFile));

            return definitions;
        }

        /// <param name="comicInfoStream">Stream containing the data necessary to create a new instance.</param>
        public ComicDefinition BuildDefinitionFromStream(Stream comicInfoStream)
        {
            ComicDefinition definition = new ComicDefinition();
            Comic comic = new Comic();
            comic.AssociateWithDefinition(definition);

            using (XmlReader reader = XmlReader.Create(comicInfoStream))
            {
                reader.Read();  //<?xml..
                reader.Read();  //Whitespace..
                reader.Read();  //<comicInfo..
                definition.Comic.Name = reader.GetAttribute("friendlyName");
                definition.Author = reader.GetAttribute("author");
                definition.AuthorEmail = reader.GetAttribute("authorEmail");

                string allowMissingStrips = reader.GetAttribute("allowMissingStrips");
                string allowMultipleStrips = reader.GetAttribute("allowMultipleStrips");

                if (!string.IsNullOrEmpty(allowMissingStrips))
                    definition.AllowMissingStrips = bool.Parse(allowMissingStrips);
                if (!string.IsNullOrEmpty(allowMultipleStrips))
                    definition.AllowMultipleStrips = bool.Parse(allowMultipleStrips);

                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "startUrl":
                            definition.HomePageAddress = new Uri(reader.ReadElementContentAsString());
                            break;
                        case "comicRegex":
                            definition.StripRegex = reader.ReadElementContentAsString();
                            break;
                        case "backButtonRegex":
                            definition.NextIssueRegex = reader.ReadElementContentAsString();
                            break;
                        case "firstIssue":
                            definition.FirstStripAddress = new Uri(reader.ReadElementContentAsString());
                            break;
                        case "latestPageRegex":
                            definition.LatestIssueRegex = reader.ReadElementContentAsString();
                            break;
                    }
                }
            }

            if (string.IsNullOrEmpty(definition.Comic.Name))
                throw new InvalidOperationException("The comic definition does not specify a name.");
            if (string.IsNullOrEmpty(definition.HomePageAddress.AbsoluteUri))
                throw new InvalidOperationException("The comic definition does not specify a home url.");
            if (string.IsNullOrEmpty(definition.StripRegex))
                throw new InvalidOperationException("The comic definition does not specify a strip regular expression.");
            if (string.IsNullOrEmpty(definition.NextIssueRegex))
                throw new InvalidOperationException("The comic definition does not specify a next issue regular expression.");

            return definition;
        }

        /// <param name="definitionFile">Path to an xml file containing the data necessary to create a new instance.</param>
        public ComicDefinition BuildDefinitionFromFile(string definitionFile)
        {
            ComicDefinition definition = BuildDefinitionFromStream(new FileStream(definitionFile, FileMode.Open, FileAccess.Read));
            definition.SourceFileName = definitionFile;
            
            return definition;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Woofy.Entities;
using System.Xml;

namespace Woofy.Services
{
    public class ComicDefinitionsService : IComicDefinitionsService
    {

        public ComicCollection BuildComicsFromDefinitions()
        {
            return BuildComicsFromDefinitions(Directory.GetFiles(ApplicationSettings.ComicDefinitionsFolder));
        }

        public ComicCollection BuildComicsFromDefinitions(string[] definitionFiles)
        {
            ComicCollection comics = new ComicCollection();

            foreach (string definitionFile in definitionFiles)
                comics.Add(BuildComicFromDefinition(definitionFile));

            return comics;
        }

        /// <param name="comicInfoStream">Stream containing the data necessary to create a new instance.</param>
        public virtual Comic BuildComicFromDefinition(Stream comicInfoStream)
        {
            Comic comic = new Comic();
            using (XmlReader reader = XmlReader.Create(comicInfoStream))
            {
                reader.Read();  //<?xml..
                reader.Read();  //Whitespace..
                reader.Read();  //<comicInfo..
                comic.Name = reader.GetAttribute("friendlyName");
                comic.DefinitionAuthor = reader.GetAttribute("author");
                comic.DefinitionAuthorEmail = reader.GetAttribute("authorEmail");

                string allowMissingStrips = reader.GetAttribute("allowMissingStrips");
                string allowMultipleStrips = reader.GetAttribute("allowMultipleStrips");

                if (!string.IsNullOrEmpty(allowMissingStrips))
                    comic.AllowMissingStrips = bool.Parse(allowMissingStrips);
                if (!string.IsNullOrEmpty(allowMultipleStrips))
                    comic.AllowMultipleStrips = bool.Parse(allowMultipleStrips);
                
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "startUrl":
                            comic.HomeUrl = reader.ReadElementContentAsString();
                            break;
                        case "comicRegex":
                            comic.StripRegex = reader.ReadElementContentAsString();
                            break;
                        case "backButtonRegex":
                            comic.NextIssueRegex = reader.ReadElementContentAsString();
                            break;
                        case "firstIssue":
                            comic.FirstStripUrl = reader.ReadElementContentAsString();
                            break;
                        case "latestPageRegex":
                            comic.LatestIssueRegex = reader.ReadElementContentAsString();
                            break;
                    }
                }
            }

            if (string.IsNullOrEmpty(comic.Name))
                throw new InvalidOperationException("The comic definition does not specify a name.");
            if (string.IsNullOrEmpty(comic.HomeUrl))
                throw new InvalidOperationException("The comic definition does not specify a home url.");
            if (string.IsNullOrEmpty(comic.StripRegex))
                throw new InvalidOperationException("The comic definition does not specify a strip regular expression.");
            if (string.IsNullOrEmpty(comic.NextIssueRegex))
                throw new InvalidOperationException("The comic definition does not specify a next issue regular expression.");

            return comic;
        }

        /// <param name="definitionFile">Path to an xml file containing the data necessary to create a new instance.</param>
        public virtual Comic BuildComicFromDefinition(string definitionFile)
        {
            Comic comic = BuildComicFromDefinition(new FileStream(definitionFile, FileMode.Open, FileAccess.Read));
            comic.DefinitionFileName = definitionFile;
            
            return comic;
        }

    }
}

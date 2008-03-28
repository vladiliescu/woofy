using System;
using System.Collections.Generic;
using System.Text;
using Woofy.Entities;
using System.Data.Common;

namespace Woofy.DatabaseAccess
{
    public static class EntityModel
    {
        private class ModelData
        {
            public string TableName { get; private set; }
            public string SelectCommandText { get; private set; }

            public ModelData(string tableName, string selectCommandText)
            {
                TableName = tableName;
                SelectCommandText = selectCommandText;
            }
        }

        private static Dictionary<Type, ModelData> ModelsData = new Dictionary<Type, ModelData>();

        static EntityModel()
        {
            ModelsData.Add(typeof(Comic), 
                new ModelData("Comics",
@"SELECT Id, Name, IsActive, FaviconPath, Priority
    FROM Comics")
                );

            ModelsData.Add(typeof(ComicDefinition), 
                new ModelData("ComicDefinitions",
@"SELECT ComicId, AllowMissingStrips, AllowMultipleStrips, Author, AuthorEmail, FirstStripAddress, HomePageAddress, LatestIssueRegex, NextIssueRegex, SourceFileName, StripRegex
    FROM ComicDefinitions")
                );

            ModelsData.Add(typeof(ComicStrip), 
                new ModelData("ComicStrips", 
@"SELECT Id, ComicId, SourcePageAddress, FilePath
    FROM ComicStrips")
                );
        }

        public static string FindSelectCommandText<T>()
        {
            return ModelsData[typeof(T)].SelectCommandText;
        }

        public static string FindTableName<T>()
        {
            return ModelsData[typeof(T)].TableName;
        }

        public static object ReadComicStrip(DbDataReader reader)
        {
            ComicStrip strip = new ComicStrip();

            strip.ComicId = reader.GetValue<long>("ComicId");
            strip.FilePath = reader.GetValue<string>("FilePath");
            strip.Id = reader.GetValue<long>("Id");
            strip.SourcePageAddress = new Uri(reader.GetValue<string>("SourcePageAddress"));

            return strip;
        }

        public static object ReadComic(DbDataReader reader)
        {
            Comic comic = new Comic();

            comic.Id = reader.GetValue<long>("Id");
            comic.Name = reader.GetValue<string>("Name");
            comic.IsActive = reader.GetValue<bool>("IsActive");
            comic.FaviconPath = reader.GetValue<string>("FaviconPath");
            comic.Priority = reader.GetValue<int>("Priority");

            return comic;
        }

        public static object ReadComicDefinition(DbDataReader reader)
        {
            ComicDefinition definition = new ComicDefinition();

            definition.ComicId = reader.GetValue<long>("ComicId");
            definition.AllowMissingStrips = reader.GetValue<bool>("AllowMissingStrips");
            definition.AllowMultipleStrips = reader.GetValue<bool>("AllowMultipleStrips");
            definition.Author = reader.GetValue<string>("Author");
            definition.AuthorEmail = reader.GetValue<string>("AuthorEmail");
            //definition.FirstStripAddress = new Uri(GetReaderValue<string>(reader, "FirstStripAddress"));
            definition.HomePageAddress = new Uri(reader.GetValue<string>("HomePageAddress"));
            definition.LatestIssueRegex = reader.GetValue<string>("LatestIssueRegex");
            definition.NextIssueRegex = reader.GetValue<string>("NextIssueRegex");
            definition.SourceFileName = reader.GetValue<string>("SourceFileName");
            definition.StripRegex = reader.GetValue<string>("StripRegex");

            return definition;
        }
    }
}

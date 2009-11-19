using System;
using System.Collections.Generic;
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

        private static readonly Dictionary<Type, ModelData> ModelsData = new Dictionary<Type, ModelData>();

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
            var strip = new ComicStrip
                            {
                                ComicId = reader.GetValue<long>("ComicId"),
                                FilePath = reader.GetValue<string>("FilePath"),
                                Id = reader.GetValue<long>("Id"),
                                SourcePageAddress = new Uri(reader.GetValue<string>("SourcePageAddress"))
                            };

            return strip;
        }

        public static object ReadComic(DbDataReader reader)
        {
            var comic = new Comic
                            {
                                Id = reader.GetValue<long>("Id"),
                                Name = reader.GetValue<string>("Name"),
                                IsActive = reader.GetValue<bool>("IsActive"),
                                Priority = reader.GetValue<int>("Priority")
                            };

            return comic;
        }

        public static object ReadComicDefinition(DbDataReader reader)
        {
            var definition = new ComicDefinition
                                 {
                                     ComicId = reader.GetValue<long>("ComicId"),
                                     AllowMissingStrips = reader.GetValue<bool>("AllowMissingStrips"),
                                     AllowMultipleStrips = reader.GetValue<bool>("AllowMultipleStrips"),
                                     Author = reader.GetValue<string>("Author"),
                                     AuthorEmail = reader.GetValue<string>("AuthorEmail"),
                                     HomePageAddress = new Uri(reader.GetValue<string>("HomePageAddress")),
                                     LatestIssueRegex = reader.GetValue<string>("LatestIssueRegex"),
                                     NextPageRegex = reader.GetValue<string>("NextIssueRegex"),
                                     SourceFileName = reader.GetValue<string>("SourceFileName"),
                                     StripRegex = reader.GetValue<string>("StripRegex")
                                 };

            return definition;
        }
    }
}

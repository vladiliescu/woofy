using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Woofy.Entities;

namespace Woofy
{
    public static class ExtensionMethods
    {
        public static T GetValue<T>(this DbDataReader reader, string columnName)
        {
            object value = reader.GetValue(reader.GetOrdinal(columnName));
            if (value == DBNull.Value)
                value = null;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Read<T>(this DbDataReader reader)
            where T : class
        {
            if (!reader.Read())
                return null;

            Type type = typeof(T);
            
            if (type == typeof(ComicDefinition))
                return (T)ReadComicDefinition(reader);
            else if (type == typeof(Comic))
                return (T)ReadComic(reader);
            else if (type == typeof(ComicStrip))
                return (T)ReadComicStrip(reader);

            return null;
            
        }

        private static object ReadComicStrip(DbDataReader reader)
        {
            ComicStrip strip = new ComicStrip();

            strip.ComicId = reader.GetValue<long>("ComicId");
            strip.FilePath = reader.GetValue<string>("FilePath");
            strip.Id = reader.GetValue<long>("Id");
            strip.SourcePageAddress = new Uri(reader.GetValue<string>("SourcePageAddress"));

            return strip;
        }

        private static object ReadComic(DbDataReader reader)
        {
            Comic comic = new Comic();

            comic.Id = reader.GetValue<long>("Id");
            comic.Name = reader.GetValue<string>("Name");
            comic.IsActive = reader.GetValue<bool>("IsActive");
            comic.FaviconPath = reader.GetValue<string>("FaviconPath");

            return comic;
        }

        private static object ReadComicDefinition(DbDataReader reader)
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

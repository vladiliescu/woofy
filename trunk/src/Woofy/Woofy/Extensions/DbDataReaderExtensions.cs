using System;
using System.Data.Common;
using Woofy.Entities;
using Woofy.DatabaseAccess;

namespace Woofy
{
    public static class DbDataReaderExtensions
    {
        public static T GetValue<T>(this DbDataReader reader, string columnName)
        {
            var value = reader.GetValue(reader.GetOrdinal(columnName));
            if (value == DBNull.Value)
                value = null;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Read<T>(this DbDataReader reader)
            where T : class
        {
            if (!reader.Read())
                return null;

            var type = typeof(T);
            
            if (type == typeof(ComicDefinition))
                return (T)EntityModel.ReadComicDefinition(reader);
            if (type == typeof(Comic))
                return (T)EntityModel.ReadComic(reader);
            if (type == typeof(ComicStrip))
                return (T)EntityModel.ReadComicStrip(reader);

            return null;            
        }        
    }
}

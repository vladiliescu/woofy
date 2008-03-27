using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;
using System.ComponentModel;

using Woofy.Entities;

namespace Woofy.DatabaseAccess
{
    public class DatabaseSession : IDisposable
    {
        private DbTransaction _transaction;
        private DbConnection _connection;
        private TypeConverter _typeConverter = new TypeConverter();

        //TODO: poate ar trebui sa unific dictionarele de mai jos in unul singur, sa zicem EntityData/EntityModel/DatabaseModel/Mappings, care sa contina informatii despre fiecare entitate
        private static Dictionary<Type, string> SelectCommands = new Dictionary<Type, string>();
        private static Dictionary<Type, string> EntityTables = new Dictionary<Type, string>();

        static DatabaseSession()
        {
            SelectCommands.Add(typeof(Comic), @"
SELECT Id, Name, IsActive, FaviconPath
    FROM Comics
");
            SelectCommands.Add(typeof(ComicDefinition), @"
SELECT ComicId, AllowMissingStrips, AllowMultipleStrips, Author, AuthorEmail, FirstStripAddress, HomePageAddress, LatestIssueRegex, NextIssueRegex, SourceFileName, StripRegex
    FROM ComicDefinitions
");
            SelectCommands.Add(typeof(ComicStrip), @"
SELECT Id, ComicId, SourcePageAddress, FilePath
    FROM ComicStrips
");

            EntityTables.Add(typeof(Comic), "Comics");
            EntityTables.Add(typeof(ComicDefinition), "ComicDefinitions");
            EntityTables.Add(typeof(ComicStrip), "ComicStrips");
        }

        #region Static Methods
        public static DatabaseSession Create()
        {
            DatabaseSession session = new DatabaseSession();

            session._connection = new SQLiteConnection(ApplicationSettings.ConnectionString);
            session._connection.Open();
            session._transaction = session._connection.BeginTransaction();

            return session;
        } 
        #endregion

        #region Constructors
        private DatabaseSession()
        {
        } 
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _transaction.Rollback();
                _connection.Close();
            }
        }

        #endregion

        #region Methods
        public DbCommand CreateCommand(string commandText, params DatabaseParameter[] parameters)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = commandText;
            foreach (DatabaseParameter parameter in parameters)
                command.Parameters.Add(parameter.DbParameter);

            return command;
        }

        public DatabaseParameter CreateParameter(string name, object value)
        {
            return CreateParameter(name, value, ParameterPurposes.Regular);
        }

        public DatabaseParameter CreateParameter(string name, object value, ParameterPurposes purpose)
        {
            return CreateParameter(name, value, purpose, null);
        }

        public DatabaseParameter CreateParameter(string name, object value, ParameterPurposes purpose, bool? sortAscending)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = name;

            if (value is string)
                parameter.Value = (string.IsNullOrEmpty((string)value) ? DBNull.Value : value);
            //else if (parameterValue is bool)
            //    command.Parameters.Add((bool)parameterValue ? 1 : 0);
            else
                parameter.Value = value;

            return new DatabaseParameter(parameter, purpose, sortAscending);
        }

        public T GetReaderValue<T>(DbDataReader reader, string columnName)
        {
            object value = reader.GetValue(reader.GetOrdinal(columnName));
            if (value == DBNull.Value)
                value = null;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public void Commit()
        {
            try { _transaction.Commit(); }
            finally { _connection.Close(); }
        }
                
        private string ColumnName(DatabaseParameter parameter)
        {
            return string.Format("{0}{1}", parameter.DbParameter.ParameterName[1].ToString().ToUpper(), parameter.DbParameter.ParameterName.Substring(2));
        }

        private string ParameterName(DatabaseParameter parameter)
        {
            return parameter.DbParameter.ParameterName;
        }

        private string SortDirection(DatabaseParameter parameter)
        {
            if (!parameter.SortAscending.HasValue)
                return "ASC";

            if (parameter.SortAscending.Value)
                return "ASC";

            return "DESC";
        }

        private delegate string ParameterFormatter(DatabaseParameter parameter);

        private string GetFormattedParameters(DatabaseParameter[] parameters, ParameterPurposes purposes, string format, int charactersToTrim, params ParameterFormatter[] formatters)
        {
            StringBuilder builder = new StringBuilder();

            foreach (DatabaseParameter parameter in parameters)
            {
                if ((parameter.Purpose & purposes) != parameter.Purpose)
                    continue;

                string[] values = new string[formatters.Length];

                for (int i = 0; i < formatters.Length; i++)
                    values[i] = formatters[i](parameter);
                

                builder.AppendFormat(format, values);
            }

            if (builder.Length == 0)
                return "";

            return builder.ToString(0, builder.Length - charactersToTrim);
        }

        public long Insert<T>(params DatabaseParameter[] parameters)
        {
            string commandText = string.Format(@"
                INSERT INTO {0} ({1})
                    VALUES ({2});

                SELECT last_insert_rowid();
                ",
                EntityTables[typeof(T)],
                GetFormattedParameters(parameters, ParameterPurposes.Any, "{0}, ", 2, ColumnName),
                GetFormattedParameters(parameters, ParameterPurposes.Any, "{0}, ", 2, ParameterName)
                );

            DbCommand command = CreateCommand(commandText, parameters);

            return (long)command.ExecuteScalar();
        }        

        public int Update<T>(params DatabaseParameter[] parameters)
        {
            string commandText = string.Format(@"
                UPDATE {0}
                    SET {1}
                WHERE {2}
                ",
                EntityTables[typeof(T)],
                GetFormattedParameters(parameters, ParameterPurposes.Regular, "{0} = {1}, ", 2, ColumnName, ParameterName),
                GetFormattedParameters(parameters, ParameterPurposes.Where, "{0} = {1} AND ", 5, ColumnName, ParameterName)
                );

            DbCommand command = CreateCommand(commandText, parameters);
            return command.ExecuteNonQuery();
        }        

        public DbDataReader Select<T>(params DatabaseParameter[] parameters)
        {
            return Select<T>(null, parameters);
        }

        public DbDataReader Select<T>(int? limit, params DatabaseParameter[] parameters)
        {
            string filterText = GetFormattedParameters(parameters, ParameterPurposes.Where, "{0} = {1}, ", 2, ColumnName, ParameterName);
            string orderByText = GetFormattedParameters(parameters, ParameterPurposes.OrderBy, "{0} {1}, ", 2, ColumnName, SortDirection);

            string commandText = string.Format(@"
                {0}
                {1}
                {2}
                {3}
                ", 
                SelectCommands[typeof(T)],
                string.IsNullOrEmpty(filterText) ? "" : "WHERE " + filterText,
                string.IsNullOrEmpty(orderByText) ? "" : "ORDER BY " + orderByText,
                limit.HasValue ? "LIMIT " + limit.Value : ""
                );

            DbCommand command = CreateCommand(commandText, parameters);
            return command.ExecuteReader(CommandBehavior.SingleResult);
        }

        public void Delete<T>(params DatabaseParameter[] parameters)
        {
            string commandText = string.Format(@"
                DELETE FROM {0}
                WHERE {1}
                ",
                 EntityTables[typeof(T)],
                 GetFormattedParameters(parameters, ParameterPurposes.Where, "{0} = {1}, ", 2, ColumnName, ParameterName)
                 );

            DbCommand command = CreateCommand(commandText, parameters);
            command.ExecuteNonQuery();
        }

        #endregion
    }
}

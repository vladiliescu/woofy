using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;
using System.ComponentModel;

using Woofy.Entities;


namespace Woofy.Services
{
    public class Session : IDisposable
    {
        private DbTransaction _transaction;
        private DbConnection _connection;
        private TypeConverter _typeConverter = new TypeConverter();

        #region Static Methods
        public static Session CreateSession()
        {
            Session session = new Session();

            session._connection = new SQLiteConnection(ApplicationSettings.ConnectionString);
            session._transaction = session._connection.BeginTransaction();

            return session;
        } 
        #endregion

        #region Constructors
        private Session()
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

        #region Helper Methods
        private DbCommand CreateCommand(string commandText, params object[] parameterValues)
        {
            DbCommand command = _connection.CreateCommand();

            command.CommandText = commandText;
            foreach (object parameterValue in parameterValues)
            {
                if (parameterValue is string)
                    command.Parameters.Add(string.IsNullOrEmpty((string)parameterValue) ? DBNull.Value : parameterValue);
                else if (parameterValue is bool)
                    command.Parameters.Add((bool)parameterValue ? 1 : 0);
                else
                    command.Parameters.Add(parameterValue);
            }

            return command;
        }

        private T GetReaderValue<T>(DbDataReader reader, string columnName)
        {
            object value = reader.GetValue(reader.GetOrdinal(columnName));
            if (value == DBNull.Value)
                value = null;

            return (T)Convert.ChangeType(value, typeof(T));
        }
        #endregion

        #region Public Methods
        public void Commit()
        {
            _transaction.Commit();
            _connection.Close();
        }

        public void CreateComic(Comic comic)
        {
            DbCommand command = CreateCommand(@"
INSERT INTO [Comics] 
    (Name, IsActive, FaviconPath)
VALUES 
    (?, ?, ?);

SELECT last_insert_rowid();
",
            comic.Name,
            comic.IsActive,
            comic.FaviconPath
            );

            comic.Id = (long)command.ExecuteScalar();

            ComicDefinition definition = comic.Definition;
            DbCommand definitionCommand = CreateCommand(@"
INSERT INTO [ComicDefinitions]
    (ComicId, AllowMissingStrips, AllowMultipleStrips, Author, AuthorEmail, FirstStripAddress, HomePageAddress, LatestIssueRegex, NextIssueRegex, SourceFileName, StripRegex)
VALUES
    (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);
",
            definition.Comic.Id,
            definition.AllowMissingStrips,
            definition.AllowMultipleStrips,
            definition.Author,
            definition.AuthorEmail,
            definition.FirstStripAddress.OriginalString,
            definition.HomePageAddress.OriginalString,
            definition.LatestIssueRegex,
            definition.NextIssueRegex,
            definition.SourceFileName,
            definition.StripRegex
            );

            definitionCommand.ExecuteNonQuery();
        }

        public void UpdateComic(Comic comic)
        {
            DbCommand command = CreateCommand(@"
UPDATE [Comics]
    SET Name = ?, IsActive = ?, FaviconPath = ?
WHERE Id = ?;
",
            comic.Name,
            comic.IsActive,
            comic.FaviconPath,
            comic.Id
            );

            command.ExecuteNonQuery();

            ComicDefinition definition = comic.Definition;
            DbCommand definitionCommand = CreateCommand(@"
UPDATE [ComicDefinitions]
    SET AllowMissingStrips = ?, AllowMultipleStrips = ?, Author = ?, AuthorEmail = ?, 
        FirstStripAddress = ?, HomePageAddress = ?, LatestIssueRegex = ?, SourceFileName = ?, StripRegex = ?
WHERE ComicId = ?;
",
            definition.AllowMissingStrips,
            definition.AllowMultipleStrips,
            definition.Author,
            definition.AuthorEmail,
            definition.FirstStripAddress.OriginalString,
            definition.HomePageAddress.OriginalString,
            definition.LatestIssueRegex,
            definition.NextIssueRegex,
            definition.SourceFileName,
            definition.StripRegex,
            definition.Comic.Id
            );

            definitionCommand.ExecuteNonQuery();
        }

 

        public ComicCollection ReadAllComics()
        {
            ComicCollection comics = new ComicCollection();

            DbCommand comicsCommand = CreateCommand(@"
SELECT Id, Name, IsActive, FaviconPath
    FROM [Comics]
ORDER BY Id
            ");

            DbCommand definitionsCommand = CreateCommand(@"
SELECT ComicId, AllowMissingStrips, AllowMultipleStrips, Author, AuthorEmail, FirstStripAddress, HomePageAddress, LatestIssueRegex, NextIssueRegex, SourceFileName, StripRegex
    FROM [ComicDefinitions]
ORDER BY ComicId
            ");            

            DbDataReader comicsReader = comicsCommand.ExecuteReader(CommandBehavior.SingleResult);
            DbDataReader definitionsReader = definitionsCommand.ExecuteReader(CommandBehavior.SingleResult);
            try
            {
                while (comicsReader.NextResult() && definitionsReader.NextResult())
                {
                    Comic comic = new Comic();

                    comic.Id = GetReaderValue<long>(comicsReader, "Id");
                    comic.Name = GetReaderValue<string>(comicsReader, "Name");
                    comic.IsActive = GetReaderValue<bool>(comicsReader, "IsActive");
                    comic.FaviconPath = GetReaderValue<string>(comicsReader, "FaviconPath");

                    ComicDefinition definition = new ComicDefinition();

                    definition.AllowMissingStrips = GetReaderValue<bool>(definitionsReader, "AllowMissingStrips");
                    definition.AllowMultipleStrips = GetReaderValue<bool>(definitionsReader, "AllowMultipleStrips");
                    definition.Author = GetReaderValue<string>(definitionsReader, "Author");
                    definition.AuthorEmail = GetReaderValue<string>(definitionsReader, "AuthorEmail");
                    definition.FirstStripAddress = new Uri(GetReaderValue<string>(definitionsReader, "FirstStripAddress"));
                    definition.HomePageAddress = new Uri(GetReaderValue<string>(definitionsReader, "HomePageAddress"));
                    definition.LatestIssueRegex = GetReaderValue<string>(definitionsReader, "LatestIssueRegex");
                    definition.NextIssueRegex = GetReaderValue<string>(definitionsReader, "NextIssueRegex");
                    definition.SourceFileName = GetReaderValue<string>(definitionsReader, "SourceFileName");
                    definition.StripRegex = GetReaderValue<string>(definitionsReader, "StripRegex");

                    comic.Definition = definition;
                    definition.Comic = comic;

                    comics.Add(comic);
                }
            }
            finally
            {
                comicsReader.Dispose();
                definitionsReader.Dispose();
            }

            return comics;
        }
        #endregion
    }
}

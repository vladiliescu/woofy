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
            session._connection.Open();
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
        private DbCommand CreateCommand(string commandText, params DbParameter[] parameters)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddRange(parameters);

            return command;
        }

        private DbParameter CreateParameter(string name, object value)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = name;

            if (value is string)
                parameter.Value = (string.IsNullOrEmpty((string)value) ? DBNull.Value : value);
            //else if (parameterValue is bool)
            //    command.Parameters.Add((bool)parameterValue ? 1 : 0);
            else
                parameter.Value = value;

            return parameter;
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
            try { _transaction.Commit(); }
            finally { _connection.Close(); }
        }

        public void CreateComic(Comic comic)
        {
            DbCommand command = CreateCommand(@"
INSERT INTO [Comics] 
    (Name, IsActive, FaviconPath)
VALUES 
    (:name, :isActive, :faviconPath);

SELECT last_insert_rowid();
",
            CreateParameter(":name", comic.Name),
            CreateParameter(":isActive", comic.IsActive),
            CreateParameter(":faviconPath", comic.FaviconPath)
            );

            comic.Id = (long)command.ExecuteScalar();

            ComicDefinition definition = comic.Definition;
            DbCommand definitionCommand = CreateCommand(@"
INSERT INTO [ComicDefinitions]
    (ComicId, AllowMissingStrips, AllowMultipleStrips, Author, AuthorEmail, FirstStripAddress, HomePageAddress, LatestIssueRegex, NextIssueRegex, SourceFileName, StripRegex)
VALUES
    (:comicId, :allowMissingStrips, :allowMultipleStrips, :author, :authorEmail, :firstStripAddress, :homePageAddress, :latestIssueRegex, :nextIssueRegex, :sourceFileName, :stripRegex);
",
            CreateParameter(":comicId", definition.Comic.Id),
            CreateParameter(":allowMissingStrips", definition.AllowMissingStrips),
            CreateParameter(":allowMultipleStrips", definition.AllowMultipleStrips),
            CreateParameter(":author", definition.Author),
            CreateParameter(":authorEmail", definition.AuthorEmail),
            CreateParameter(":firstStripAddress", definition.FirstStripAddress == null ? null : definition.FirstStripAddress.OriginalString),
            CreateParameter(":homePageAddress", definition.HomePageAddress == null ? null : definition.HomePageAddress.OriginalString),
            CreateParameter(":latestIssueRegex", definition.LatestIssueRegex),
            CreateParameter(":nextIssueRegex", definition.NextIssueRegex),
            CreateParameter(":sourceFileName", definition.SourceFileName),
            CreateParameter(":stripRegex", definition.StripRegex)
            );

            definitionCommand.ExecuteNonQuery();
        }

        public void UpdateComic(Comic comic)
        {
            DbCommand command = CreateCommand(@"
UPDATE [Comics]
    SET Name = :name, IsActive = :isActive, FaviconPath = :faviconPath
WHERE Id = :id;
",
            CreateParameter(":name", comic.Name),
            CreateParameter(":isActive", comic.IsActive),
            CreateParameter(":faviconPath", comic.FaviconPath),
            CreateParameter(":id", comic.Id)
            );

            command.ExecuteNonQuery();

            ComicDefinition definition = comic.Definition;
            DbCommand definitionCommand = CreateCommand(@"
UPDATE [ComicDefinitions]
    SET AllowMissingStrips = :allowMissingStrips, AllowMultipleStrips = :allowMultipleStrips, Author = :author, AuthorEmail = :authorEmail, 
        FirstStripAddress = :firstStripAddress, HomePageAddress = :homePageAddress, LatestIssueRegex = :latestIssueRegex, SourceFileName = :sourceFileName, StripRegex = :stripRegex
WHERE ComicId = :comicId;
",
            CreateParameter(":allowMissingStrips", definition.AllowMissingStrips),
            CreateParameter(":allowMultipleStrips", definition.AllowMultipleStrips),
            CreateParameter(":author", definition.Author),
            CreateParameter(":authorEmail", definition.AuthorEmail),
            CreateParameter(":firstStripAddress", definition.FirstStripAddress == null ? null : definition.FirstStripAddress.OriginalString),
            CreateParameter(":homePageAddress", definition.HomePageAddress.OriginalString),
            CreateParameter(":latestIssueRegex", definition.LatestIssueRegex),
            CreateParameter(":nextIssueRegex", definition.NextIssueRegex),
            CreateParameter(":sourceFileName", definition.SourceFileName),
            CreateParameter(":stripRegex", definition.StripRegex),
            CreateParameter(":comicId", definition.Comic.Id)
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
                while (comicsReader.Read() && definitionsReader.Read())
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
                    //definition.FirstStripAddress = new Uri(GetReaderValue<string>(definitionsReader, "FirstStripAddress"));
                    definition.HomePageAddress = new Uri(GetReaderValue<string>(definitionsReader, "HomePageAddress"));
                    definition.LatestIssueRegex = GetReaderValue<string>(definitionsReader, "LatestIssueRegex");
                    definition.NextIssueRegex = GetReaderValue<string>(definitionsReader, "NextIssueRegex");
                    definition.SourceFileName = GetReaderValue<string>(definitionsReader, "SourceFileName");
                    definition.StripRegex = GetReaderValue<string>(definitionsReader, "StripRegex");

                    comic.AssociateWithDefinition(definition);

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

        public ComicDefinitionCollection ReadAllDefinitions()
        {
            ComicDefinitionCollection definitions = new ComicDefinitionCollection();

            DbCommand command = CreateCommand(@"
SELECT ComicId, AllowMissingStrips, AllowMultipleStrips, Author, AuthorEmail, FirstStripAddress, HomePageAddress, LatestIssueRegex, NextIssueRegex, SourceFileName, StripRegex
    FROM [ComicDefinitions]
ORDER BY ComicId
            ");

            using (DbDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
            {
                while (reader.Read())
                {
                    ComicDefinition definition = new ComicDefinition();
                    Comic comic = new Comic();
                    definition.AssociateWithComic(comic);

                    comic.Id = GetReaderValue<long>(reader, "ComicId");
                    definition.AllowMissingStrips = GetReaderValue<bool>(reader, "AllowMissingStrips");
                    definition.AllowMultipleStrips = GetReaderValue<bool>(reader, "AllowMultipleStrips");
                    definition.Author = GetReaderValue<string>(reader, "Author");
                    definition.AuthorEmail = GetReaderValue<string>(reader, "AuthorEmail");
                    //definition.FirstStripAddress = new Uri(GetReaderValue<string>(reader, "FirstStripAddress"));
                    definition.HomePageAddress = new Uri(GetReaderValue<string>(reader, "HomePageAddress"));
                    definition.LatestIssueRegex = GetReaderValue<string>(reader, "LatestIssueRegex");
                    definition.NextIssueRegex = GetReaderValue<string>(reader, "NextIssueRegex");
                    definition.SourceFileName = GetReaderValue<string>(reader, "SourceFileName");
                    definition.StripRegex = GetReaderValue<string>(reader, "StripRegex");

                    definitions.Add(definition);
                }
            }            

            return definitions;
        }

        public void UpdateDefinition(ComicDefinition definition)
        {
            DbCommand command = CreateCommand(@"
UPDATE [ComicDefinitions]
    SET AllowMissingStrips = :allowMissingStrips, AllowMultipleStrips = :allowMultipleStrips, Author = :author, AuthorEmail = :authorEmail, 
        FirstStripAddress = :firstStripAddress, HomePageAddress = :homePageAddress, LatestIssueRegex = :latestIssueRegex, SourceFileName = :sourceFileName, StripRegex = :stripRegex
WHERE ComicId = :comicId;
",
            CreateParameter(":allowMissingStrips", definition.AllowMissingStrips),
            CreateParameter(":allowMultipleStrips", definition.AllowMultipleStrips),
            CreateParameter(":author", definition.Author),
            CreateParameter(":authorEmail", definition.AuthorEmail),
            CreateParameter(":firstStripAddress", definition.FirstStripAddress == null ? null : definition.FirstStripAddress.OriginalString),
            CreateParameter(":homePageAddress", definition.HomePageAddress.OriginalString),
            CreateParameter(":latestIssueRegex", definition.LatestIssueRegex),
            CreateParameter(":nextIssueRegex", definition.NextIssueRegex),
            CreateParameter(":sourceFileName", definition.SourceFileName),
            CreateParameter(":stripRegex", definition.StripRegex),
            CreateParameter(":comicId", definition.Comic.Id)
            );

            command.ExecuteNonQuery();
        }

        public void CreateComicStrip(ComicStrip comicStrip)
        {
            DbCommand command = CreateCommand(@"
INSERT INTO ComicStrips (ComicId, SourcePageAddress, FilePath)
    VALUES (:comicId, :sourcePageAddress, :filePath)

SELECT last_insert_rowid();
",
            CreateParameter(":comicId", comicStrip.Comic.Id),
            CreateParameter(":sourcePageAddress", comicStrip.SourcePageAddress),
            CreateParameter(":filePath", comicStrip.FilePath)
            );

            comicStrip.Id = (long)command.ExecuteScalar();
        }
        #endregion


        
    }
}

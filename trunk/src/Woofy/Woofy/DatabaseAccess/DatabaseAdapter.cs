using System;
using System.Collections.Generic;
using System.Text;
using Woofy.Entities;
using System.Data.Common;
using System.Data;

namespace Woofy.DatabaseAccess
{
    public class DatabaseAdapter
    {
        public ComicCollection ReadAllComics()
        {
            ComicCollection comics = new ComicCollection();

            using (DatabaseSession session = DatabaseSession.Create())
            {
                DbDataReader comicsReader = session.Select<Comic>(session.CreateParameter(":id", null, ParameterPurposes.OrderBy));
                DbDataReader definitionsReader = session.Select<ComicDefinition>(session.CreateParameter(":comicId", null, ParameterPurposes.OrderBy));
                try
                {
                    Comic comic = new Comic();
                    ComicDefinition definition = new ComicDefinition();

                    while ((comic = comicsReader.Read<Comic>()) != null &&
                            (definition = definitionsReader.Read<ComicDefinition>()) != null)
                    {
                        comic.AssociateWithDefinition(definition);
                        comics.Add(comic);
                    }
                }
                finally
                {
                    comicsReader.Dispose();
                    definitionsReader.Dispose();
                }                
            }

            return comics;
        }

        public void InsertStrip(ComicStrip strip)
        {
            using (DatabaseSession session = DatabaseSession.Create())
            {
                strip.Id = session.Insert<ComicStrip>(
                                session.CreateParameter(":comicId", strip.Comic.Id),
                                session.CreateParameter(":sourcePageAddress", strip.SourcePageAddress),
                                session.CreateParameter(":filePath", strip.FilePath)
                                );

                session.Commit();
            }
        }        

        public void InsertOrUpdateComicAndDefinition(ComicDefinition definition)
        {
            using (DatabaseSession session = DatabaseSession.Create())
            {
                ComicDefinition existingDefinition = null;
                using (DbDataReader reader =
                            session.Select<ComicDefinition>(
                                session.CreateParameter(":sourceFileName", definition.SourceFileName, ParameterPurposes.Where)
                                ))
                {
                    existingDefinition = reader.Read<ComicDefinition>();
                }


                Comic comic = definition.Comic;
                if (existingDefinition == null)
                {
                    long comicId = session.Insert<Comic>(
                                    session.CreateParameter(":name", comic.Name),
                                    session.CreateParameter(":isActive", comic.IsActive),
                                    session.CreateParameter(":faviconPath", comic.IconPath)
                                    );

                    session.Insert<ComicDefinition>(
                        session.CreateParameter(":comicId", comicId),
                        session.CreateParameter(":allowMissingStrips", definition.AllowMissingStrips),
                        session.CreateParameter(":allowMultipleStrips", definition.AllowMultipleStrips),
                        session.CreateParameter(":author", definition.Author),
                        session.CreateParameter(":authorEmail", definition.AuthorEmail),
                        session.CreateParameter(":firstStripAddress", definition.FirstStripAddress == null ? null : definition.FirstStripAddress.OriginalString),
                        session.CreateParameter(":homePageAddress", definition.HomePageAddress == null ? null : definition.HomePageAddress.OriginalString),
                        session.CreateParameter(":latestIssueRegex", definition.LatestIssueRegex),
                        session.CreateParameter(":nextIssueRegex", definition.NextIssueRegex),
                        session.CreateParameter(":sourceFileName", definition.SourceFileName),
                        session.CreateParameter(":stripRegex", definition.StripRegex)
                        );
                }
                else
                {
                    session.Update<Comic>(
                        session.CreateParameter(":name", comic.Name),
                        session.CreateParameter(":id", existingDefinition.ComicId, ParameterPurposes.Where)
                        );

                    session.Update<ComicDefinition>(
                        session.CreateParameter(":allowMissingStrips", definition.AllowMissingStrips),
                        session.CreateParameter(":allowMultipleStrips", definition.AllowMultipleStrips),
                        session.CreateParameter(":author", definition.Author),
                        session.CreateParameter(":authorEmail", definition.AuthorEmail),
                        session.CreateParameter(":firstStripAddress", definition.FirstStripAddress == null ? null : definition.FirstStripAddress.OriginalString),
                        session.CreateParameter(":homePageAddress", definition.HomePageAddress == null ? null : definition.HomePageAddress.OriginalString),
                        session.CreateParameter(":latestIssueRegex", definition.LatestIssueRegex),
                        session.CreateParameter(":nextIssueRegex", definition.NextIssueRegex),
                        session.CreateParameter(":stripRegex", definition.StripRegex),
                        session.CreateParameter(":sourceFileName", definition.SourceFileName),
                        session.CreateParameter(":comicId", existingDefinition.ComicId, ParameterPurposes.Where)
                        );
                }

                session.Commit();
            }
        }

        public ComicStripCollection ReadStripsForComic(Comic comic)
        {
            ComicStripCollection strips = new ComicStripCollection();

            using (DatabaseSession session = DatabaseSession.Create())
            {
                using (DbDataReader reader = session.Select<ComicStrip>(
                                        session.CreateParameter(":comicId", comic.Id, ParameterPurposes.Where),
                                        session.CreateParameter(":id", null, ParameterPurposes.OrderBy)
                                        ))
                {
                    ComicStrip strip;
                    while ((strip = reader.Read<ComicStrip>()) != null)
                    {
                        strips.Add(strip);
                    }
                }

            }

            return strips;
        }

        public ComicStrip ReadMostRecentStrip(Comic comic)
        {
            using (DatabaseSession session = DatabaseSession.Create())
            {
                using (DbDataReader reader = session.Select<ComicStrip>(
                                                1,
                                                session.CreateParameter(":comicId", comic.Id, ParameterPurposes.Where),
                                                session.CreateParameter(":id", null, ParameterPurposes.OrderBy, false)
                                                ))
                {
                    return reader.Read<ComicStrip>();
                }
            }
        }

        //TODO: as vrea sa scriu ceva in genul:
        /*
         * using (new DatabaseSession())
         * {
         *      Session.Delete<ComicStrip>(
         *          CreateParameter(Model.ComicStrip.Id, strip.Id, ParameterPurposes.Where)
         *      );
         * }
         * 
         * (si fara commit)
         */
        public void DeleteStrip(ComicStrip strip)
        {
            using (DatabaseSession session = DatabaseSession.Create())
            {
                session.Delete<ComicStrip>(
                    session.CreateParameter(":id", strip.Id, ParameterPurposes.Where)
                    );

                session.Commit();
            }
        }
    }
}

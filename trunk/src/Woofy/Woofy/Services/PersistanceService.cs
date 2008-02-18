using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

using System.Data.SQLite;

using Woofy.Entities;
using System.Data;

namespace Woofy.Services
{
    public class PersistanceService
    {
        public void RefreshDatabaseComics(ComicDefinitionCollection definitions)
        {
            using (Session session = Session.CreateSession())
            {
                ComicDefinitionCollection databaseDefinitions = session.ReadAllDefinitions();
                
                foreach (ComicDefinition definition in definitions)
                {
                    ComicDefinition databaseDefinition = databaseDefinitions.FindBySourceFileName(definition.SourceFileName);
                    if (databaseDefinition == null)
                    {
                        session.CreateComic(definition.Comic);
                    }
                    else
                    {
                        definition.AssociateWithComic(databaseDefinition.Comic);
                        session.UpdateDefinition(definition);
                    }
                }

                session.Commit();
            }
        }

        public ComicCollection ReadAllComics()
        {
            using (Session session = Session.CreateSession())
            {
                return session.ReadAllComics();
            }
        }

        public void CreateComicStrip(ComicStrip comicStrip)
        {
            using (Session session = Session.CreateSession())
            {
                session.CreateComicStrip(comicStrip);
            }
        }
    }
}

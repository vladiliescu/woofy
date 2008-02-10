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
        public void RefreshDatabaseComics(ComicCollection comics)
        {
            using (Session session = Session.CreateSession())
            {
                ComicCollection databaseComics = session.ReadAllComics();
                
                foreach (Comic comic in comics)
                {
                    Comic databaseComic = databaseComics.FindBySourceFileName(comic.Definition.SourceFileName);
                    if (databaseComic == null)
                    {
                        databaseComics.Add(comic);
                        session.CreateComic(comic);
                    }
                    else
                    {
                        comic.Definition.CopyTo(databaseComic.Definition);
                        session.UpdateComic(databaseComic);
                    }
                }

                session.Commit();
            }
        }
    }
}

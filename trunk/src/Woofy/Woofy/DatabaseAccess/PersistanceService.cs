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
        //public void RefreshDatabaseComics(ComicDefinitionCollection definitions)
        //{
        //    using (DatabaseSession session = DatabaseSession.CreateSession())
        //    {
        //        ComicDefinitionCollection databaseDefinitions = session.ReadAllDefinitions();
                
        //        foreach (ComicDefinition definition in definitions)
        //        {
        //            ComicDefinition databaseDefinition = databaseDefinitions.FindBySourceFileName(definition.SourceFileName);
        //            if (databaseDefinition == null)
        //            {
        //                session.InsertComic(definition.Comic);
        //                session.InsertDefinition(definition);
        //            }
        //            else
        //            {
        //                definition.AssociateWithComic(databaseDefinition.Comic);
        //                session.UpdateDefinition(definition);
        //            }
        //        }

        //        session.Commit();
        //    }
        //}
    }
}

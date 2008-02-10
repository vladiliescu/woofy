using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Services
{
    public interface IComicPersistanceService
    {
        void RefreshDatabaseComics(Woofy.Entities.ComicCollection Comics);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections;

using Woofy.Entities;
using Woofy.Services;
using Woofy.Views;

namespace Woofy.Controllers
{
    public class ComicsPresenter : IPresenter
    {
        public ComicCollection Comics { get; private set; }
        private IComicPersistanceService _comicPersistanceService;
        private IComicDefinitionsService _comicDefinitionService;

        public void RunApplication()
        {
            _comicDefinitionService = new ComicDefinitionsService();
            Comics = _comicDefinitionService.BuildComicsFromDefinitions();

            SelectComics startWindow = new SelectComics(this);
            Application application = new Application();
            application.Run(startWindow);
        }

        public void ActivateComics(IList comicsToActivate)
        {
            foreach (Comic comic in comicsToActivate)
                comic.IsActive = true;
        }

        public void DeactivateComics(IList comicsToDeactivate)
        {
            foreach (Comic comic in comicsToDeactivate)
                comic.IsActive = false;
        }

        public void PersistComicsChanges()
        {
        }

        public void DiscardComicsChanges()
        {
        }
    }
}

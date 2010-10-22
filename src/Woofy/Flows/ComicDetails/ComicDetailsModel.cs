namespace Woofy.Flows.ComicDetails
{
    public class ComicDetailsViewModel
    {
        public ComicModel[] AvailableComics { get; private set; }

        public ComicDetailsViewModel(ComicModel[] availableComics)
        {
            AvailableComics = availableComics;
        }

        public class ComicModel
        {
            public string Name { get; set; }
            public string DefaultDownloadFolder { get; set; }

            public ComicModel(string name, string defaultDownloadFolder)
            {
                Name = name;
                DefaultDownloadFolder = defaultDownloadFolder;
            }
        }
    }
}
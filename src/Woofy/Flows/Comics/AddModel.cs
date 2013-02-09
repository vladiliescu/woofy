namespace Woofy.Flows.Comics
{
	public class AddViewModel
	{
		public ComicModel[] AvailableComics { get; private set; }

		public AddViewModel(ComicModel[] availableComics)
		{
			AvailableComics = availableComics;
		}

		public class ComicModel
		{
			public string Name { get; private set; }
			public string Id { get; private set; }

			public ComicModel(string id, string name)
			{
				Id = id;
				Name = name;
			}
		}
	}

    public class AddInputModel
    {
        public string ComicId { get; private set; }
        public bool PrependIndexToStrips { get; private set; }
        public bool EmbedMetadata { get; private set; }

        public AddInputModel(string comicId, bool prependIndexToStrips, bool embedMetadata)
        {
            ComicId = comicId;
            PrependIndexToStrips = prependIndexToStrips;
            EmbedMetadata = embedMetadata;
        }
    }
}
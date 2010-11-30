namespace Woofy.Flows.AddComic
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
			public string Name { get; private set; }
			public string Id { get; private set; }

			public ComicModel(string id, string name)
			{
				Id = id;
				Name = name;
			}
		}
	}
}
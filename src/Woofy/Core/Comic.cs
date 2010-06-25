namespace Woofy.Core
{
    public class Comic
    {
    	public DownloadOutcome DownloadOutcome { get; set; }
    	public string Name { get; private set; }
    	public long DownloadedComics { get; set; }
    	public string DownloadFolder { get; private set; }
    	public TaskStatus Status { get; set; }
    	public string CurrentUrl { get; set; }
		public bool RandomPausesBetweenRequests { get; set; }
    	public ComicDefinition Definition { get; set; }

    	/// <summary>
		/// Used for the Json.NET deserialization
		/// </summary>
		public Comic()
		{
		}

    	public Comic(string name, ComicDefinition definition, string downloadFolder, string currentUrl)
			: this(name, definition, downloadFolder, currentUrl, false)
        {
        }

		public Comic(string name, ComicDefinition definition, string downloadFolder, string currentUrl, bool randomPausesBetweenRequests)
			: this(name, definition, 0, downloadFolder, currentUrl, TaskStatus.Running, randomPausesBetweenRequests)
		{
		}

		private Comic(string name, ComicDefinition definition, long downloadedComics, string downloadFolder, string currentUrl, TaskStatus status, bool randomPausesBetweenRequests)
        {
            Name = name;
			Definition = definition;
            DownloadedComics = downloadedComics;
            DownloadFolder = downloadFolder;
            CurrentUrl = currentUrl;
            Status = status;
			RandomPausesBetweenRequests = randomPausesBetweenRequests;
        }

#warning: ensure that the definition's name can represent a valid download folder; i could try and sanitize the name before setting the download folder.
    	public Comic(ComicDefinition definition)
			: this(definition.Name, definition, definition.Name, null)
    	{
    	}

    	public override string ToString()
        {
            return Name;
        }
    }
}

using Newtonsoft.Json;

namespace Woofy.Core
{
    public class Comic
    {
    	public DownloadOutcome DownloadOutcome { get; set; }
    	public string Name { get; private set; }
    	public string ComicInfoFile { get; private set; }
    	public long DownloadedComics { get; set; }
    	public string DownloadFolder { get; private set; }
    	public TaskStatus Status { get; set; }
    	public string CurrentUrl { get; set; }
		public bool RandomPausesBetweenRequests { get; set; }
		[JsonIgnore]
    	public ComicDefinition Definition { get; set; }

    	/// <summary>
		/// Used for the Json.NET deserialization
		/// </summary>
		public Comic()
		{
		}

    	public Comic(string name, string comicInfoFile, string downloadFolder, string currentUrl)
            : this(name, comicInfoFile, downloadFolder, currentUrl, false)
        {
        }

		public Comic(string name, string comicInfoFile, string downloadFolder, string currentUrl, bool randomPausesBetweenRequests)
			: this(name, comicInfoFile, 0, downloadFolder, currentUrl, TaskStatus.Running, randomPausesBetweenRequests)
		{
		}

        private Comic(string name, string comicInfoFile, long downloadedComics, string downloadFolder, string currentUrl, TaskStatus status, bool randomPausesBetweenRequests)
        {
            Name = name;
            ComicInfoFile = comicInfoFile;
			Definition = new ComicDefinition(comicInfoFile);
            DownloadedComics = downloadedComics;
            DownloadFolder = downloadFolder;
            CurrentUrl = currentUrl;
            Status = status;
			RandomPausesBetweenRequests = randomPausesBetweenRequests;
        }

		//TODO: ensure that the definition's name can represent a valid download folder;
    	public Comic(ComicDefinition definition)
			: this(definition.Name, definition.ComicInfoFile, definition.Name, null)
    	{
    	}

    	public override string ToString()
        {
            return Name;
        }
    }
}

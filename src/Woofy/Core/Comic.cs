using System;

namespace Woofy.Core
{
    public class Comic
    {
    	public long Id { get; private set; }

    	public DownloadOutcome DownloadOutcome { get; set; }

    	public string Name { get; private set; }

    	public string ComicInfoFile { get; private set; }

    	public long DownloadedComics { get; set; }

    	public long? ComicsToDownload { get; private set; }

    	public string DownloadFolder { get; private set; }

    	public long OrderNumber { get; set; }

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

    	public Comic(string name, string comicInfoFile, long? comicsToDownload, string downloadFolder, string currentUrl)
            : this(name, comicInfoFile, comicsToDownload, downloadFolder, currentUrl, false)
        {
        }

		public Comic(string name, string comicInfoFile, long? comicsToDownload, string downloadFolder, string currentUrl, bool randomPausesBetweenRequests)
			: this(-1, name, comicInfoFile, 0, comicsToDownload, downloadFolder, 1, currentUrl, TaskStatus.Running, randomPausesBetweenRequests)
		{
		}

        private Comic(long id, string name, string comicInfoFile, long downloadedComics, long? comicsToDownload, string downloadFolder, long orderNumber, string currentUrl, TaskStatus status, bool randomPausesBetweenRequests)
        {
            Id = id;
            Name = name;
            ComicInfoFile = comicInfoFile;
            DownloadedComics = downloadedComics;
            ComicsToDownload = comicsToDownload;
            DownloadFolder = downloadFolder;
            OrderNumber = orderNumber;
            CurrentUrl = currentUrl;
            Status = status;
			RandomPausesBetweenRequests = randomPausesBetweenRequests;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Id, Name);
        }
    }
}

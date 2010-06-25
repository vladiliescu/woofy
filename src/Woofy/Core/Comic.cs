using System.IO;
using Newtonsoft.Json;

namespace Woofy.Core
{
    public class Comic
    {
		/// <summary>
		/// The definition's filename. It uniquely identifies a comic/definition.
		/// </summary>
		public string DefinitionFilename { get; set; }

    	public DownloadOutcome DownloadOutcome { get; set; }
    	public string Name { get; private set; }
    	public long DownloadedComics { get; set; }
    	public string DownloadFolder { get; private set; }
    	public TaskStatus Status { get; set; }
    	public string CurrentUrl { get; set; }
		public bool RandomPausesBetweenRequests { get; set; }

		[JsonIgnore]
    	public ComicDefinition Definition { get; set; }

#warning This should be merged with Status, once the whole thing is stable.
		/// <summary>
		/// Gets or sets whether the comic is active or not.
		/// </summary>
    	public bool IsActive { get; set; }

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
			DefinitionFilename = definition.Filename;
            DownloadedComics = downloadedComics;
            DownloadFolder = downloadFolder;
            CurrentUrl = currentUrl;
            Status = status;
			RandomPausesBetweenRequests = randomPausesBetweenRequests;
        }

    	public Comic(ComicDefinition definition)
    	{
			Name = definition.Name;
			Definition = definition;
			DefinitionFilename = definition.Filename;
#warning the download folder should be combined with the default download folder
			DownloadFolder = Path.GetFileNameWithoutExtension(definition.Filename);
			Status = TaskStatus.Running;
    	}

    	public override string ToString()
        {
            return Name;
        }

    	public bool Equals(Comic other)
    	{
    		if (ReferenceEquals(null, other)) return false;
    		if (ReferenceEquals(this, other)) return true;
    		return Equals(other.Definition, Definition);
    	}

    	public override bool Equals(object obj)
    	{
    		if (ReferenceEquals(null, obj)) return false;
    		if (ReferenceEquals(this, obj)) return true;
    		if (obj.GetType() != typeof (Comic)) return false;
    		return Equals((Comic) obj);
    	}

    	public override int GetHashCode()
    	{
    		return (Definition != null ? Definition.GetHashCode() : 0);
    	}

    	public static bool operator ==(Comic left, Comic right)
    	{
    		return Equals(left, right);
    	}

    	public static bool operator !=(Comic left, Comic right)
    	{
    		return !Equals(left, right);
    	}
    }
}

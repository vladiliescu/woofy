using System;
using System.IO;
using Woofy.Core.SystemProxies;

namespace Woofy.Core
{
	public interface IFileDownloader
	{
		void Download(Uri address, string fileName);
	}

    /// <summary>
    /// Downloads one or more files to a specified directory.
    /// </summary>
    public class FileDownloader : IFileDownloader
    {
		private readonly IWebClientProxy webClient;
		private readonly IDirectoryProxy directory;

    	public FileDownloader(IWebClientProxy webClient, IDirectoryProxy directory)
    	{
    		this.webClient = webClient;
    		this.directory = directory;
    	}

    	public void Download(Uri address, string fileName)
    	{
#warning it should check for duplicates.
			EnsureFolderExists(fileName);

    		webClient.Download(address, fileName);
    	}

    	private void EnsureFolderExists(string fileName)
    	{
			var dir = Path.GetDirectoryName(fileName);
			if (directory.Exists(dir))
				return;

			directory.CreateDirectory(dir);
    	}
    }
}
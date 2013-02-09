using System;
using System.IO;
using Woofy.Core.SystemProxies;

namespace Woofy.Core.Engine
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
        private readonly IFileProxy file;

    	public FileDownloader(IWebClientProxy webClient, IDirectoryProxy directory, IFileProxy file)
    	{
    		this.webClient = webClient;
    	    this.file = file;
    	    this.directory = directory;
    	}

    	public void Download(Uri address, string fileName)
    	{
			EnsureFolderExists(fileName);

            //in case Woofy gets shut down during the download process - prevents incomplete files from being created
            var tempFile = Path.GetTempFileName();
    		webClient.Download(address, tempFile);
            
            file.Move(tempFile, fileName);
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
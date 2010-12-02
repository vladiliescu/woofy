using System;
using System.IO;
using Woofy.Core.ComicManagement;

namespace Woofy.Core
{
    public interface IComicPath
    {
        string DownloadFolderFor(Comic comic);
        string DownloadPathFor(Comic comic, string fileName);
        string DownloadFolderFor(string comicId);
    	string DownloadPathFor(string comicId, string fileName);
        string FileNameFor(Uri link);
        string DownloadPathFor(string comicId, Uri link);
    }

    public class ComicPath : IComicPath
    {
        private readonly IUserSettings userSettings;

        public ComicPath(IUserSettings userSettings)
        {
            this.userSettings = userSettings;
        }

        public string DownloadFolderFor(string comicId)
        {
            if (string.IsNullOrEmpty(userSettings.DownloadFolder))
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, comicId);

            return Path.Combine(userSettings.DownloadFolder, comicId);
        }

		public string DownloadPathFor(string comicId, string fileName)
		{
			return Path.Combine(DownloadFolderFor(comicId), fileName);
		}

        public string FileNameFor(Uri link)
        {
            return WebPath.GetFileName(link);
        }

        public string DownloadPathFor(string comicId, Uri link)
        {
            return DownloadPathFor(comicId, FileNameFor(link));
        }

        public string DownloadFolderFor(Comic comic)
        {
            if (string.IsNullOrEmpty(userSettings.DownloadFolder))
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, comic.Id);

            return Path.Combine(userSettings.DownloadFolder, comic.Id);
        }

        public string DownloadPathFor(Comic comic, string fileName)
        {
            return Path.Combine(DownloadFolderFor(comic), fileName);
        }
    }
}
using System;
using System.IO;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;

namespace Woofy.Core
{
    public interface IComicPath
    {
        string DownloadFolderFor(Comic comic);
        string DownloadPathFor(Comic comic, string fileName);
        string DownloadFolderFor(string comicId);
    	string DownloadPathFor(string comicId, string fileName);
        string FileNameFor(string comicId, Uri link);
        string DownloadPathFor(string comicId, Uri link);
        string DownloadFolderRoot();
    }

    public class ComicPath : IComicPath
    {
        private readonly IUserSettings userSettings;
        private readonly IComicStore comicStore;

        public ComicPath(IUserSettings userSettings, IComicStore comicStore)
        {
            this.userSettings = userSettings;
            this.comicStore = comicStore;
        }

        public string DownloadFolderRoot()
        {
            if (string.IsNullOrEmpty(userSettings.DownloadFolder))
                return AppDomain.CurrentDomain.BaseDirectory;

            return userSettings.DownloadFolder;
        }

        public string DownloadFolderFor(string comicId)
        {
            return Path.Combine(DownloadFolderRoot(), comicId);
        }

		public string DownloadPathFor(string comicId, string fileName)
		{
			return Path.Combine(DownloadFolderFor(comicId), fileName);
		}

        public string FileNameFor(string comicId, Uri link)
        {
            var comic = comicStore.Find(comicId);
            var rawFileName = WebPath.GetFileName(link);

            if (!comic.PrependIndexToStrips)
                return rawFileName;

            return "{0:0000}_{1}".FormatTo(comic.DownloadedStrips + 1, rawFileName);
        }

        public string DownloadPathFor(string comicId, Uri link)
        {
            return DownloadPathFor(comicId, FileNameFor(comicId, link));
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
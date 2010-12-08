using System;
using System.IO;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.SystemProxies;

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
        void EnsureDownloadFolderExistsFor(string comicId);
    }

    public class ComicPath : IComicPath
    {
        private readonly IUserSettings userSettings;
        private readonly IComicStore comicStore;
        private readonly IDirectoryProxy directory;

        public ComicPath(IUserSettings userSettings, IComicStore comicStore, IDirectoryProxy directory)
        {
            this.userSettings = userSettings;
            this.directory = directory;
            this.comicStore = comicStore;
        }

        public string DownloadFolderRoot()
        {
            if (string.IsNullOrEmpty(userSettings.DownloadFolder))
                return AppDomain.CurrentDomain.BaseDirectory;

            return userSettings.DownloadFolder;
        }

        public void EnsureDownloadFolderExistsFor(string comicId)
        {
            var dir = DownloadFolderFor(comicId);
            if (directory.Exists(dir))
                return;

            directory.CreateDirectory(dir);
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
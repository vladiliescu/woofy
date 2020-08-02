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
        string DownloadPathFor(string comicId, Uri link);
        string DownloadFolderRoot();
        void EnsureDownloadFolderExistsFor(string comicId);
        //HACK: added so I could check whether the current strip has been downloaded before, while the PrependIndexToStrip option is selected
        string DownloadPathForPreviousIndex(string comicId, Uri link);
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

        private string FileNameFor(string comicId, Uri link)
        {
            return FileNameFor(comicId, link, 1);
        }

        private string FileNameFor(string comicId, Uri link, int indexOffset)
        {
            var comic = comicStore.Find(comicId);
            var rawFileName = link.Segments[link.Segments.Length - 1];
            var windowsSafeFileName = ReplaceIllegalCharactersInFileName(rawFileName);

            if (!comic.PrependIndexToStrips)
                return windowsSafeFileName;

            return "{0:0000}_{1}".FormatTo(comic.DownloadedStrips + indexOffset, windowsSafeFileName);
        }

        private string ReplaceIllegalCharactersInFileName(string fileName)
        {
            //windows illegal characters are \/:*?"<>|
            return fileName.Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_');
        }

        public string DownloadPathFor(string comicId, Uri link)
        {
            return DownloadPathFor(comicId, FileNameFor(comicId, link));
        }

        public string DownloadPathForPreviousIndex(string comicId, Uri link)
        {
            return DownloadPathFor(comicId, FileNameFor(comicId, link, 0));
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
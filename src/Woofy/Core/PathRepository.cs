using System;
using System.IO;
using Woofy.Core.ComicManagement;

namespace Woofy.Core
{
    public interface IPathRepository
    {
        string DownloadFolderFor(Comic comic);
        string DownloadPathFor(Comic comic, string fileName);
        string DownloadFolderFor(string comicId);
    }

    public class PathRepository : IPathRepository
    {
        private readonly IUserSettings userSettings;

        public PathRepository(IUserSettings userSettings)
        {
            this.userSettings = userSettings;
        }

        public string DownloadFolderFor(string comicId)
        {
            if (string.IsNullOrEmpty(userSettings.DefaultDownloadFolder))
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, comicId);

            return Path.Combine(userSettings.DefaultDownloadFolder, comicId);
        }

        public string DownloadFolderFor(Comic comic)
        {
            if (string.IsNullOrEmpty(userSettings.DefaultDownloadFolder))
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, comic.Id);

            return Path.Combine(userSettings.DefaultDownloadFolder, comic.Id);
        }

        public string DownloadPathFor(Comic comic, string fileName)
        {
            return Path.Combine(DownloadFolderFor(comic), fileName);
        }
    }
}
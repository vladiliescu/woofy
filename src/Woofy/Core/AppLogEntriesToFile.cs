using System;
using System.IO;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core
{
    public class AppLogEntriesToFile : IEventHandler<AppLogEntryAdded>
    {
        private readonly IComicPath comicPath;
        private readonly IFileProxy file;

        private readonly object fileLock = new object();

        public AppLogEntriesToFile(IComicPath comicPath, IFileProxy file)
        {
            this.comicPath = comicPath;
            this.file = file;
        }

        public void Handle(AppLogEntryAdded eventData)
        {
            if (eventData.ComicId == null)
                return;

            lock (fileLock)
            {
                comicPath.EnsureDownloadFolderExistsFor(eventData.ComicId);
                var comicFolder = comicPath.DownloadFolderFor(eventData.ComicId);
                var log = Path.Combine(comicFolder, eventData.ComicId + ".txt");

                file.AppendAllText(log, "[{0}][{1} {2}] {3}\n".FormatTo(DateTime.Now, eventData.ComicId, eventData.ExpressionName, eventData.Message));
            }
        }
    }
}
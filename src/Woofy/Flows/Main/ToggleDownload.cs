using System.ComponentModel;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Main
{
    public class ToggleDownload : ICommand
    {
        public Comic Comic { get; private set; }

        public ToggleDownload(Comic comic)
        {
            Comic = comic;
        }
    }

    public class ToggleDownloadHandler : ICommandHandler<ToggleDownload>
    {
        private readonly IApplicationController applicationController;

        public ToggleDownloadHandler(IApplicationController applicationController)
        {
            this.applicationController = applicationController;
        }

        public void Handle(ToggleDownload command)
        {
            var comic = command.Comic;
            if (comic.HasFinished)
                return;

            switch (comic.Status)
            {
                case Status.Paused:
                    comic.Status = Status.Running;
					applicationController.Raise(new ComicChanged(comic));
                    applicationController.Execute(new StartDownload(comic));
                    break;
                case Status.Running:
                    comic.Status = Status.Paused;
					applicationController.Raise(new ComicChanged(comic));
					applicationController.Execute(new PauseDownload(comic));
                    break;
                default:
                   throw new InvalidEnumArgumentException("command.Comic.Status", (int)command.Comic.Status, typeof(Status));
            }
        }
    }
}
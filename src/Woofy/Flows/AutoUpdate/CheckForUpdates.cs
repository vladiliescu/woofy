using System;
using Woofy.Core;
using Woofy.Core.Infrastructure;
using Woofy.Flows.Main;

namespace Woofy.Flows.AutoUpdate
{
    public class CheckForUpdates : ICommand
    {
        [Obsolete]
        public MainForm Form { get; private set; }
        public CheckForUpdates(MainForm form)
        {
            Form = form;
        }
    }

    public class CheckForUpdatesHandler : ICommandHandler<CheckForUpdates>
    {
        private readonly IUserSettings userSettings;

        public CheckForUpdatesHandler(IUserSettings userSettings)
        {
            this.userSettings = userSettings;
        }

        public void Handle(CheckForUpdates command)
        {
            if (userSettings.AutomaticallyCheckForUpdates)
                UpdateManager.CheckForUpdatesAsync(false, command.Form);
        }
    }
}
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.AutoUpdate
{
	public class AppUpdateCheckHandler : ICommandHandler<AppUpdateCheck>
	{
		private readonly IAppUpdateChecker updateChecker;
		private readonly IAppSettings appSettings;
		private readonly IAppInfo appInfo;
		private readonly IAppController appController;
		private readonly IUserSettings userSettings;

		public AppUpdateCheckHandler(IAppUpdateChecker updateChecker, IAppSettings appSettings, IAppController appController, IUserSettings userSettings, IAppInfo appInfo)
		{
			this.updateChecker = updateChecker;
			this.appInfo = appInfo;
			this.userSettings = userSettings;
			this.appController = appController;
			this.appSettings = appSettings;
		}

		public void Handle(AppUpdateCheck command)
		{
			if (!userSettings.AutomaticallyCheckForUpdates)
				return;

			var updateInfo = updateChecker.CheckForUpdates();
			if (updateInfo == null)
				return;

			var answer = MessageBox.Show("A newer {0} version ({1}) has been released. Would you like to go to the download site?".FormatTo(appInfo.Name, updateInfo.ApplicationVersion), appInfo.Name, MessageBoxButtons.YesNo);
			if (answer != DialogResult.Yes)
			{
				userSettings.AlreadyRejectedApplicationVersion = updateInfo.ApplicationVersion;
				userSettings.Save();
				return;
			}

			appController.Execute(new StartProcess(updateInfo.UpdateAddress));
		}
	}
}
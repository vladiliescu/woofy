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
		private readonly IApplicationController applicationController;
		private readonly IUserSettings userSettings;

		public AppUpdateCheckHandler(IAppUpdateChecker updateChecker, IAppSettings appSettings, IApplicationController applicationController, IUserSettings userSettings, IAppInfo appInfo)
		{
			this.updateChecker = updateChecker;
			this.appInfo = appInfo;
			this.userSettings = userSettings;
			this.applicationController = applicationController;
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

			applicationController.Execute(new StartProcess(updateInfo.UpdateAddress));
		}
	}
}
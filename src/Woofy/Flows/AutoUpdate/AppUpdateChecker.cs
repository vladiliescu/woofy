using System.Net;
using Woofy.Core;
using Woofy.Core.SystemProxies;

namespace Woofy.Flows.AutoUpdate
{
	public interface IAppUpdateChecker
	{
		UpdateInfo CheckForUpdates();
	}

	public class AppUpdateChecker : IAppUpdateChecker
	{
		private readonly IAppSettings appSettings;
		private readonly IUserSettings userSettings;
		private readonly IWebClientProxy webClient;
		private readonly IAppInfo appInfo;

		public AppUpdateChecker(IAppSettings appSettings, IWebClientProxy webClient, IUserSettings userSettings, IAppInfo appInfo)
		{
			this.appSettings = appSettings;
			this.appInfo = appInfo;
			this.userSettings = userSettings;
			this.webClient = webClient;
		}

		public UpdateInfo CheckForUpdates()
		{
			string updateInfoResponse;
			try
			{
				updateInfoResponse = webClient.DownloadString(appSettings.UpdateInfoAddress);
			}
			catch (WebException)
			{
				return null;
			}

			UpdateInfo updateInfo;
			try
			{
				updateInfo = new UpdateInfo(updateInfoResponse);
			}
			catch
			{
				return null;
			}

			if (updateInfo.ApplicationVersion <= appInfo.Version)
				return null;

			if (updateInfo.ApplicationVersion <= userSettings.AlreadyRejectedApplicationVersion)
				return null;
            
			return updateInfo;
		}
	}
}
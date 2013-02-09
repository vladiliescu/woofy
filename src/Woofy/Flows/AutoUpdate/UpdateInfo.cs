using System;
using Newtonsoft.Json;

namespace Woofy.Flows.AutoUpdate
{
	public class UpdateInfo
	{
		public Version ApplicationVersion { get; private set; }
		public string UpdateAddress { get; private set; }

		public UpdateInfo(string updateInfoString)
		{
			var info = JsonConvert.DeserializeObject<UpdateInfoSerialized>(updateInfoString);

			ApplicationVersion = new Version(info.ApplicationVersion);
			UpdateAddress = info.UpdateAddress;
		}

		public UpdateInfo(Version applicationVersion, string updateAddress)
		{
			ApplicationVersion = applicationVersion;
			UpdateAddress = updateAddress;
		}

		private class UpdateInfoSerialized
		{
			public string ApplicationVersion { get; set; }
			public string UpdateAddress { get; set; }
		}
	}
}
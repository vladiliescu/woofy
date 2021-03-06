using System;
using System.Windows.Forms;
using System.Diagnostics;

using Woofy.Core;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.About
{
	partial class AboutForm : Form
	{
		private readonly IAppInfo appInfo = ContainerAccessor.Resolve<IAppInfo>();
        private readonly IAppSettings appSettings = ContainerAccessor.Resolve<IAppSettings>();

		public AboutForm()
		{
			InitializeComponent();

			Text = string.Format("About {0}", appInfo.Name);
			lblProductInfo.Text = string.Format("{0} {1} {2} {3}", appInfo.Name, appInfo.Version.ToExtendedPrettyString(), appInfo.Copyright, appInfo.Company);

			btnOK.Focus();
		}

		private void OnLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(((LinkLabel)sender).Text);
		}

		private void OnHomePageClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(appSettings.AuthorHomePage);
		}

		private void OnWoofyHomePageClicked(object sender, EventArgs e)
		{
            Process.Start(appSettings.HomePage);
		}
	}
}
using System;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Core.Infrastructure;
using Woofy.Properties;

namespace Woofy.Flows.Tray
{
	public interface ITrayIconController : IDisposable
	{
		void DisplayIcon();
	}

	public class TrayIconController : ITrayIconController
	{
		private NotifyIcon icon;

		private readonly IAppInfo appInfo;
		private readonly IApplicationController appController;

		public TrayIconController(IAppInfo appInfo, IApplicationController appController)
		{
			this.appInfo = appInfo;
			this.appController = appController;
		}

		public void DisplayIcon()
		{
			icon = new NotifyIcon
			{
				Icon = Resources.ApplicationIcon,
				ContextMenuStrip = BuildTrayIconMenu(),
				Text = "{0} {1}".FormatTo(appInfo.Title, appInfo.Version.ToPrettyString())
			};
			icon.DoubleClick += (s, e) => appController.Execute<HideOrShowApplication>();
			icon.Visible = true;
		}

		private ContextMenuStrip BuildTrayIconMenu()
		{
			var menu = new ContextMenuStrip();

			menu.Items.Add("&Hide/Show {0}".FormatTo(appInfo.Name), Resources.Woofy, (o, e) => appController.Execute<HideOrShowApplication>());
			menu.Items.Add(new ToolStripSeparator());
			menu.Items.Add("E&xit", /*Resources.Exit*/null, (o, e) =>
			{
				Dispose();
				Application.Exit();
			});

			return menu;
		}

		public void Dispose()
		{
			if (icon == null)
				return;

			icon.Dispose();
			icon = null;
		}
	}
}
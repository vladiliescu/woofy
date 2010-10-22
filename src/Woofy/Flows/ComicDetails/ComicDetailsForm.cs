using System;
using System.Windows.Forms;

namespace Woofy.Flows.ComicDetails
{
	public partial class ComicDetailsForm : Form
	{
		private readonly IComicDetailsPresenter presenter;

		public ComicDetailsForm(IComicDetailsPresenter presenter)
		{
			InitializeComponent();

			this.presenter = presenter;

            RegisterCommands();
		}

	    private void OnLoad(object sender, EventArgs e)
		{
            var model = presenter.Load();
            cbComics.DataSource = model.AvailableComics;
            cbComics.DisplayMember = "Name";



			//ShowOrHideAdvancedOptions(UserSettings.ShowAdvancedComicOptions);

			//cbComics.DataSource = ComicDefinition.GetAvailableComicDefinitions();

			//if (!string.IsNullOrEmpty(UserSettings.LastUsedComicDefinitionFile))
			//{
			//    int i = 0;
			//    foreach (ComicDefinition comicInfo in cbComics.Items)
			//    {
			//        if (comicInfo.ComicInfoFile.Equals(UserSettings.LastUsedComicDefinitionFile, StringComparison.OrdinalIgnoreCase))
			//        {
			//            cbComics.SelectedIndex = i;
			//            break;
			//        }

			//        i++;
			//    }
			//}

			//if (UserSettings.LastNumberOfComicsToDownload.HasValue)
			//{
			//    rbDownloadLast.Checked = true;
			//    numComicsToDownload.Value = (decimal)UserSettings.LastNumberOfComicsToDownload;
			//}
			//else
			//{
			//    rbDownloadOnlyNew.Checked = true;
			//}

			//UpdateDownloadFolder();            
		}        

		private void OnOK(object sender, EventArgs e)
		{
			//if (cbComics.SelectedItem == null)
			//    return;

			//ComicDefinition comicInfo = (ComicDefinition)cbComics.SelectedItem;
			//long? comicsToDownload;
			//if (rbDownloadLast.Checked)
			//    comicsToDownload = (long)numComicsToDownload.Value;
			//else
			//    comicsToDownload = null;
			//string downloadFolder = txtDownloadFolder.Text;
			//string startUrl = chkOverrideStartUrl.Checked ? txtOverrideStartUrl.Text : comicInfo.StartUrl;

			//ComicTask task = new ComicTask(comicInfo.FriendlyName, comicInfo.ComicInfoFile, comicsToDownload, downloadFolder, startUrl, chkRandomPauses.Checked);
			//bool taskAdded = tasksController.AddNewTask(task);

			//if (!taskAdded)
			//{
			//    notificationToolTip.Show("A task for this comic has already been added.", btnOk);
			//    return;
			//}

			//UpdateUserSettings();
			//this.DialogResult = DialogResult.OK;
		}

		private void OnBrowse(object sender, EventArgs e)
		{
			var folderBrowser = new FolderBrowserDialog();

			if (folderBrowser.ShowDialog() == DialogResult.OK)
				txtDownloadFolder.Text = folderBrowser.SelectedPath;
		}
	
        private void RegisterCommands()
        {
            cbComics.SelectedIndexChanged += (s, e) => txtDownloadFolder.Text = ((ComicDetailsViewModel.ComicModel)cbComics.SelectedValue).DefaultDownloadFolder;
        }
	}
}
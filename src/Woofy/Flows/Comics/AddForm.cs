using System;
using System.Windows.Forms;

namespace Woofy.Flows.Comics
{
	public partial class AddForm : Form
	{
        private readonly IComicsPresenter presenter;

        public AddForm(IComicsPresenter presenter)
		{
			InitializeComponent();

			this.presenter = presenter;
		}

		private void OnLoad(object sender, EventArgs e)
		{
			var model = presenter.InitializeAdd();
			cbComics.DataSource = model.AvailableComics;
			cbComics.DisplayMember = "Name";

            ttInfo.SetToolTip(chkPrependIndex, "If checked, Woofy will rename each strip, prepending the strip's index to the filename.");
            ttInfo.SetToolTip(chkEmbedMetadata, "If checked, Woofy will embed additional metadata such as the strip's address inside each downloaded strip.\nYou can view the metadata using an image viewer that supports the XMP format, such as XnView MP.");
		}        

		private void OnOK(object sender, EventArgs e)
		{
			if (cbComics.SelectedItem == null)
				return;

			var comic = (AddViewModel.ComicModel)cbComics.SelectedItem;
			presenter.AddComic(new AddInputModel(comic.Id, chkPrependIndex.Checked, chkEmbedMetadata.Checked));
			
			DialogResult = DialogResult.OK;
		}
	}
}
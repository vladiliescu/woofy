using System;
using System.Windows.Forms;
using Woofy.Flows.AddComic;

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
			var model = presenter.Load();
			cbComics.DataSource = model.AvailableComics;
			cbComics.DisplayMember = "Name";
		}        

		private void OnOK(object sender, EventArgs e)
		{
			if (cbComics.SelectedItem == null)
				return;

			var comic = (ComicDetailsViewModel.ComicModel)cbComics.SelectedItem;
			presenter.SelectComic(comic.Id);
			
			DialogResult = DialogResult.OK;
		}
	}
}
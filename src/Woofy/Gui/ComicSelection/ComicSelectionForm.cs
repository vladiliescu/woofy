using System;
using System.Linq;
using System.Windows.Forms;

namespace Woofy.Gui.ComicSelection
{
	public partial class ComicSelectionForm : BaseForm
	{
		readonly IComicSelectionController controller;

		public ComicSelectionForm(IComicSelectionController controller)
		{
			InitializeComponent();
			this.controller = controller;
		}

		private void OnLoad(object sender, EventArgs e)
		{
			InitializeControls();
			LoadData();

			//model.AvailableComics;
			//model.ActiveComics;
		}

		private void InitializeControls()
		{
			chActiveComics.Width = lvwActiveComics.Width - 5;
			chAvailableComics.Width = lvwAvailableComics.Width - 5;
		}

		private void LoadData()
		{
			var model = controller.LoadComics();
			lvwActiveComics.Items.AddRange((	
			                               	from comic in model.ActiveComics
			                               	select new ListViewItem(comic.ComicName)
			                               ).ToArray());
			lvwAvailableComics.Items.AddRange((
			                                  	from comic in model.AvailableComics
			                                  	select new ListViewItem(comic.ComicName)
			                                  ).ToArray());
		}
	}
}
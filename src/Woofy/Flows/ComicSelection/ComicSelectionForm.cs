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
		}

		private void OnActivateComic(object sender, EventArgs e)
		{
			lvwAvailableComics.MoveSelectedItemsTo(lvwActiveComics);
		}

		private void OnDeactivateComic(object sender, EventArgs e)
		{
			lvwActiveComics.MoveSelectedItemsTo(lvwAvailableComics);
		}

		private void OnOK(object sender, EventArgs e)
		{
			controller.UpdateActiveComics(new ComicSelectionInputModel((
				from ListViewItem item in lvwActiveComics.Items
					select item.Name
			).ToList()));

			Close();
		}

		private void OnCancel(object sender, EventArgs e)
		{
			Close();
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
											select new ListViewItem(comic.ComicName) { Name = comic.DefinitionFile }
										   ).ToArray());
			lvwAvailableComics.Items.AddRange((
			                                  	from comic in model.AvailableComics
												select new ListViewItem(comic.ComicName) { Name = comic.DefinitionFile }
			                                  ).ToArray());
		}
	}

	public static class ListViewExtensions
	{
		public static void MoveSelectedItemsTo(this ListView source, ListView destination)
		{
			if (source.SelectedItems.Count == 0)
				return;

			foreach (ListViewItem item in source.SelectedItems)
			{
				source.Items.Remove(item);
				destination.Items.Add(item);
			}
		}
	}
}
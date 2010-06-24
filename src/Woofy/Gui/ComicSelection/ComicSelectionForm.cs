namespace Woofy.Gui.ComicSelection
{
	public partial class ComicSelectionForm : BaseForm
	{
		public IComicLoadingService ComicLoadingService { get; set; }
		readonly IComicSelectionController controller;

		public ComicSelectionForm(IComicSelectionController controller)
		{
			InitializeComponent();
			this.controller = controller;
		}

		private void OnLoad(object sender, System.EventArgs e)
		{
			var model = controller.LoadComics();

			
			//model.AvailableComics;
			//model.ActiveComics;
		}
	}
}
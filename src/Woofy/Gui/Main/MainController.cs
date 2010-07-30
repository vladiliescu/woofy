using System.Windows.Forms;
using Woofy.Core;
using Woofy.Gui.ComicSelection;
using System.Linq;

namespace Woofy.Gui.Main
{
	public interface IMainController
	{
		void DisplayComicSelectionForm();
		/// <summary>
		/// Temporary property, used until I merge the MainController and ComicTasksController.
		/// </summary>
		ComicTasksController TasksController { get; set; }
	}

	public class MainController : IMainController
	{
		readonly IComicSelectionController comicSelectionController;
		readonly IComicRepository comicRepository;

		public ComicTasksController TasksController { get; set; }

        public MainController(IComicSelectionController comicSelectionController, IComicRepository comicRepository)
		{
			this.comicSelectionController = comicSelectionController;
			this.comicRepository = comicRepository;
		}

		public void DisplayComicSelectionForm()
		{
			var result = comicSelectionController.DisplayComicSelectionForm();
			if (result == DialogResult.Cancel)
				return;

            //refresh the already running comics
			var comics = comicRepository.RetrieveActiveComics();
			for (var i = 0; i < TasksController.Tasks.Count;)
			{
				var activeComic = TasksController.Tasks[i];
				var comicIsStillActive = comics.FirstOrDefault(x => x == activeComic) != null;
				if (comicIsStillActive)
				{
					i++;
					continue;
				}

				TasksController.DeleteTask(activeComic);
			}

			foreach (var comic in comics)
			{
				var comicIsAlreadyActive = TasksController.Tasks.FirstOrDefault(x => x == comic) != null;
				if (comicIsAlreadyActive)
					continue;

				TasksController.AddNewTask(comic);
			}

			TasksController.ResetTasksBindings();
		}
	}
}
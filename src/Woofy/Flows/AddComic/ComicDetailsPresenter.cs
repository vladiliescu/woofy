using Woofy.Core;
using Woofy.Core.ComicManagement;
using System.Linq;

namespace Woofy.Flows.AddComic
{
	public interface IComicActivator
	{
		Result<string> Activate();
	}

	public class ComicDetailsPresenter : IComicActivator
	{
		private string comicId;

		private readonly IComicStore comicStore;
		public ComicDetailsPresenter(IComicStore comicStore)
		{
			this.comicStore = comicStore;
		}

		public Result<string> Activate()
		{
			using (var form = new ComicDetailsForm(this))
			{
				form.ShowDialog();
			}

			return comicId.IsNotNullOrEmpty() ?
					new Result<string>(ServiceResult.Ok, comicId) :
					new Result<string>(ServiceResult.Cancel);
		}

		public ComicDetailsViewModel Load()
		{
			return new ComicDetailsViewModel(
				comicStore.Comics
					.Where(comic => !comic.IsActive)
					.Select(comic => new ComicDetailsViewModel.ComicModel(comic.Id, comic.Name))
					.ToArray()
				);
		}

		public void SelectComic(string id)
		{
			comicId = id;
		}
	}
}
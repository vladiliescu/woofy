using System;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core;

namespace Woofy.Flows.Main
{
	public interface IComicViewModelMapper
	{
		void MapToViewModel(Comic comic, ComicViewModel viewModel);
		ComicViewModel MapToViewModel(Comic comic);
	}

	public class ComicViewModelMapper : IComicViewModelMapper
	{
		public void MapToViewModel(Comic comic, ComicViewModel viewModel)
		{
			viewModel.Id = comic.Id;
			viewModel.Name = comic.Name;
			viewModel.DownloadedStrips = comic.DownloadedStrips;
			viewModel.Status = MapStatus(comic);
			if (comic.CurrentPage != null)
				viewModel.CurrentPage = comic.CurrentPage.AbsoluteUri;
			else
				viewModel.CurrentPage = comic.Definition.StartAt;
		}

		private static ComicViewModel.ComicStatus MapStatus(Comic comic)
		{
			if (comic.HasFinished)
				return ComicViewModel.ComicStatus.Finished;

			if (comic.Status == Status.Paused)
				return ComicViewModel.ComicStatus.Paused;

			if (comic.Status == Status.Running)
				return ComicViewModel.ComicStatus.Running;

			throw new ArgumentException("I don't know how to handle this status: {0}".FormatTo(comic.Status));
		}

		public ComicViewModel MapToViewModel(Comic comic)
		{
			var viewModel = new ComicViewModel();
			MapToViewModel(comic, viewModel);
			return viewModel;
		}
	}
}
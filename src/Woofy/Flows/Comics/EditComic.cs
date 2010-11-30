using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Comics
{
    public class EditComic : ICommand
    {
        public string ComicId { get; private set; }

        public EditComic(string comicId)
        {
            ComicId = comicId;
        }
    }
}
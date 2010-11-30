using Woofy.Flows.Comics;

namespace Woofy.Core
{
    public interface IScreenActivator
    {
        void AddComic();
    }

    public class ScreenActivator : IScreenActivator
    {
        private readonly IComicsPresenter comicsPresenter;

        public ScreenActivator(IComicsPresenter comicsPresenter)
        {
            this.comicsPresenter = comicsPresenter;
        }

        public void AddComic()
        {
            using (var form = new AddForm(comicsPresenter))
            {
                form.ShowDialog();
            }
        }
    }
}
using System.Windows.Forms;
using Woofy.Core;

namespace Woofy.Flows.Comics
{
    public partial class EditForm : Form
    {
        private readonly IComicsPresenter presenter;
        private string comicId;

        public EditForm(IComicsPresenter presenter, EditViewModel viewModel)
        {
            this.presenter = presenter;

            InitializeComponent();
            InitializeControls(viewModel);
        }

        private void InitializeControls(EditViewModel viewModel)
        {
            comicId = viewModel.Id;
            gbComic.Text = viewModel.Name;
            Text = "Edit {0}".FormatTo(viewModel.Name);
            chkPrependIndex.Checked = viewModel.PrependIndexToStrips;
        }

        private void OnOk(object sender, System.EventArgs e)
        {
            presenter.EditComic(new EditInputModel(comicId, chkPrependIndex.Checked));
        }
    }
}

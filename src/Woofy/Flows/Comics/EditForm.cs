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
            chkEmbedMetadata.Checked = viewModel.EmbedMetadata;

            ttInfo.SetToolTip(chkPrependIndex, "If checked, Woofy will rename each strip, prepending the strip's index to the filename.");
            ttInfo.SetToolTip(chkEmbedMetadata, "If checked, Woofy will embed additional metadata such as the strip's address inside each downloaded strip.\nYou can view the metadata using an image viewer that supports the XMP format, such as XnView MP.");
        }

        private void OnOk(object sender, System.EventArgs e)
        {
            presenter.EditComic(new EditInputModel(comicId, chkPrependIndex.Checked, chkEmbedMetadata.Checked));
        }
    }
}

using System.Diagnostics;
using System.Windows.Forms;
using Woofy.Properties;

namespace Woofy.Gui.CompilationError
{
	public partial class CompilationErrorDetails : Form
	{
		public CompilationErrorDetails(CompilationErrorViewModel error)
		{
			InitializeComponent();

			Icon = Resources.PrimaryIcon;

			BindToViewModel(error);
		}

		private void BindToViewModel(CompilationErrorViewModel model)
		{
			lnkErrorSource.Text = model.PathToFile;
			lnkErrorSource.Click += (o, e) => Process.Start("notepad", model.PathToFile);

			txtErrorDetails.Text = model.Error;
		}
	}
}

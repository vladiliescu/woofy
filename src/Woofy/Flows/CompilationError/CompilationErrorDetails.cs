using System.Diagnostics;
using System.Windows.Forms;
using Woofy.Gui.CompilationError;
using Woofy.Properties;

namespace Woofy.Flows.CompilationError
{
	public partial class CompilationErrorDetails : Form
	{
		public CompilationErrorDetails(CompilationErrorViewModel error)
		{
			InitializeComponent();

			Icon = Resources.ApplicationIcon;

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

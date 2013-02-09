using System.Windows.Forms;
using Woofy.Core.Engine;
using Woofy.Flows.CompilationError;

namespace Woofy.Gui.CompilationError
{
	public interface ICompilationErrorController
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="error"></param>
		/// <returns>True if we should try to recompile the definition, false otherwise.</returns>
		bool DisplayError(CompilationException error);
	}

	public class CompilationErrorController : ICompilationErrorController
	{
		public bool DisplayError(CompilationException error)
		{
			var viewModel = new CompilationErrorViewModel
			{
				PathToFile = error.Errors[0].LexicalInfo.FullPath,
				Error = error.ToString()
			};

			using (var form = new CompilationErrorDetails(viewModel))
			{
				return form.ShowDialog() == DialogResult.Retry;
			}
		}
	}
}
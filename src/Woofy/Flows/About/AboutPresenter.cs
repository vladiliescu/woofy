using Woofy.Core.Infrastructure;

namespace Woofy.Flows.About
{
	public class AboutPresenter : ICommandHandler<DisplayAboutScreen>
	{
		public void Handle(DisplayAboutScreen command)
		{
			using (var form = new AboutForm())
			{
				form.ShowDialog();
			}
		}
	}
}
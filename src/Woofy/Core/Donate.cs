using Woofy.Core.Infrastructure;

namespace Woofy.Core
{
	public class Donate : ICommand
	{
	}

	public class DonateHandler : ICommandHandler<Donate>
	{
		private readonly IAppController appController;

		public DonateHandler(IAppController appController)
		{
			this.appController = appController;
		}

		public void Handle(Donate command)
		{
			appController.Execute(new StartProcess("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=GC2A3SSM3XGE2&lc=RO&item_name=Vlad%20Iliescu&item_number=Woofy&currency_code=EUR&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHosted"));
		}
	}
}
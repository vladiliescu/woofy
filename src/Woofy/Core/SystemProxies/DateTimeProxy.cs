using System;

namespace Woofy.Core.SystemProxies
{
	public interface IDateTimeProxy
	{
		DateTime Now();
	}

	public class DateTimeProxy : IDateTimeProxy
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}
	}
}
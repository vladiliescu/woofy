using System;
using Woofy.Core;

namespace Woofy.Core
{
	public static class FormatExtensions
	{
		public static string ToPrettyString(this Version version)
		{
			return "{0}.{1}{2}".FormatTo(version.Major, version.Minor, version.Build);
		}

		public static string ToExtendedPrettyString(this Version version)
		{
			return "{0}.{1}{2}.{3}".FormatTo(version.Major, version.Minor, version.Build, version.Revision);
		}
	}
}
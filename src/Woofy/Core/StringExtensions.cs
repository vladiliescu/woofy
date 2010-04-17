using System;

namespace Woofy.Core
{
	public static class StringExtensions
	{
		public static string FormatTo(this string str, params object[] args)
		{
			return string.Format(str, args);
		}

		public static string[] Split(this string str, string separator)
		{
			return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
		}

		public static bool IsNotNullOrEmpty(this string str)
		{
			return !string.IsNullOrEmpty(str);
		}

		public static T ParseAs<T>(this string str)
		{
			return (T)Convert.ChangeType(str, typeof(T));
		}

		/// <summary>
		/// If the string cannot be parsed, then it returns the default value for T.
		/// </summary>
		public static T ParseAsSafe<T>(this string str)
		{
			return ParseAsSafe(str, default(T));
		}

		public static T ParseAsSafe<T>(this string str, T defaultValue)
		{
			try
			{
				return (T)Convert.ChangeType(str, typeof(T));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static T? ParseAsNullable<T>(this string str)
			where T : struct
		{
			try
			{
				return (T)Convert.ChangeType(str, typeof(T));
			}
			catch
			{
				return null;
			}
		}
	}
}
using System;
using System.Reflection;

namespace Woofy.Core
{
	public interface IApplicationInfo
	{
		Version Version { get; }
		string Name { get; }
		string Title { get; }
		string Copyright { get; }
		string Company { get; }
	}

	public class ApplicationInfo : IApplicationInfo
	{
		private readonly Assembly assembly = Assembly.GetExecutingAssembly();
		
		public Version Version { get; private set; }
		public string Title { get; private set; }
		public string Name { get; private set; }
		public string Copyright { get; private set; }
		public string Company { get; private set; }

		public ApplicationInfo()
		{
			var assemblyName = assembly.GetName();

			Version = assemblyName.Version;
			Name = assemblyName.Name;
			Title = GetCustomAttributeProperty<AssemblyTitleAttribute>(x => x.Title) ?? Name;
			Copyright = GetCustomAttributeProperty<AssemblyCopyrightAttribute>(x => x.Copyright);
			Company = GetCustomAttributeProperty<AssemblyCompanyAttribute>(x => x.Company);
		}

		private string GetCustomAttributeProperty<T>(Func<T, string> extractProperty)
		{
			var attributes = assembly.GetCustomAttributes(typeof(T), false);
			if (attributes.Length == 0)
				return null;

			return extractProperty((T)attributes[0]);
		}
	}
}

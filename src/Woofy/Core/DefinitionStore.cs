using System;
using System.Collections.Generic;
using System.IO;

namespace Woofy.Core
{
	public interface IDefinitionStore
	{
		IList<ComicDefinition> RetrieveAll();
		
		/// <summary>
		/// Returns a definition based on its filename.
		/// </summary>
		/// <param name="definitionFileName">The definition's filename (usually without path information).</param>
		/// <returns></returns>
		ComicDefinition Retrieve(string definitionFileName);
	}

	public class DefinitionStore : IDefinitionStore
	{
		readonly IAppSettings appSettings;

		public DefinitionStore(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
		}

		public IList<ComicDefinition> RetrieveAll()
		{
			var definitions = new List<ComicDefinition>();

			if (!Directory.Exists(appSettings.ComicDefinitionsFolder))
				return new ComicDefinition[0];

			foreach (var definitionFile in Directory.GetFiles(appSettings.ComicDefinitionsFolder, "*.xml"))
			{
				try
				{
					definitions.Add(Retrieve(definitionFile));
				}
				catch (Exception ex)
				{
					Logger.LogException("Error initializing definition: {0}".FormatTo(definitionFile), ex);
				}
			}

			return definitions.ToArray();
		}

		public ComicDefinition Retrieve(string definitionFileName)
		{
			var definitionFile = Path.IsPathRooted(definitionFileName) ? definitionFileName : Path.Combine(appSettings.ComicDefinitionsFolder, definitionFileName);
			if (!File.Exists(definitionFile))
				return null;
			return new ComicDefinition(Path.GetFileName(definitionFile), new FileStream(definitionFile, FileMode.Open, FileAccess.Read));
		}
	}
}
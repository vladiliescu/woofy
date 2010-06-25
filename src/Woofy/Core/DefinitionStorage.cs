using System;
using System.Collections.Generic;
using System.IO;

namespace Woofy.Core
{
	public interface IDefinitionStorage
	{
		IList<ComicDefinition> RetrieveAll();
		
		/// <summary>
		/// Returns a definition based on its filename.
		/// </summary>
		/// <param name="definitionFileName">The definition's filename (usually without path information).</param>
		/// <returns></returns>
		ComicDefinition Retrieve(string definitionFileName);
	}

	public class DefinitionStorage : IDefinitionStorage
	{
		readonly IAppSettings appSettings;

		public DefinitionStorage(IAppSettings appSettings)
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
			return new ComicDefinition(new FileStream(definitionFile, FileMode.Open, FileAccess.Read), Path.GetFileName(definitionFileName));
		}
	}
}
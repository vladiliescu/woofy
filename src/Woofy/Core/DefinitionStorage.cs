using System;
using System.Collections.Generic;
using System.IO;

namespace Woofy.Core
{
	public interface IDefinitionStorage
	{
		IList<ComicDefinition> RetrieveAll();
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
					var definition = new ComicDefinition(definitionFile);
					definitions.Add(definition);
				}
				catch (Exception ex)
				{
					Logger.LogException("Error initializing definition: {0}".FormatTo(definitionFile), ex);
				}
			}

			return definitions.ToArray();
		}
	}
}
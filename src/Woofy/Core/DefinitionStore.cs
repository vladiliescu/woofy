using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Woofy.Core
{
	public interface IDefinitionStore
	{
        ComicDefinition[] Definitions { get; }
	    ComicDefinition FindByFilename(string filename);
	}

	public class DefinitionStore : IDefinitionStore
	{
		readonly IAppSettings appSettings;
        public ComicDefinition[] Definitions { get; set; }

		public DefinitionStore(IAppSettings appSettings)
		{
			this.appSettings = appSettings;

            InitializeDefinitionCache();
		}

        public ComicDefinition FindByFilename(string filename)
        {
            return Definitions.SingleOrDefault(x => x.Filename == filename);
        }

	    private void InitializeDefinitionCache()
	    {
            var definitions = new List<ComicDefinition>();

            if (!Directory.Exists(appSettings.ComicDefinitionsFolder))
            {
                Definitions = new ComicDefinition[0];
            }

	        foreach (var definitionFile in Directory.GetFiles(appSettings.ComicDefinitionsFolder, "*.xml"))
            {
                try
                {
                    definitions.Add(Create(definitionFile));
                }
                catch (Exception ex)
                {
                    Logger.LogException("Error initializing definition: {0}".FormatTo(definitionFile), ex);
                }
            }

            Definitions = definitions.ToArray();
	    }

        /// <summary>
        /// Creates a definition based on its filename.
        /// </summary>
        /// <param name="definitionFileName">The definition's filename (usually without path information).</param>
        /// <returns></returns>
		private ComicDefinition Create(string definitionFileName)
		{
			var definitionFile = Path.IsPathRooted(definitionFileName) ? definitionFileName : Path.Combine(appSettings.ComicDefinitionsFolder, definitionFileName);
			if (!File.Exists(definitionFile))
				return null;
			return new ComicDefinition(Path.GetFileName(definitionFile), new FileStream(definitionFile, FileMode.Open, FileAccess.Read));
		}
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Woofy.Core.Engine;

namespace Woofy.Core
{
	public interface IDefinitionStore
	{
		Definition[] Definitions { get; }
		Definition FindByFilename(string filename);
	}

	public class DefinitionStore : IDefinitionStore
	{
        public Definition[] Definitions { get; set; }

		readonly IAppSettings appSettings;
		readonly IDefinitionCompiler compiler;

		public DefinitionStore(IAppSettings appSettings, IDefinitionCompiler compiler)
		{
			this.appSettings = appSettings;
			this.compiler = compiler;

            InitializeDefinitionCache();
		}

		public Definition FindByFilename(string filename)
        {
			return null;
            //return Definitions.SingleOrDefault(x => x.Filename == filename);
        }

	    private void InitializeDefinitionCache()
	    {
            if (!Directory.Exists(appSettings.ComicDefinitionsFolder))
            {
				Definitions = new Definition[0];
				return;
            }

			var assembly = compiler.Compile(Directory.GetFiles(appSettings.ComicDefinitionsFolder, "*.def"));
			var definitions = assembly.GetTypes();

            Definitions = definitions.Select(definition => (Definition)Activator.CreateInstance(definition)).ToArray();
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
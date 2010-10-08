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
		void InitializeDefinitionCache();
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
		}

		public Definition FindByFilename(string filename)
        {
			return null;
            //return Definitions.SingleOrDefault(x => x.Filename == filename);
        }

	    public void InitializeDefinitionCache()
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
	}
}
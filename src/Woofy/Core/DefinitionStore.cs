using System;
using System.IO;
using System.Linq;
using Woofy.Core.Engine;

namespace Woofy.Core
{
	public interface IDefinitionStore
	{
		Definition[] Definitions { get; }
		Definition Find(string id);
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

		public Definition Find(string id)
        {
            return Definitions.SingleOrDefault(x => x.Id == id);
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
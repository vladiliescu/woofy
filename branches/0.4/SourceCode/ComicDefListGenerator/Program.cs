using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Woofy.Core;

namespace ComicDefListGenerator
{
    class Program
    {
        private static List<ExtendedComicDefinition> definitions = new List<ExtendedComicDefinition>();

        static void Main(string[] args)
        {
            var definitionsFolder = args[0];
            var newDefinitionsFile = args[1];
            var plaintextChangelogFile = args[2];

            InitializeDefinitions(definitionsFolder, newDefinitionsFile);

            GeneratePlaintextChangelog(plaintextChangelogFile);

            return;

            using (var writer = new StreamWriter("defsList.html"))
            {
                foreach (ComicDefinition comicDefinition in ComicDefinition.GetAvailableComicDefinitions())
                {
                    writer.WriteLine(string.Format("<li><a target=\"_blank\" rel=\"nofollow\" href=\"{0}\">{1}</a></li>",
                                                    comicDefinition.StartUrl, comicDefinition.FriendlyName));
                }
            }
        }

        private static void GeneratePlaintextChangelog(string plaintextChangelogFile)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Added:");

            foreach (var definition in definitions)
            {
                if (definition.Status != DefinitionStatus.Added)
                    continue;

                builder.AppendFormat("  * {0}\n", definition.FriendlyName);
            }

            var areUpdated = false;
            foreach (var definition in definitions)
            {
                if (definition.Status != DefinitionStatus.Updated)
                    continue;

                if (!areUpdated)
                {
                    areUpdated = true;

                    builder.AppendLine();
                    builder.AppendLine();
                    builder.AppendLine("Updated:");
                }

                builder.AppendFormat(">> {0}\n", definition.FriendlyName);
            }

            using (var writer = new StreamWriter(plaintextChangelogFile))
            {
                writer.Write(builder.ToString());
            }
            
        }

        private static void InitializeDefinitions(string definitionsFolder, string newDefinitionsFile)
        {
            var definitionsStatuses = new Dictionary<string, DefinitionStatus>();
            using (var reader = new StreamReader(newDefinitionsFile))
            {
                do
                {
                    var definitionFileName = reader.ReadLine();
                    var definitionStatus = (DefinitionStatus)Enum.Parse(typeof(DefinitionStatus), reader.ReadLine());

                    definitionsStatuses.Add(definitionFileName, definitionStatus);
                }
                while (!reader.EndOfStream);
            }

            foreach (var definitionFile in Directory.GetFiles(definitionsFolder, "*.xml"))
            {
                var definition = new ExtendedComicDefinition(definitionFile);
                definitions.Add(definition);

                var definitionFileName = Path.GetFileName(definitionFile);
                if (!definitionsStatuses.ContainsKey(definitionFileName))
                {
                    definition.Status = DefinitionStatus.None;
                    continue;
                }

                definition.Status = definitionsStatuses[definitionFileName];
            }
        }
    }

    public enum DefinitionStatus
    {
        None = 0,
        Added = 1,
        Updated = 2
    }
}

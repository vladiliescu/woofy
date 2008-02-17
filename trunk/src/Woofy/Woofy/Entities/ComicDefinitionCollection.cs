using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Woofy.Entities
{
    public class ComicDefinitionCollection : Collection<ComicDefinition>
    {
        public ComicDefinition FindBySourceFileName(string fileName)
        {
            foreach (ComicDefinition definition in this)
            {
                if (definition.SourceFileName.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    return definition;
            }

            return null;
        }
    }
}

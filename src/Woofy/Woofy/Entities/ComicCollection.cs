using System;
using System.Collections.ObjectModel;

namespace Woofy.Entities
{
    public class ComicCollection : ObservableCollection<Comic>
    {
        public Comic FindBySourceFileName(string sourceFileName)
        {
            foreach (var comic in this)
            {
                if (comic.Definition.SourceFileName.Equals(sourceFileName, StringComparison.OrdinalIgnoreCase))
                    return comic;
            }

            return null;
        }
    }
}

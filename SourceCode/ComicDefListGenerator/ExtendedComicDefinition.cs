using Woofy.Core;

namespace ComicDefListGenerator
{
    public class ExtendedComicDefinition : ComicDefinition
    {
        public DefinitionStatus Status { get; set; }

        public ExtendedComicDefinition(string definitionFile)
            : base(definitionFile)
        {
        }
    }
}

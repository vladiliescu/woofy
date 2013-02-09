namespace Woofy.Flows.Comics
{
    public class EditViewModel
    {
        public string Name { get; private set; }
        public bool PrependIndexToStrips { get; private set; }
        public string Id { get; private set; }
        public bool EmbedMetadata { get; private set; }

        public EditViewModel(string id, string name, bool prependIndexToStrips, bool embedMetadata)
        {
            Id = id;
            Name = name;
            PrependIndexToStrips = prependIndexToStrips;
            EmbedMetadata = embedMetadata;
        }
    }

    public class EditInputModel
    {
        public string ComicId { get; private set; }
        public bool PrependIndexToStrips { get; private set; }
        public bool EmbedMetadata { get; set; }

        public EditInputModel(string comicId, bool prependIndexToStrips, bool embedMetadata)
        {
            ComicId = comicId;
            PrependIndexToStrips = prependIndexToStrips;
            EmbedMetadata = embedMetadata;
        }
    }
}
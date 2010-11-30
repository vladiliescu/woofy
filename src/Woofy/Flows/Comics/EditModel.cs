namespace Woofy.Flows.Comics
{
    public class EditViewModel
    {
        public string Name { get; set; }
        public bool PrependIndexToStrips { get; set; }
        public string Id { get; private set; }

        public EditViewModel(string id, string name, bool prependIndexToStrips)
        {
            Id = id;
            Name = name;
            PrependIndexToStrips = prependIndexToStrips;
        }
    }

    public class EditInputModel
    {
        public string ComicId { get; private set; }
        public bool PrependIndexToStrips { get; private set; }

        public EditInputModel(string comicId, bool prependIndexToStrips)
        {
            ComicId = comicId;
            PrependIndexToStrips = prependIndexToStrips;
        }
    }
}
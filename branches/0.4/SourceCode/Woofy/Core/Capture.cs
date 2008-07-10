namespace Woofy.Core
{
    public class Capture
    {
        public string Name { get; private set; }
        public string Content { get; private set; }

        public Capture(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
namespace Woofy.Core
{
    public class Capture
    {
        public string Name { get; private set; }
        public string Content { get; private set; }
		public CaptureTarget Target { get; private set; }

        public Capture(string name, string content)
			: this(name, content, CaptureTarget.Body)
        {
        }

		public Capture(string name, string content, CaptureTarget target)
		{
			Name = name;
			Content = content;
			Target = target;
		}
    }
}
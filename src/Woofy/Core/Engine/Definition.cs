namespace Woofy.Core.Engine
{
	public abstract class Definition
	{
        public abstract string Comic { get; }
        public abstract string StartAt { get; }

        public abstract void Run();
	}
}
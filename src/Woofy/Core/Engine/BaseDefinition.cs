namespace Woofy.Core.Engine
{
	public abstract class BaseDefinition
	{
        public abstract string Comic { get; }
        public abstract string StartAt { get; }

        public abstract void Run();
	}
}
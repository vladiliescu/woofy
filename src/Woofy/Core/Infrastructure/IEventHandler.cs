namespace Woofy.Core.Infrastructure
{
	public interface IEventHandler<T>
	{
		void Handle(T eventData);
	}
}
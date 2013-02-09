namespace Woofy.Core.Infrastructure
{
	public interface IEventHandler<T>
        where T : IEvent
	{
		void Handle(T eventData);
	}
}
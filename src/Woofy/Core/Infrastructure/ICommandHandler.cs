namespace Woofy.Core.Infrastructure
{
	public interface ICommandHandler<T>
	{
		void Handle(T command);
	}
}
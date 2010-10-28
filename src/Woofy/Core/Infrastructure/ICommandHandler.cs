namespace Woofy.Core.Infrastructure
{
	public interface ICommandHandler<T>
        where T : ICommand
	{
		void Handle(T command);
	}
}
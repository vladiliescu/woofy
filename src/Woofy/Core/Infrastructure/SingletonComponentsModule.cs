using Autofac;
using Woofy.Core.ComicManagement;

namespace Woofy.Core.Infrastructure
{
	public class SingletonComponentsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ComicStore>().As<IComicStore>().SingleInstance();
            builder.RegisterType<DefinitionStore>().As<IDefinitionStore>().SingleInstance();
            builder.RegisterType<BotSupervisor>().As<IBotSupervisor>().SingleInstance();
			builder.Register(c => Program.SynchronizationContext).SingleInstance();
			builder.Register(c => ContainerAccessor.Container).As<IContainer>().SingleInstance();
		}
	}
}
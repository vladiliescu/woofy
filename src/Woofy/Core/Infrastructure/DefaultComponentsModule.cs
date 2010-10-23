using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Woofy.Core.Infrastructure
{
	public class DefaultComponentsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .SingleInstance();

			builder.Register(c => Program.SynchronizationContext).SingleInstance();
		}
	}
}
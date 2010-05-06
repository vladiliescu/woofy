using Autofac;
using Autofac.Builder;

namespace Woofy.Core.Infrastructure
{
	public static class ContainerAccesor
	{
		public static IContainer Container { get; private set; }

		public static void RegisterComponents()
		{
			var builder = new ContainerBuilder();
			builder.SetDefaultScope(InstanceScope.Factory);

			builder.RegisterModule(new ComponentsAsImplementedInterfacesModule());
			builder.RegisterModule(new SingletonComponentsModule());

			Container = builder.Build();
		}
		
		public static T Resolve<T>()
		{
			return Container.Resolve<T>();
		}
	}
}
using Autofac.Builder;

namespace Woofy.Core.Infrastructure
{
	public class SingletonComponentsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<ComicStore>().As<IComicStore>().SingletonScoped();
		}
	}
}
using System;
using System.Linq;
using Autofac.Builder;

namespace Woofy.Core.Infrastructure
{
	public class ComponentsAsImplementedInterfacesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var classes = from type in GetType().Assembly.GetTypes()
			              where type.IsClass
			              select type;

			foreach (var c in classes)
			{
				var correspondingInterface = Type.GetType(c.FullName.Replace(c.Name, "I" + c.Name));
				if (correspondingInterface == null)
					continue;

				builder.Register(c).As(correspondingInterface);
			}
		}
	}
}
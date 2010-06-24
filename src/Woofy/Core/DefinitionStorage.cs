using System.Collections.Generic;

namespace Woofy.Core
{
	public interface IDefinitionStorage
	{
		IList<ComicDefinition> RetrieveAll();
	}
}
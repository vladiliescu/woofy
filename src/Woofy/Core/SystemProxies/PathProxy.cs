using System.IO;

namespace Woofy.Core.SystemProxies
{
	public interface IPathProxy
	{
		string GetDirectoryName(string path);
	}

	public class PathProxy : IPathProxy
	{
		public string GetDirectoryName(string path)
		{
			return Path.GetDirectoryName(path);
		}
	}
}
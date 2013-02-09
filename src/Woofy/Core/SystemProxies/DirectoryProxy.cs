using System.IO;

namespace Woofy.Core.SystemProxies
{
	public interface IDirectoryProxy
	{
		bool Exists(string path);
		DirectoryInfo CreateDirectory(string path);
	}

	public class DirectoryProxy : IDirectoryProxy
	{
		public bool Exists(string path)
		{
			return Directory.Exists(path);
		}

		public DirectoryInfo CreateDirectory(string path)
		{
			return Directory.CreateDirectory(path);
		}
	}
}
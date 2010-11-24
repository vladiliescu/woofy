using System.IO;

namespace Woofy.Core.SystemProxies
{
	public interface IFileProxy
	{
		void WriteAllText(string path, string contents);
		string ReadAllText(string path);
		bool Exists(string path);
		void AppendAllText(string path, string contents);
        void Move(string sourceFileName, string destFileName);
	}

	public class FileProxy : IFileProxy
	{
		public void WriteAllText(string path, string contents)
		{
			File.WriteAllText(path, contents);
		}

		public string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}

		public bool Exists(string path)
		{
			return File.Exists(path);
		}

		public void AppendAllText(string path, string contents)
		{
			File.AppendAllText(path, contents);
		}

        public void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }
	}
}
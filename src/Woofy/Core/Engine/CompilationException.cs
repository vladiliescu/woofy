using System;
using Boo.Lang.Compiler;

namespace Woofy.Core.Engine
{
	[Serializable]
	public class CompilationException : Exception
	{
		public CompilerErrorCollection Errors { get; private set; }

		public CompilationException(CompilerErrorCollection errors)
		{
			Errors = errors;
		}

		public override string ToString()
		{
			return Errors.ToString(false);
		}
	}
}
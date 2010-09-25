using System.Reflection;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using Woofy.Core.Engine;

namespace Woofy.Console
{
	class Program
	{
		/*
		 * start_at http://...
		 * do_while visit_next(regex)
		 *		download regex
		 */
		static void Main(string[] args)
		{
			CompileBooScriptUsingCustomMacro();

			//CompileBooScript();

			//BuildAndRunStatements();
		}

		private static void CompileBooScriptUsingCustomMacro()
		{
			var code = @"
import Boo.Lang.PatternMatching
import Woofy.Console

class Foo:
    public Bar = 1
    public Baz = 2
    
foo = Foo()
print foo.Bar, foo.Baz

with foo:
	print _Bar, _Baz

";

			var parameters = new CompilerParameters()
			{
				OutputType = CompilerOutputType.ConsoleApplication,
				Pipeline = new Run(),
				OutputAssembly = "CompiledBooScriptUsingCustomMacro.dll",
				Input = { new StringInput("integration.boo", code) }//,
				//References = { Assembly.GetExecutingAssembly() }
			};

			parameters.References.Add(Assembly.GetExecutingAssembly());

			var compiler = new BooCompiler(parameters);

			var context = compiler.Run();

			if (context.Errors.Count > 0)
				throw new CompilerError("");

		}

		private static void CompileBooScript()
		{
			var code = @"
import Boo.Lang.PatternMatching

macro hello(message as string):
	return [|
		print ""Hello "" + $message
	|]		

hello ""world""
";

			var parameters = new CompilerParameters()
			{
				OutputType = CompilerOutputType.ConsoleApplication,
				Pipeline = new Run(),
				OutputAssembly = "CompiledBooScript.dll",
				Input = { new StringInput("integration.boo", code) }
			};

			var compiler = new BooCompiler(parameters);

			var context = compiler.Run();

			if (context.Errors.Count > 0)
				throw new CompilerError("");
		}

		private static void BuildAndRunStatements()
		{
			var statements = new IStatement[] { 
        	    new StartAt { Url = "http://xkcd.com/5/" }, 
                new DoWhile
                {
                    Condition = new [] { new VisitNext { Regex = @"<a\shref=""(?<content>/[\d]*/)""\saccesskey=""p"">" } },
                    Body = new[] { new Download { Regex = @"<img\ssrc=""(?<content>http://imgs.xkcd.com/comics/[\w()-]*\.(gif|jpg|jpeg|png))" }}
                }
            };

			var context = new Context();
			foreach (var statement in statements)
			{
				statement.Run(context);
			}
		}
	}
}

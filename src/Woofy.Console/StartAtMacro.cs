using System;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Console
{
	public class Start_atMacro : LexicalInfoPreservingMacro
	{
		protected override Statement ExpandImpl(MacroStatement macro)
		{
			var printStatement = new MacroStatement() { Name="print", Arguments = ExpressionCollection.FromArray(new [] { new StringLiteralExpression("jinkies") }) };
			return printStatement;

			//throw new Exception("`hello` macro invocation argument(s) did not match definition: `hello((message as string))`");
		}
	}

	/// <summary>
	/// Extracted from Boo.Lang.Compiler via Reflector
	/// </summary>
	public class HelloMacro : LexicalInfoPreservingMacro
	{
		protected override Statement ExpandImpl(MacroStatement hello)
		{
			if (hello == null)
			{
				throw new ArgumentNullException("hello");
			}

			var macro = hello;
			if ((macro.Name == "hello") && ((1 == macro.Arguments.Count) && (macro.Arguments[0] is StringLiteralExpression)))
			{
				var argument = (StringLiteralExpression) macro.Arguments[0];
				var message = argument.Value;
				
				var printStatement = new MacroStatement {Name = "print"};
				var addition = new BinaryExpression {Operator = BinaryOperatorType.Addition};
				var helloExpression = new StringLiteralExpression {Value = "Hello "};
				addition.Left = helloExpression;
				addition.Right = Expression.Lift(message);
				var items = new[] { addition };
				printStatement.Arguments = ExpressionCollection.FromArray(items);
				printStatement.Body = new Block();
				return printStatement;
			}

			throw new Exception("`hello` macro invocation argument(s) did not match definition: `hello((message as string))`");
		}

	}
}
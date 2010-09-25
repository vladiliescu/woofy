using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Console
{
	public class WithMacro : AbstractAstMacro
	{
		private class NameExpander : DepthFirstTransformer
		{
			private ReferenceExpression _inst;
			public NameExpander(ReferenceExpression inst)
			{
				_inst = inst;
			}

			public override void OnReferenceExpression(ReferenceExpression node)
			{
				// if the name of the reference begins with '_'
				// then convert the reference to a member reference
				// of the provided instance
				if (node.Name.StartsWith("_"))
				{
					// create the new member reference and set it up
					var mre = new MemberReferenceExpression(node.LexicalInfo);
					mre.Name = node.Name.Substring(1);
					mre.Target = _inst.CloneNode();

					// replace the original reference in the AST
					// with the new member-reference
					ReplaceCurrentNode(mre);
				}
			}
		}

		public override Statement Expand(MacroStatement macro)
		{
			var inst = (ReferenceExpression)macro.Arguments[0];
			var block = macro.Body;
			var ne = new NameExpander(inst);
			ne.Visit(block);
			return block;
		}
	}
}
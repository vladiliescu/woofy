using System;
using Boo.Lang.Compiler.Ast;
using Boo.Lang.Compiler.Steps;

namespace Woofy.Core.Engine
{
	public class DefinitionClassCompilerStep : AbstractCompilerStep
	{
		private static readonly Type BaseDefinitionType = typeof(BaseDefinition);

		public override void Run()
		{
			// if (Context.References.Contains(baseClass.Assembly) == false)
			//     Context.Parameters.References.Add(baseClass.Assembly);

			foreach (Module module in CompileUnit.Modules)
			{
				//foreach (string ns in namespaces)
				//{
				//    module.Imports.Add(new Import(module.LexicalInfo, ns));
				//}

				var definition = new ClassDefinition();
				definition.Name = "_" + module.Name;
				definition.BaseTypes.Add(new SimpleTypeReference(BaseDefinitionType.FullName));

				//GenerateConstructors(definition);

				// This is called before the module.Globals is set to a new block so that derived classes may retrieve the
				// block from the module.
				//ExtendBaseClass(module, definition);

				module.Globals = new Block();
				module.Members.Add(definition);
			}
		}
	}
}
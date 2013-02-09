using Boo.Lang.Compiler.Ast;
using Rhino.DSL;
using Woofy.Core.Engine.Expressions;

namespace Woofy.Core.Engine
{
	public class DefinitionClassCompilerStep : ImplicitBaseClassCompilerStep
	{
		public DefinitionClassCompilerStep()
			: this(new ParameterDeclarationCollection { new ParameterDeclaration("context", TypeReference.Lift(typeof(Context))) })
		{
		}

	    public DefinitionClassCompilerStep(ParameterDeclarationCollection parameters)
			: base(typeof(Definition), "RunImpl", parameters, 
                    typeof(MetaMethods).Namespace,
                    typeof(Definition).Namespace
                )
	    {
	    }

        protected override void ExtendBaseClass(TypeDefinition definition)
        {
            definition.Name = "_" + definition.Name;
        }
	}
}
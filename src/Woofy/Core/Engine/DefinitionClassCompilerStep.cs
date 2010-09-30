using System;
using Boo.Lang.Compiler.Ast;
using Boo.Lang.Compiler.Steps;
using Rhino.DSL;

namespace Woofy.Core.Engine
{
	public class DefinitionClassCompilerStep : ImplicitBaseClassCompilerStep
	{
        public DefinitionClassCompilerStep()
            : this(null)
	    {
	    }

	    public DefinitionClassCompilerStep(ParameterDeclarationCollection parameters)
            : base(typeof(BaseDefinition), "Run", parameters, "Woofy.Core.Engine.Macros")
	    {
	    }

        protected override void ExtendBaseClass(TypeDefinition definition)
        {
            definition.Name = "_" + definition.Name;
        }
	}
}
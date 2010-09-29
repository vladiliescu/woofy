using System;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine.Macros
{
    public class ComicMacro : LexicalInfoPreservingMacro
    {
        protected override Statement ExpandImpl(MacroStatement macro)
        {
            var classDefinition = new ClassDefinition(macro.LexicalInfo);
            //return Expression.Lift(classDefinition);

            

            return null;
        }
    }
}
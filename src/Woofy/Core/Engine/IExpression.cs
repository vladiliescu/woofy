namespace Woofy.Core.Engine
{
    public interface IExpression
    {
        object Invoke(object argument, Context context);
    }
}
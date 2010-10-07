namespace Woofy.Core.Engine
{
    public interface IExpression
    {
        object Invoke(Context context);
    }
}
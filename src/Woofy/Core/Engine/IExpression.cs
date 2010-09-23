namespace Woofy.Core.Engine
{
    public interface IExpression
    {
        object Execute(Context context);
    }
}
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    public interface IEditableImplementation<T>
        where T:Expression
    {
        T ToTypedExpression();
    }
}

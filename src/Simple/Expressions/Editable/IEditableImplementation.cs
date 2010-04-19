using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    public interface IEditableImplementation<T>
        where T:Expression
    {
        T ToTypedExpression();
    }
}

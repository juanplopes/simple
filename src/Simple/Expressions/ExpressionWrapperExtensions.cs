using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Expressions;

namespace Simple
{
    public static class ExpressionWrapper
    {
        public static ExpressionWrapper<T, P> ToSettable<T, P>(this Expression<Func<T, P>> expr)
        {
            return new ExpressionWrapper<T, P>(expr);
        }
    }
}

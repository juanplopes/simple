using System;
using System.Linq;
using Simple.Entities.QuerySpec;
using System.Linq.Expressions;

namespace Simple
{
    public static class SpecBuilderExtensions
    {
        public static SpecBuilder<T> ToSpec<T>(this Expression<Func<IQueryable<T>, IQueryable<T>>> expr)
        {
            return new SpecBuilder<T>().Expr(expr);
        }

        public static SpecBuilder<T> ApplyFuncs<T>(this SpecBuilder<T> builder, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return options.Aggregate(builder, (x, func) => func(x));
        }
    }
}

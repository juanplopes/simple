using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Expressions.Editable;

namespace Simple.Entities.QuerySpec
{
    public interface IFilterResolver<T>
    {
        IQueryable<T> Filter(IQueryable<T> query, Expression<Func<T, bool>> expr);
    }

    [Serializable]
    public class FilterItem<T> : ISpecItem<T, IFilterResolver<T>>
    {
        public EditableExpression<Func<T, bool>> Expression { get; set; }

        public FilterItem(Expression<Func<T, bool>> expr)
        {
            this.Expression = expr.Funcletize().ToEditableExpression();
        }

        public IQueryable<T> Execute(IQueryable<T> query, IFilterResolver<T> resolver)
        {
            return resolver.Filter(query, Expression.ToTypedLambda());
        }
    }

}

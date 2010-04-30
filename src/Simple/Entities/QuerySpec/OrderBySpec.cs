using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Expressions.Editable;

namespace Simple.Entities.QuerySpec
{
    [Serializable]
    public class OrderBySpec<T> : SpecBuilder<T>
    {
        public OrderBySpec(IList<ISpecItem<T>> items, ISpecItem<T> fetch)
            : base(items)
        {
            Items.Add(fetch);
        }

        public OrderBySpec<T> ThenBy(Expression<Func<T, object>> expr)
        {
            Items.Add(new OrderThenByAscItem<T>(expr));
            return this;
        }

        public OrderBySpec<T> ThenByDesc(Expression<Func<T, object>> expr)
        {
            Items.Add(new OrderThenByDescItem<T>(expr));
            return this;
        }

        public OrderBySpec<T> None
        {
            get { return this; }
        }
    }

    public interface IOrderByResolver<T>
    {
        IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expr);
        IQueryable<T> ThenBy(IQueryable<T> query, Expression<Func<T, object>> expr);
        IQueryable<T> OrderByDescending(IQueryable<T> query, Expression<Func<T, object>> expr);
        IQueryable<T> ThenByDescending(IQueryable<T> query, Expression<Func<T, object>> expr);
    }

    [Serializable]
    public abstract class OrderByItem<T> : ISpecItem<T, IOrderByResolver<T>>
    {
        public LazyExpression<Func<T, object>> Expression { get; protected set; }

        public OrderByItem(Expression<Func<T, object>> expr)
        {
            Expression = expr.ToLazyExpression();
        }


        public abstract IQueryable<T> Execute(IQueryable<T> query, IOrderByResolver<T> resolver);
    }

    [Serializable]
    public class OrderByAscItem<T> : OrderByItem<T>
    {
        public OrderByAscItem(Expression<Func<T, object>> expr) : base(expr) { }

        public override IQueryable<T> Execute(IQueryable<T> query, IOrderByResolver<T> resolver)
        {
            return resolver.OrderBy(query, Expression.Real);
        }
    }

    [Serializable]
    public class OrderByDescItem<T> : OrderByItem<T>
    {
        public OrderByDescItem(Expression<Func<T, object>> expr) : base(expr) { }

        public override IQueryable<T> Execute(IQueryable<T> query, IOrderByResolver<T> resolver)
        {
            return resolver.OrderByDescending(query, Expression.Real);
        }
    }

    [Serializable]
    public class OrderThenByAscItem<T> : OrderByItem<T>
    {
        public OrderThenByAscItem(Expression<Func<T, object>> expr) : base(expr) { }

        public override IQueryable<T> Execute(IQueryable<T> query, IOrderByResolver<T> resolver)
        {
            return resolver.ThenBy(query, Expression.Real);
        }
    }

    [Serializable]
    public class OrderThenByDescItem<T> : OrderByItem<T>
    {
        public OrderThenByDescItem(Expression<Func<T, object>> expr) : base(expr) { }

        public override IQueryable<T> Execute(IQueryable<T> query, IOrderByResolver<T> resolver)
        {
            return resolver.ThenByDescending(query, Expression.Real);
        }
    }

}

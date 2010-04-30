using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions.Editable;
using System.Linq.Expressions;
using Simple.Entities.QuerySpec;
using Simple.Expressions;

namespace Simple.Entities.QuerySpec
{
    [Serializable]
    public class FetchSpec<T, TFetched> : SpecBuilder<T>
    {
        public FetchSpec(IList<ISpecItem<T>> items, ISpecItem<T> fetch)
            : base(items)
        {
            Items.Add(fetch);
        }

        public FetchSpec<T, TFetching> ThenFetch<TFetching>(Expression<Func<TFetched, TFetching>> expr)
        {
            return new FetchSpec<T, TFetching>(Items, new ThenFetchItem<T, TFetched, TFetching>(expr));
        }

        public FetchSpec<T, TFetching> ThenFetchMany<TFetching>(Expression<Func<TFetched, IEnumerable<TFetching>>> expr)
        {
            return new FetchSpec<T, TFetching>(Items, new ThenFetchManyItem<T, TFetched, TFetching>(expr));
        }
    }

    public interface IFetchResolver<T>
    {
        IQueryable<T> Fetch<P>(IQueryable<T> query, Expression<Func<T, P>> expr);
        IQueryable<T> FetchMany<P>(IQueryable<T> query, Expression<Func<T, IEnumerable<P>>> expr);
        IQueryable<T> ThenFetch<P, Q>(IQueryable<T> query, Expression<Func<P, Q>> expr);
        IQueryable<T> ThenFetchMany<P, Q>(IQueryable<T> query, Expression<Func<P, IEnumerable<Q>>> expr);
    }

    public interface IFetchItem<T> : ISpecItem<T, IFetchResolver<T>>
    {
    }

    [Serializable]
    public class FetchExpression<T, P>
    {
        public LazyExpression<Func<T, P>> Expression { get; set; }

        public FetchExpression(Expression<Func<T, P>> expr)
        {
            this.Expression = expr.ToLazyExpression();
        }
    }

    [Serializable]
    public class FetchItem<T, TFetched> : FetchExpression<T, TFetched>, IFetchItem<T>
    {
        public FetchItem(Expression<Func<T, TFetched>> expr) : base(expr) { }

        public IQueryable<T> Execute(IQueryable<T> query, IFetchResolver<T> resolver)
        {
            return resolver.Fetch(query, Expression.Real);
        }
    }

    [Serializable]
    public class ThenFetchItem<T, TFetched, TFetching> : FetchExpression<TFetched, TFetching>, IFetchItem<T>
    {
        public ThenFetchItem(Expression<Func<TFetched, TFetching>> expr) : base(expr) { }

        public IQueryable<T> Execute(IQueryable<T> query, IFetchResolver<T> resolver)
        {
            return resolver.ThenFetch(query, Expression.Real);
        }
    }

    [Serializable]
    public class ThenFetchManyItem<T, TFetched, TFetching> : FetchExpression<TFetched, IEnumerable<TFetching>>, IFetchItem<T>
    {
        public ThenFetchManyItem(Expression<Func<TFetched, IEnumerable<TFetching>>> expr) : base(expr) { }

        public IQueryable<T> Execute(IQueryable<T> query, IFetchResolver<T> resolver)
        {
            return resolver.ThenFetchMany(query, Expression.Real);
        }
    }

    [Serializable]
    public class FetchManyItem<T, TFetched> : FetchExpression<T, IEnumerable<TFetched>>, IFetchItem<T>
    {
        public FetchManyItem(Expression<Func<T, IEnumerable<TFetched>>> expr) : base(expr) { }

        public IQueryable<T> Execute(IQueryable<T> query, IFetchResolver<T> resolver)
        {
            return resolver.FetchMany(query, Expression.Real);
        }
    }

}

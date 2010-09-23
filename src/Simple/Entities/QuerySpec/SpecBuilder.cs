using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Simple.Entities.QuerySpec
{
    [Serializable]
    public class SpecBuilder<T>
    {
        public IEnumerable<ISpecItem<T>> Items { get; protected set; }

        public SpecBuilder() : this(new LinkedList<ISpecItem<T>>()) { }
        public SpecBuilder(IEnumerable<ISpecItem<T>> items)
        {
            this.Items = new LinkedList<ISpecItem<T>>(items);
        }

        public SpecBuilder<T> Merge(SpecBuilder<T> spec)
        {
            return this.Merge(spec.Items);
        }

        public SpecBuilder<T> Merge(IEnumerable<ISpecItem<T>> items)
        {
            return new SpecBuilder<T>(this.Items.Union(items));
        }

        public FetchSpec<T, TFetching> Fetch<TFetching>(Expression<Func<T, TFetching>> expr)
        {
            return new FetchSpec<T, TFetching>(Items, new FetchItem<T, TFetching>(expr));
        }

        public FetchSpec<T, TFetching> FetchMany<TFetching>(Expression<Func<T, IEnumerable<TFetching>>> expr)
        {
            return new FetchSpec<T, TFetching>(Items, new FetchManyItem<T, TFetching>(expr));
        }

        public OrderBySpec<T> OrderBy(Expression<Func<T, object>> expr)
        {
            return new OrderBySpec<T>(Items, new OrderByAscItem<T>(expr));
        }

        public OrderBySpec<T> OrderByDesc(Expression<Func<T, object>> expr)
        {
            return new OrderBySpec<T>(Items, new OrderByDescItem<T>(expr));
        }

        public SpecBuilder<T> Skip(int value)
        {
            return new SpecBuilder<T>(
                Items.Union(new SkipItem<T>(value)));
        }

        public SpecBuilder<T> Take(int value)
        {
            return new SpecBuilder<T>(
                Items.Union(new TakeItem<T>(value)));
        }

        public SpecBuilder<T> Filter(Expression<Func<T, bool>> expr)
        {
            return new SpecBuilder<T>(
                Items.Union(new FilterItem<T>(expr)));
        }

        public SpecBuilder<T> Expr(Expression<Func<IQueryable<T>, IQueryable<T>>> expr)
        {
            return new SpecBuilder<T>(
                Items.Union(new ExpressionItem<T>(expr)));
        }
    }
}

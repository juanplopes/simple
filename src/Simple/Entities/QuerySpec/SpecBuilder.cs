using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simple.Entities.QuerySpec
{
    [Serializable]
    public class SpecBuilder<T>
    {
        public IList<ISpecItem<T>> Items { get; protected set; }

        public SpecBuilder() : this(new List<ISpecItem<T>>()) { }
        public SpecBuilder(IList<ISpecItem<T>> items)
        {
            this.Items = items;
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
            Items.Add(new SkipItem<T>(value));
            return this;
        }

        public SpecBuilder<T> Take(int value)
        {
            Items.Add(new TakeItem<T>(value));
            return this;
        }

        public SpecBuilder<T> Filter(Expression<Func<T, bool>> expr)
        {
            Items.Add(new FilterItem<T>(expr));
            return this;
        }
    }
}

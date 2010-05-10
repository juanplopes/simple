using System;
using System.Linq;

namespace Simple.Entities.QuerySpec
{
    public interface ILimitsResolver<T>
    {
        IQueryable<T> Skip(IQueryable<T> query, int skip);
        IQueryable<T> Take(IQueryable<T> query, int take);
    }

    [Serializable]
    public abstract class LimitsItem<T> : ISpecItem<T, ILimitsResolver<T>>
    {
        public int Value { get; set; }

        public LimitsItem(int value) { this.Value = value; }

        public abstract IQueryable<T> Execute(IQueryable<T> query, ILimitsResolver<T> resolver);
    }

    [Serializable]
    public class SkipItem<T> : LimitsItem<T>
    {
        public SkipItem(int value) : base(value) { }

        public override IQueryable<T> Execute(IQueryable<T> query, ILimitsResolver<T> resolver)
        {
            return resolver.Skip(query, Value);
        }
    }

    [Serializable]
    public class TakeItem<T> : LimitsItem<T>
    {
        public TakeItem(int value) : base(value) { }

        public override IQueryable<T> Execute(IQueryable<T> query, ILimitsResolver<T> resolver)
        {
            return resolver.Take(query, Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Expressions
{
    public class EmptyQueryable<T> : IOrderedQueryable<T>
    {
        public EmptyQueryable(string paramName) : this(Expression.Parameter(typeof(IQueryable<T>), paramName)) { }

        public EmptyQueryable(Expression expression) : this(new EmptyProvider<T>(), expression)
        {
        }

        public EmptyQueryable(IQueryProvider provider, Expression expression)
        {
            Expression = expression;
            Provider = provider;
        }

        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }


    public class EmptyProvider<T> : IQueryProvider
    {
        public EmptyProvider()
        {
        }

        public virtual IQueryable CreateQuery(Expression expression)
        {
            return CreateQuery<T>(expression);
        }

        // Queryable's collection-returning standard query operators call this method.
        public virtual IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return new EmptyQueryable<TResult>(this, expression);
        }

        public virtual object Execute(Expression expression)
        {
            return Execute<object>(expression);
        }

        // Queryable's "single value" standard query operators call this method.
        // It is also called from QueryableTerraServerData.GetEnumerator().
        public virtual TResult Execute<TResult>(Expression expression)
        {
            return default(TResult);
        }
    }
}

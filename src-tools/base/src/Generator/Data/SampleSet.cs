using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using System.Linq.Expressions;

namespace Sample.Project.Generator.Data
{
    public abstract class SampleSet<T> : ISampleSet
        where T:IEntity<T>
    {

        public abstract IEnumerable<string> Environments { get; }
        public abstract Expression<Func<T, bool>> ExistsPredicate(T entity);

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}

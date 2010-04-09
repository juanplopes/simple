using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Entities
{
    public interface IEntity
    {
    }
    public interface IEntity<T> : IEntity
    {
        T Clone();
        T Refresh();
        T Reload();
        T Merge();
        T Evict();
        T Persist();
        T Save();
        T Update();
        void Delete();
        T SaveOrUpdate();
        IList<ValidationItem> Validate();
        IList<ValidationItem> Validate(string propName);
        IList<ValidationItem> Validate<P>(Expression<Func<T, P>> expr);
    }

}


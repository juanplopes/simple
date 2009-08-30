using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Entities
{
    public interface IEntity
    {
    }
    public interface IEntity<T> : IEntity
    {
        T Clone();
        T Refresh();
        T Merge();
        T Persist();
        T Save();
        T Update();
        void Delete();
        T SaveOrUpdate();
    }

}

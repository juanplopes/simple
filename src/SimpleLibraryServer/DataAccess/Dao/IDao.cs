using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.DataAccess.Dao
{
    public interface IDao<T>
    {
        IList<T> List();
        IEnumerable<T> Enumerable();
        
        T UniqueResult();
        T First();
    }
}

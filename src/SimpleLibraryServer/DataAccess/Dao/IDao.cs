using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLibrary.DataAccess.Dao
{
    public interface IDao<T>
    {
        IList<T> List();
        IEnumerable<T> Enumerable();
        
        T UniqueResult();
        T First();
    }
}

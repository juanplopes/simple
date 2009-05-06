using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Threading
{
    public interface IDataStoreLockToken : ILockToken
    {
        object Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Threading
{
    public interface IDataStoreLockToken : ILockToken
    {
        object Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.Threading
{
    public class NHLockRow
    {
        public virtual NHLockId Id { get; set; }
        public virtual byte[] Data { get; set; }
    }
}

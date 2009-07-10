using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Persistence;

namespace Test
{
    [Serializable]
    public class Test2 : PersistedInstanceState<Test2>
    {
        public string oi { get; set; }
    }
}

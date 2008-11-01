using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Threading;

namespace BasicLibrary.Common
{
    public class BasicCache
    {
        ThreadData<BasicCache> data = new ThreadData<BasicCache>();
        
    }
}

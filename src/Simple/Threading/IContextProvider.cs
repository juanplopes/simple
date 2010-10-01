using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Simple.Threading
{
    public interface IContextProvider
    {
        IDictionary GetStorage();
        void SetStorage(IDictionary storage);
    }
}

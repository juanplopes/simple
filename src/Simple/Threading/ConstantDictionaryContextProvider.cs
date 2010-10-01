using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Simple.Threading
{
    public class DictionaryContextProvider : IContextProvider
    {
        IDictionary dic = new Dictionary<object, object>();
        public IDictionary GetStorage()
        {
            return dic;
        }

        public void SetStorage(IDictionary storage)
        {
            dic = storage;
        }

    }
}

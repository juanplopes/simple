using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;

namespace Simple.Threading
{
    public class GenericContextProvider : IContextProvider
    {
        Guid storageKey = Guid.NewGuid();
        Func<Guid, IDictionary> getter = null;
        Action<Guid, IDictionary> setter = null;

        public GenericContextProvider(Func<Guid, IDictionary> getter, Action<Guid, IDictionary> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public GenericContextProvider(Func<IDictionary> dic)
            : this(x => dic()[x] as IDictionary, (x, y) => dic()[x] = y)
        {
        }


        public IDictionary GetStorage()
        {
            IDictionary dic = null;
            try
            {
                dic = getter(storageKey) as IDictionary;
            }
            catch(KeyNotFoundException) { }

            if (dic == null)
            {
                dic = new Hashtable();
                setter(storageKey, dic);
            }
            return dic;
        }

        public void SetStorage(IDictionary storage)
        {
            setter(storageKey, storage);
        }
    }
}

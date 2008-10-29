using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Common;
using BasicLibrary.Threading;

namespace BasicLibrary.Persistence
{
    [Serializable]
    public abstract class GenericInstanceState<T, PType, TType> : IDisposable, IInitializable
        where T : GenericInstanceState<T, PType, TType>, new()
        where PType : IDataStoreLockingProvider<TType>, new()
        where TType : IDataStoreLockToken
    {
        [NonSerialized]
        protected TType Token;
        [NonSerialized]
        protected PType Provider;

        public static T Get(int id)
        {
            return Get(id, TimeoutValues.DefaultWait);
        }

        public static T Get(int id, int timeout)
        {
            PType provider = new PType();
            TType token = provider.Lock(typeof(T).FullName, id, timeout);
            T ret = provider.GetData<T>(token);

            ret.Token = token;
            ret.Provider = provider;

            return ret;
        }

        public void Remove()
        {
            Provider.RemoveData(Token);
        }

        public void Persist()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            if (Token != null)
            {
                Provider.SetData(Token, this);
            }
            else
            {
                throw new InvalidOperationException("Cannot persist with actual state");
            }

        }

        public virtual void Init() { }
    }
}

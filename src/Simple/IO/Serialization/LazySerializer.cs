using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Simple.IO.Serialization
{
    [Serializable]
    public abstract class LazySerializer<TReal, TProxy> : ISerializable
        where TReal : class
        where TProxy : class
    {
        public bool IsNull { get; protected set; }

        public bool IsRealActivated { get; protected set; }
        TReal real = null;
        public TReal Real
        {
            get
            {
                if (!IsRealActivated)
                {
                    if (!IsNull)
                        real = TransformToReal(proxy);
                    else
                        real = null;

                    IsRealActivated = true;
                }
                return real;
            }
            set
            {
                real = value;
                IsRealActivated = true;

                if (real == null)
                    IsNull = true;
            }
        }

        public bool IsProxyActivated { get; protected set; }
        TProxy proxy = null;
        public TProxy Proxy
        {
            get
            {
                if (!IsProxyActivated)
                {
                    if (real != null)
                        proxy = TransformToProxy(real);
                    else
                        proxy = null;

                    IsProxyActivated = true;
                }
                return proxy;
            }
            protected set
            {
                proxy = value;
                IsProxyActivated = true;
            }
        }

        protected abstract TReal TransformToReal(TProxy proxy);
        protected abstract TProxy TransformToProxy(TReal real);


        public LazySerializer(TReal real)
        {
            this.Real = real;
        }

        #region ISerializable Members

        protected LazySerializer(SerializationInfo info, StreamingContext context)
        {
            this.Proxy = (TProxy)info.GetValue("Proxy", typeof(TProxy));
            this.IsNull = info.GetBoolean("IsNull");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Proxy", Proxy);
            info.AddValue("IsNull", IsNull);
        }

        #endregion
    }
}

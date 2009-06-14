using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Simple.Persistence;
using System.IO;
using System.Xml;
using Simple.IO;

namespace Simple.ConfigSource
{
    public class XmlConfigSource<T> : 
        IConfigSource<T>,
        IConfigSource<T, Stream>,
        IConfigSource<T, string>,
        IConfigSource<T, XmlNode>,
        IConfigSource<T, XmlDocument>
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(T));

        public virtual T Load(Stream stream)
        {
            return (T)serializer.Deserialize(stream);
        }

        public event HandleConfigExpired<T> Expired;
        protected virtual void InvokeExpired()
        {
            if (Expired != null)
                Expired.Invoke(this);
        }

        public virtual T Load(string input)
        {
            using (var stream = XmlHelper.GetStream(input))
            {
                return Load(stream);
            }
        }

        public virtual T Load(XmlNode input)
        {
            using (var stream = XmlHelper.GetStream(input))
            {
                return Load(stream);
            }
        }

        public virtual T Load(XmlDocument input)
        {
            return Load(input.DocumentElement);
        }

        public virtual T Reload()
        {
            throw new NotSupportedException("This implementation doesn't support reloading");
        }

        #region IDisposable Members

        public virtual void Dispose()
        {            
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Simple.Common;
using Simple.IO.Serialization;

namespace Simple.Persistence
{
    [Serializable]
    public class SelfSerializableClass<T>
    {
        public string ToBase64String()
        {
            return Convert.ToBase64String(SimpleSerializer.Binary().Serialize(this));
        }

        public static T FromBase64String(string pstrBase64)
        {
            return (T)SimpleSerializer.Binary().Deserialize(Convert.FromBase64String(pstrBase64));
        }
    }
}

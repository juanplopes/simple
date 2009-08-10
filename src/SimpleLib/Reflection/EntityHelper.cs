using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Simple.Common;

namespace Simple.Reflection
{
    public class EntityHelper
    {
        protected List<string> _ids = new List<string>();
        protected Type _entityType = null;
        protected object _obj = null;

        public EntityHelper(Type entityType)
        {
            _entityType = entityType;
        }

        public EntityHelper(object obj)
        {
            if (obj == null)
                throw new InvalidOperationException("Cannot use null valued object in EntityHelper constructor");
            _entityType = obj.GetType();
            _obj = obj;
        }

        public void AddID(params string[] propName)
        {
            _ids.AddRange(propName);
        }

        public void AddAllProperties()
        {
            foreach (PropertyInfo info in _entityType.GetProperties())
            {
                this.AddID(info.Name);
            }
        }

        protected Dictionary<string, bool> ToDic(string[] props)
        {
            Dictionary<string, bool> res = new Dictionary<string, bool>();
            foreach (string s in props)
                res[s] = true;

            return res;
        }

        public bool ObjectEquals(object obj1, object obj2, params string[] toIgnore)
        {
            if (obj1 == null ^ obj2 == null) return false;
            if (obj1 == null && obj2 == null) return true;
            if (!_entityType.IsAssignableFrom(obj1.GetType())) return false;
            if (!_entityType.IsAssignableFrom(obj2.GetType())) return false;

            var ignore = ToDic(toIgnore);
            foreach (string idProp in _ids)
            {
                if (ignore.ContainsKey(idProp)) continue;

                PropertyInfo info = _entityType.GetProperty(idProp);
                object value1 = info.GetValue(obj1, null);
                object value2 = info.GetValue(obj2, null);

                if (value1 == null ^ value2 == null) return false;
                if (value1 != null && value2 != null && !value1.Equals(value2)) return false;
            }

            return true;
        }

        public bool ObjectEquals(object obj2, params string[] toIgnore)
        {
            if (_obj == null)
                throw new InvalidOperationException("Intern object reference not intialized");

            return ObjectEquals(_obj, obj2, toIgnore);
        }

        public int ObjectGetHashCode(object obj, params string[] toIgnore)
        {
            if (obj == null) return 1;
            if (!_entityType.IsAssignableFrom(obj.GetType())) return -1;

            IEnumerator<int> primes = PrimeNumbers.GetPrimesEnumerable().GetEnumerator();
            int res = 1;

            var ignore = ToDic(toIgnore);
            foreach (string idProp in _ids)
            {
                if (ignore.ContainsKey(idProp)) continue;

                primes.MoveNext();
                PropertyInfo info = _entityType.GetProperty(idProp);
                if (info == null) throw new InvalidOperationException("Property not found: " + idProp);

                object value = info.GetValue(obj, null);
                if (value != null)
                {
                    res *= (value.GetHashCode() * primes.Current);
                }
            }
            return res;
        }

        public int ObjectGetHashCode(params string[] toIgnore)
        {
            return ObjectGetHashCode(_obj, toIgnore);
        }

        protected static string GetToString(object obj, params string[] toIgnore)
        {
            if (obj == null) return "<null>";
            return obj.ToString();
        }

        public string ObjectToString(object obj, params string[] toIgnore)
        {
            if (obj == null) return "<null>";
            if (!_entityType.IsAssignableFrom(obj.GetType())) throw new InvalidOperationException("Invalid object type");

            string[] response = new string[_ids.Count];

            var ignore = ToDic(toIgnore);
            for (int i = 0; i < _ids.Count; i++)
            {
                if (ignore.ContainsKey(_ids[i])) continue;

                response[i] = _ids[i] + "=" + GetToString(_entityType.GetProperty(_ids[i]).GetValue(obj, null));
            }

            return string.Join(" | ", response);
        }

        public string ObjectToString(params string[] toIgnore)
        {
            return ObjectToString(_obj, toIgnore);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Simple.Common;
using System.Linq.Expressions;
using Simple.Expressions;
using System.Linq;

namespace Simple.Reflection
{
    public class EntityHelper<T> : EntityHelper
    {
        public EntityHelper()
            : this(new Expression<Func<T, object>>[0])
        {
        }

        public EntityHelper(params Expression<Func<T, object>>[] ids)
            : base(typeof(T))
        {
            foreach (var id in ids)
                AddID<T>(id);
        }

        public EntityHelper<T> AddID(Expression<Func<T, object>> expr)
        {
            this.AddID<T>(expr);
            return this;
        }
    }

    public class EntityHelper
    {
        protected MethodCache _cache = new MethodCache();
        protected List<string> _ids = new List<string>();
        protected Type _entityType = null;
        protected object _obj = null;

        public bool HasIdentifiers { get { return _ids.Count > 0; } }

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

        public EntityHelper AddID(string propName)
        {
            _ids.Add(propName);
            return this;
        }

        public EntityHelper AddID<T>(Expression<Func<T, object>> expr)
        {
            AddID(ExpressionHelper.GetMemberName(expr));
            return this;
        }

        public void AddAllProperties()
        {
            foreach (PropertyInfo info in _entityType.GetProperties())
            {
                this.AddID(info.Name);
            }
        }

        protected HashSet<string> ToHashSet(string[] props)
        {
            return new HashSet<string>(props);
        }

        public bool ObjectEquals(object obj1, object obj2, params string[] toIgnore)
        {
            if (obj1 == null ^ obj2 == null) return false;
            if (obj1 == null && obj2 == null) return true;
            if (!_entityType.IsAssignableFrom(obj1.GetType())) return false;
            if (!_entityType.IsAssignableFrom(obj2.GetType())) return false;

            var ignore = ToHashSet(toIgnore);
            foreach (string idProp in _ids)
            {
                if (ignore.Contains(idProp)) continue;

                PropertyInfo info = _entityType.GetProperty(idProp);
                InvocationDelegate getter = _cache.GetGetter(info);
                object value1 = getter(obj1, null);
                object value2 = getter(obj2, null);

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

            int res = 1;

            var ignore = ToHashSet(toIgnore);
            foreach (string idProp in _ids)
            {
                if (ignore.Contains(idProp)) continue;

                PropertyInfo info = _entityType.GetProperty(idProp);
                if (info == null) throw new InvalidOperationException("Property not found: " + idProp);

                InvocationDelegate getter = _cache.GetGetter(info);
                object value = getter(obj, null);
                if (value != null)
                {
                    res ^= value.GetHashCode();
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

            var ignore = ToHashSet(toIgnore);
            for (int i = 0; i < _ids.Count; i++)
            {
                if (ignore.Contains(_ids[i])) continue;

                response[i] = _ids[i] + "=" + GetToString(_entityType.GetProperty(_ids[i]).GetValue(obj, null));
            }

            return "(" + string.Join(" | ", response) + ")";
        }

        public string ObjectToString(params string[] toIgnore)
        {
            return ObjectToString(_obj, toIgnore);
        }
    }
}

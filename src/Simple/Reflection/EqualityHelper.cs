using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;
using System.Linq;


namespace Simple.Reflection
{
    public class EqualityHelper<T> : EqualityHelper
    {
        public EqualityHelper()
            : this(new Expression<Func<T, object>>[0])
        {
        }

        public EqualityHelper(params Expression<Func<T, object>>[] ids)
            : base(typeof(T))
        {
            foreach (var id in ids)
                Add<T>(id);
        }

        public EqualityHelper<T> Add(Expression<Func<T, object>> expr)
        {
            this.Add<T>(expr);
            return this;
        }

        public EqualityHelper<T> Add(Expression<Func<T, object>> expr, IEqualityComparer comparer)
        {
            this.Add<T>(expr, comparer);
            return this;
        }
    }

    public class EqualityHelper
    {
        public class Entry
        {
            public string Property { get; set; }
            public IEqualityComparer Comparer { get; set; }

            public Entry(string property) : this(property, EqualityComparer<object>.Default)
            {
            }
            public Entry(string property, IEqualityComparer comparer)
            {
                Property = property;
                Comparer = comparer;
            }
            public override string ToString()
            {
                return Property;
            }
          
        }

        protected MethodCache _cache = new MethodCache();
        protected List<Entry> _ids = new List<Entry>();
        protected Type _entityType = null;
        protected object _obj = null;

        public bool HasIdentifiers { get { return _ids.Count > 0; } }

        public IEnumerable<Entry> IdentifierList
        {
            get
            {
                return _ids;
            }
        }

        public IEnumerable<string> IdentifierNamesList
        {
            get
            {
                return _ids.Select(x => x.Property);
            }
        }

        public EqualityHelper(Type entityType)
        {
            _entityType = entityType;
        }

        public EqualityHelper(object obj)
        {
            if (obj == null)
                throw new InvalidOperationException("Cannot use null valued object in EntityHelper constructor");
            _entityType = obj.GetType();
            _obj = obj;
        }

        public EqualityHelper ConvertTo(Type type)
        {
            return new EqualityHelper(type).AddMany(_ids);
        }

        public EqualityHelper<T> ConvertTo<T>()
        {
            var helper = new EqualityHelper<T>();
            helper.AddMany(_ids);
            return helper;
        }

        public EqualityHelper Add(string propName)
        {
            _ids.Add(new Entry(propName));
            return this;
        }

        public EqualityHelper Add(string propName, IEqualityComparer comparer)
        {
            _ids.Add(new Entry(propName, comparer));
            return this;
        }


        public EqualityHelper AddMany(IEnumerable<Entry> props)
        {
            _ids.AddRange(props);
            return this;
        }

        public EqualityHelper Add<T>(Expression<Func<T, object>> expr)
        {
            Add(ExpressionHelper.GetMemberName(expr));
            return this;
        }

        public EqualityHelper Add<T>(Expression<Func<T, object>> expr, IEqualityComparer comparer)
        {
            Add(ExpressionHelper.GetMemberName(expr), comparer);
            return this;
        }

        public void AddAllProperties()
        {
            foreach (PropertyInfo info in _entityType.GetProperties())
            {
                this.Add(info.Name);
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
            foreach (var idProp in _ids)
            {
                if (ignore.Contains(idProp.Property)) continue;

                PropertyInfo info = _entityType.GetProperty(idProp.Property);
                InvocationDelegate getter = _cache.GetGetter(info);
                object value1 = getter(obj1, null);
                object value2 = getter(obj2, null);

                if (!idProp.Comparer.Equals(value1, value2)) return false;
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
            foreach (var idProp in _ids)
            {
                if (ignore.Contains(idProp.Property)) continue;

                PropertyInfo info = _entityType.GetProperty(idProp.Property);
                if (info == null) throw new InvalidOperationException("Property not found: " + idProp);

                InvocationDelegate getter = _cache.GetGetter(info);
                object value = getter(obj, null);
                if (value != null)
                {
                    res ^= idProp.Comparer.GetHashCode(value);
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
                if (ignore.Contains(_ids[i].Property)) continue;

                response[i] = _ids[i] + "=" + GetToString(_entityType.GetProperty(_ids[i].Property).GetValue(obj, null));
            }

            return "(" + string.Join(" | ", response) + ")";
        }

        public string ObjectToString(params string[] toIgnore)
        {
            return ObjectToString(_obj, toIgnore);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;
using System.Linq;


namespace Simple.Reflection
{
    public class EqualityHelper<T> : EqualityHelper, IEqualityComparer<T>
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

        #region IEqualityComparer<T> Members

        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            return this.ObjectEquals(x, y);
        }

        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return this.ObjectGetHashCode(obj);
        }

        #endregion
    }

    public class EqualityHelper : IEqualityComparer
    {


        protected MethodCache _cache = new MethodCache();
        protected List<EqualityHelperEntry> _ids = new List<EqualityHelperEntry>();
        protected Type _entityType = null;
        protected object _obj = null;

        public bool HasIdentifiers { get { return _ids.Count > 0; } }

        public IEnumerable<EqualityHelperEntry> IdentifierList
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
                return _ids.Select(x => x.Property.Name).ToArray();
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
            return Add(propName, null);
        }

        public EqualityHelper Add(string propName, IEqualityComparer comparer)
        {
            var property = propName.GetMember(_entityType);
            return Add(property, comparer);
        }

        public EqualityHelper Add(IProperty property)
        {
            return Add(property, null);
        }

        public EqualityHelper Add(IProperty property, IEqualityComparer comparer)
        {

            _ids.Add(new EqualityHelperEntry(property, comparer));
            return this;
        }

        public EqualityHelper AddMany(IEnumerable<EqualityHelperEntry> props)
        {
            _ids.AddRange(props);
            return this;
        }

        public EqualityHelper Add<T>(Expression<Func<T, object>> expr)
        {
            Add(expr.GetMemberName());
            return this;
        }

        public EqualityHelper Add<T>(Expression<Func<T, object>> expr, IEqualityComparer comparer)
        {
            Add(expr.GetMemberName(), comparer);
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
                if (ignore.Contains(idProp.Property.Name)) continue;

                object value1 = idProp.Property.Get(obj1);
                object value2 = idProp.Property.Get(obj2);

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
                if (ignore.Contains(idProp.Property.Name)) continue;

                object value = idProp.Property.Get(obj);
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

            var ignore = ToHashSet(toIgnore);

            var response = _ids.Where(x => !ignore.Contains(x.Property.Name))
                .Select(x => x.Property.Name + "=" + (x.Property.Get(obj) ?? "<null>")).ToArray();

            return "(" + string.Join(" | ", response) + ")";
        }

        public string ObjectToString(params string[] toIgnore)
        {
            return ObjectToString(_obj, toIgnore);
        }

        #region IEqualityComparer Members

        int IEqualityComparer.GetHashCode(object obj)
        {
            return this.ObjectGetHashCode(obj);
        }

        bool IEqualityComparer.Equals(object x, object y)
        {
            return this.ObjectEquals(x, y);
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Simple.Patterns
{
    [Serializable]
    public class DictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Base { get; set; }

        public DictionaryWrapper() : this(new Dictionary<TKey, TValue>()) { }

        public DictionaryWrapper(IDictionary<TKey, TValue> baseDictionary)
        {
            Base = baseDictionary;
        }

        #region IDictionary<TKey,TValue> Members

        public virtual void Add(TKey key, TValue value)
        {
            Base.Add(key, value);
        }

        public virtual bool ContainsKey(TKey key)
        {
            return Base.ContainsKey(key);
        }

        public virtual ICollection<TKey> Keys
        {
            get { return Base.Keys; }
        }

        public virtual bool Remove(TKey key)
        {
            return Base.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = this[key];
                return true;
            }
            catch (KeyNotFoundException)
            {
                value = default(TValue);
                return false;
            }
        }

        public virtual ICollection<TValue> Values
        {
            get { return Base.Values; }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                return Base[key];
            }
            set
            {
                Base[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        public virtual void Clear()
        {
            Base.Clear();
        }

        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Base.Contains(item);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Base.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get { return Base.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return Base.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Base.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (Base as IEnumerable).GetEnumerator();
        }

        #endregion
    }
}

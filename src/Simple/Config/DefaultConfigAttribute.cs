using System;
using System.Reflection;
using Simple.Reflection;

namespace Simple.Config
{
    /// <summary>
    /// Used to determine the default configuration object key in come contexts.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple=false, Inherited=true)]
    public class DefaultConfigAttribute : Attribute
    {
        /// <summary>
        /// The configuration object key.
        /// </summary>
        public object Key { get; protected set; }

        /// <summary>
        /// Initializes attribute with default key.
        /// </summary>
        /// <param name="key">The key.</param>
        public DefaultConfigAttribute(object key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key for the specified reflected member.
        /// </summary>
        /// <param name="info">A reflected <see cref="MemberInfo"/>. May be an instance of <see cref="Type"/></param>
        /// <returns>The default key for it.</returns>
        public static object GetKey(MemberInfo info)
        {
            DefaultConfigAttribute attr = AttributeCache.Do.First<DefaultConfigAttribute>(info);
            object key = attr == null ? null : attr.Key;
            return key;
        }
    }
}

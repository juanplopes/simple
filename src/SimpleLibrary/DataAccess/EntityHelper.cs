using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Proxy;
using Castle.DynamicProxy;
using NHibernate.Engine;
using SimpleLibrary.Config;
using System.Reflection;
using NHibernate.Collection;
using System.Collections;
using NHibernate.Persister.Collection;

namespace SimpleLibrary.DataAccess
{
    public class EntityHelper
    {
        protected static void InternalUnproxy(object graph, HashSet<object> alreadyVisited)
        {
            if (alreadyVisited.Contains(graph)) return;
            alreadyVisited.Add(graph);

            foreach (PropertyInfo info in graph.GetType().GetProperties())
            {
                if (info.GetIndexParameters().Length > 0) continue;
                object value = info.GetValue(graph, null);
                if (value is IPersistentCollection)
                {
                    value = ((IPersistentCollection)value).StoredSnapshot;
                    info.SetValue(graph, value, null);
                }

                if (value is IEnumerable)
                {
                    foreach (object obj in ((IEnumerable)value))
                    {
                        InternalUnproxy(obj, alreadyVisited);
                    }
                }
                else
                {
                    InternalUnproxy(value, alreadyVisited);
                }
            }
        }

        public static object Unproxy(object graph, IPersistenceContext context)
        {
            graph = context.Unproxy(graph);
            InternalUnproxy(graph, new HashSet<object>());
            return graph;
        }
    }
}

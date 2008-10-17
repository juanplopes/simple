using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace SimpleLibrary.EntityPruner
{
    public class Pruner
    {
        protected HashSet<object> State { get; set; }
        protected object ObjToPrune { get; set; }
        protected IDictionary<PruneType, int> Params { get; set; }
        protected int Pruned { get; set; }

        public static void PruneObject(object obj, Type desiredType, int maxItems, int maxDepth)
        {
            Pruner pruner = new Pruner(obj);
            if (maxItems > 0) pruner.SetParam(PruneType.ByNumberOfItems, maxItems);
            if (maxDepth > 0) pruner.SetParam(PruneType.ByDepth, maxDepth);

            pruner.Prune(desiredType);
        }

        public static void PruneObject(object obj, int maxItems, int maxDepth)
        {
            PruneObject(obj, obj.GetType(), maxItems, maxDepth);
        }

        protected Pruner(object obj)
        {
            ObjToPrune = obj;
            State = new HashSet<object>();
            Params = new Dictionary<PruneType, int>();
        }

        protected void SetParam(PruneType type, int value)
        {
            Params[type] = value;
        }

        protected object Prune(Type desiredType)
        {
            return Prune(ObjToPrune, desiredType, 1);
        }

        protected IEnumerable PruneEnumerable(IEnumerable enumerable, Type desiredType, int depth)
        {
            foreach (object obj in enumerable)
            {
                State.Add(obj);
                Prune(obj,null, depth + 1);
            }
            return enumerable;
        }

        protected object Prune(object obj, Type desiredType, int depth)
        {
            if (obj is IEnumerable) return PruneEnumerable(obj as IEnumerable, desiredType, depth);
            if (State.Contains(obj)) return obj;

            Pruned++;

            foreach (PropertyInfo prop in desiredType.GetProperties())
            {
                if ((depth >= Params[PruneType.ByDepth] || Pruned >= Params[PruneType.ByNumberOfItems]) &&
                    !State.Contains(obj))
                {
                    prop.SetValue(obj, CreateDefault(prop.PropertyType), null);
                }
                else
                {
                    object propValue = prop.GetValue(obj, null);
                    Prune(propValue, prop.PropertyType, depth + 1);
                }
            }
            return obj;
        }

        protected object CreateDefault(Type t)
        {
            if (!t.IsValueType)
            {
                return null;
            }
            else
            {
                return Activator.CreateInstance(t);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using BasicLibrary.Common;

namespace BasicLibrary.Reflection
{
    /// <summary>
    /// Class for Field-To-Field or EnumValue cast helping.
    /// </summary>
    /// <typeparam name="T">The casting destination type.</typeparam>
    public class CastingHelper<T> where T : new()
    {
        public delegate T CastingDelegate(object pobjObject);
        public delegate void CastListDelegate(object pobjObject, IList<T> plstInstance);

        public T Cast(object pobjObject)
        {
            if (pobjObject is Enum)
            {
                return CastEnum(pobjObject);
            }
            else
            {
                return CastObject(pobjObject);
            }
        }

        public void Cast(object pobjObject, T pobjReturn)
        {
            if (pobjObject is Enum)
            {
                pobjReturn = CastEnum(pobjObject);
            }
            else
            {
                CastObject(pobjObject, pobjReturn);
            }
        }

        public IList<T> CastList(IEnumerable pobjEnumerable)
        {
            return CastList(pobjEnumerable, new CastingHelper<T>.CastingDelegate(this.Cast));
        }

        public IList<T> CastList(IEnumerable pobjEnumerable, CastListDelegate pobjDelegate)
        {
            if (pobjEnumerable == null) return null;

            IList<T> llstResult = new List<T>();
            foreach (object lobjInstance in pobjEnumerable)
            {
                pobjDelegate.Invoke(lobjInstance, llstResult);
            }
            return llstResult;
        }

        public IList<T> CastList(IEnumerable pobjEnumerable, CastingDelegate pobjDelegate)
        {
            IList<T> llstResult = new List<T>();
            foreach (object lobjInstance in pobjEnumerable)
            {
                llstResult.Add(pobjDelegate.Invoke(lobjInstance));
            }
            return llstResult;
        }

        private void CastList(object pobjObject, IList<T> plstReturn)
        {
            plstReturn.Add(CastObject(pobjObject));
        }

        private T CastObject(object pobjObject)
        {
            T lobjReturn = new T();
            CastObject(pobjObject, lobjReturn);

            return lobjReturn;
        }

        private void CastObject(object pobjObject, T pobjReturn)
        {
            CastObject(pobjObject, pobjReturn, false);
        }

        private void CastObject(object pobjObject, T pobjReturn, bool pblnOverrideNulls)
        {
            Type lobjFromType = pobjObject.GetType();
            Type lobjToType = typeof(T);

            foreach (PropertyInfo lobjProperty in lobjFromType.GetProperties())
            {
                PropertyInfo lobjToProp;
                if ((lobjToProp = lobjToType.GetProperty(lobjProperty.Name)) != null)
                {
                    object lobjValue = UltraCaster.TryCast(lobjProperty.GetValue(pobjObject, null), lobjToProp.PropertyType); ;

                    if (pblnOverrideNulls ||
                        (lobjValue is ICastable && !(lobjValue as ICastable).IsNull) ||
                        (!(lobjValue is ICastable) && lobjValue != null))
                        lobjToProp.SetValue(pobjReturn, lobjValue, null);
                }
            }
        }

        private T CastEnum(object penmObject)
        {
            return (T)Enum.Parse(typeof(T), penmObject.ToString());
        }
    }
}

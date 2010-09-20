using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Common;
using System.Globalization;
using System.Collections.ObjectModel;

namespace Simple.Web.Mvc
{
    public class ModelSelectList<T> : ReadOnlyCollection<SelectListItem>, IModelSelectList<T>
    {
        public Func<T, object> ValueSelector { get; protected set; }
        public Func<T, object> TextSelector { get; set; }

        protected ModelSelectList(IList<SelectListItem> items, Func<T, object> valueSelector, Func<T, object> textSelector)
            : base(items)
        {
            this.ValueSelector = valueSelector;
            this.TextSelector = textSelector;
        }

        public ModelSelectList(IEnumerable<T> items, Func<T, object> valueSelector, Func<T, object> textSelector)
            : this(Transform(items, valueSelector, textSelector).ToList(), valueSelector, textSelector)
        {
        }

        protected static IEnumerable<SelectListItem> Transform(IEnumerable<T> items, Func<T, object> valueSelector, Func<T, object> textSelector)
        {
            if (items == null) yield break;

            foreach (var item in items)
            {
                yield return new SelectListItem()
                {
                    Text = string.Format("{0}", textSelector(item)),
                    Value = string.Format("{0}", valueSelector(item)),
                };
            }
        }

        public IModelSelectList Sort()
        {
            return new ModelSelectList<T>(this.OrderBy(x => x.Text).ToList(), ValueSelector, TextSelector);
        }

        public IModelSelectList<Q> Select<Q>(params Q[] models)
        {
            return this.As<Q>().Select(models);
        }

        public IModelSelectList<T> Select(params T[] models)
        {
            models = models ?? new T[0];
            var selectedValues = models.Select(x => SafeNullable.Get(() => ValueSelector(x))).ToArray();
            return SelectValue(selectedValues).As<T>();
        }

        public IModelSelectList<Q> As<Q>()
        {
            return (IModelSelectList<Q>)this;
        }


        public IModelSelectList SelectValue(params object[] selectedValues)
        {
            selectedValues = selectedValues ?? new object[0];
            var selectedStrings = new HashSet<string>(selectedValues.Select(x => string.Format("{0}", x)));
            var list = new List<SelectListItem>();
            foreach (var item in this)
            {
                if (selectedStrings.Contains(item.Value))
                    list.Add(new SelectListItem() { Text = item.Text, Value = item.Value, Selected = true });
                else
                    list.Add(item);
            }

            return new ModelSelectList<T>(list, ValueSelector, TextSelector);
        }

        public IModelSelectList ClearSelection()
        {
            var list = new List<SelectListItem>();
            foreach (var item in this)
            {
                if (item.Selected)
                    list.Add(new SelectListItem() { Text = item.Text, Value = item.Value });
                else
                    list.Add(item);
            }

            return new ModelSelectList<T>(list, ValueSelector, TextSelector);
        }

    }
}

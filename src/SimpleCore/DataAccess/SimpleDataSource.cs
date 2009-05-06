using System;
using System.Collections.Generic;

using System.Text;
using System.Web.UI;
using Simple.Rules;
using Simple.Filters;
using System.Data;

namespace Simple.DataAccess
{
    public class SimpleDataSource
    {
        public static SimpleDataSource<T> Create<T>(IBaseRules<T> rules)
        {
            return new SimpleDataSource<T>(rules);
        }
    }

    public class SimpleDataSource<T> : IDataSource
    {
        protected IList<PropertyDescriptionMapping> Mappings { get; set; }
        protected IBaseRules<T> Rules { get; set; }
        public Filter Filters { get; set; }

        public SimpleDataSource(IBaseRules<T> rules)
        {
            Filters = BooleanExpression.True;
            Mappings = new List<PropertyDescriptionMapping>();
            Rules = rules;
        }

        public void AddPropertyMapping(string source, string dest)
        {
            Mappings.Add(new PropertyDescriptionMapping(source, dest));
        }

        public void ClearPropertyMapping()
        {
            Mappings.Clear();
        }

        public event EventHandler DataSourceChanged;
        public void InvokeDataSourceChanged(EventArgs e)
        {
            DataSourceChanged(this, e);
        }
        public DataSourceView GetView()
        {
            return GetView(string.Empty);
        }

        public DataSourceView GetView(string viewName)
        {
            return new SimpleDataSourceView<T>(this, Rules, Filters, Mappings, viewName);
        }

        public System.Collections.ICollection GetViewNames()
        {
            return new string[] { string.Empty };
        }

    }
}

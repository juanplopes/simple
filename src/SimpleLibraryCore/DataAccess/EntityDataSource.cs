using System;
using System.Collections.Generic;

using System.Text;
using System.Web.UI;
using SimpleLibrary.Rules;

namespace SimpleLibrary.DataAccess
{
    public class EntityDataSource<T, R> : IDataSource
        where R : class, IBaseRules<T>
    {
        public IList<PropertyDescriptionMapping> Mappings { get; set; }

        public EntityDataSource()
        {
            Mappings = new List<PropertyDescriptionMapping>();
        }

        public void AddPropertyMapping(string source, string dest)
        {
            Mappings.Add(new PropertyDescriptionMapping(source, dest));
        }

        public event EventHandler DataSourceChanged;

        public DataSourceView GetView(string viewName)
        {
            return new EntityDataSourceView<T, R>(this, viewName, Mappings);
        }

        public System.Collections.ICollection GetViewNames()
        {
            return new string[] { string.Empty };
        }

    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Web.UI;
using Simple.Filters;
using Simple.Rules;
using System.Data;

namespace Simple.DataAccess
{
    public class SimpleDataSourceView<T> : DataSourceView
    {
        protected IList<PropertyDescriptionMapping> Mappings { get; set; }
        protected IBaseRules<T> Rules { get; set; }
        public Filter Filters { get; set; }

        public SimpleDataSourceView(IDataSource owner, IBaseRules<T> rules, Filter filters, IList<PropertyDescriptionMapping> mappings, string viewName) :
            base(owner, viewName)
        {
            this.Mappings = mappings;
            this.Rules = rules;
            this.Filters = filters;
        }

        protected override System.Collections.IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
        {
            OrderByCollection col = SortExpressionAnalyzer.Analyze(arguments.SortExpression);
            EntityTransformer<T> transformer = new EntityTransformer<T>(Mappings);

            if (arguments.MaximumRows > 0)
            {
                IPage<T> page = Rules.PaginateByFilter(Filters, col, arguments.StartRowIndex, arguments.MaximumRows);
                arguments.TotalRowCount = page.TotalCount;
                return transformer.ToEnumerableDictionary(page);
            }
            else
            {
                IList<T> page = Rules.ListByFilter(Filters, col);
                arguments.TotalRowCount = page.Count;
                return transformer.ToEnumerableDictionary(page);
            }

        }

        public override bool CanSort
        {
            get
            {
                return true;
            }
        }

        public override bool CanRetrieveTotalRowCount
        {
            get
            {
                return true;
            }
        }

        public override bool CanPage
        {
            get
            {
                return true;
            }
        }
    }
}

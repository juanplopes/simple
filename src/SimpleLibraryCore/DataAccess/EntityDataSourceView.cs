using System;
using System.Collections.Generic;

using System.Text;
using System.Web.UI;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;

namespace SimpleLibrary.DataAccess
{
    public class EntityDataSourceView<T, R> : DataSourceView
        where R : class, IBaseRules<T>
    {
        public IList<PropertyDescriptionMapping> Mappings { get; set; }

        public EntityDataSourceView(IDataSource owner, string viewName, IList<PropertyDescriptionMapping> mappings) :
            base(owner, viewName)
        {
            this.Mappings = mappings;
        }

        protected override System.Collections.IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
        {
            OrderByCollection col = SortExpressionAnalyzer.Analyze(arguments.SortExpression);

            R rules = RulesFactory.Create<R>();

            if (arguments.MaximumRows > 0)
            {
                Page<T> page = rules.PageByFilter(BooleanExpression.True, col, arguments.StartRowIndex, arguments.MaximumRows);
                return page.Items;
            }
            else
            {
                IList<T> page = rules.ListByFilter(BooleanExpression.True, col);
                return page;
            }
        }
    }
}

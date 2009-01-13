using System.Collections.Generic;
using SimpleLibrary.Rules;
using Sample.BusinessInterface.Rules;
using SimpleLibrary.Filters;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface.Domain;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();

            IPage<Empresa> page = rules.PaginateByFilter(BooleanExpression.True, OrderBy.None(), 30, 20);
            page.TotalPages.ToString();
        }
    }
}


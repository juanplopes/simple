using System.Collections.Generic;
using Simple.Services;
using Simple.Tests.Contracts;
using System.Linq;

namespace Sample.BusinessServer.Rules
{
    public partial class EmpresaRules : BaseRules<Empresa>, IEmpresaRules
    {

        public Empresa GetByNameLinq(string name)
        {
            var query = from e in Linq()
                        where e.Nome == name
                        select e;
            return query.First();

        }


    }
}
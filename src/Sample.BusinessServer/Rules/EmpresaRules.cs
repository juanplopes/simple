using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface;
using SimpleLibrary.ServiceModel;
using NHibernate;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using SimpleLibrary.Rules;

namespace Sample.BusinessServer.Rules
{
    public class EmpresaRules : BaseRules<Empresa, EmpresaDao>, IEmpresaRules
    {
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Rules;
using System.ServiceModel;
using SimpleLibrary.ServiceModel;

namespace Sample.BusinessInterface
{
    [ServiceContract]
    [MainContract]
    public interface IEmpresaFuncionarioRules : IBaseRules<EmpresaFuncionario>
    {
    }
}

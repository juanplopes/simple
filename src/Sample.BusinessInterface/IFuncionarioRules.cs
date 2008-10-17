using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SimpleLibrary.Rules;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.ServiceModel;

namespace Sample.BusinessInterface
{
    [ServiceContract]
    [MainContract]
    public interface IFuncionarioRules : IBaseRules<Funcionario>
    {
    }
}

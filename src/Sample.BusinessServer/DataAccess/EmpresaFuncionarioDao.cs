using System;
using Sample.BusinessInterface.Domain;
using Simple.DataAccess;
using NHibernate;

namespace Sample.BusinessServer.DataAccess
{
    public partial class EmpresaFuncionarioDao : BaseDao<EmpresaFuncionario>
    {
        public EmpresaFuncionarioDao() : base() { }
        public EmpresaFuncionarioDao(ISession session) : base(session) { }
        public EmpresaFuncionarioDao(BaseDao previousDao) : base(previousDao) { }
    }
}
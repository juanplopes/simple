using System;
using Sample.BusinessInterface.Domain;
using Simple.DataAccess;
using NHibernate;

namespace Sample.BusinessServer.DataAccess
{
    public partial class FuncionarioDao : BaseDao<Funcionario>
    {
        public FuncionarioDao() : base() { }
        public FuncionarioDao(ISession session) : base(session) { }
        public FuncionarioDao(BaseDao previousDao) : base(previousDao) { }
    }
}
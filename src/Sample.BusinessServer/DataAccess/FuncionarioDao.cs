using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.DataAccess;
using NHibernate;

namespace Sample.BusinessServer.DataAccess
{
    public class FuncionarioDao : BaseDao<Funcionario>
    {
        public FuncionarioDao(ISession session) : base(session){}
        public FuncionarioDao() : base() { }
        public FuncionarioDao(BaseDao previousDao) : base(previousDao) { }
    }
}

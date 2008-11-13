using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.DataAccess;
using NHibernate;

namespace Sample.BusinessServer.DataAccess
{
    public class EmpresaFuncionarioDao : BaseDao<EmpresaFuncionario>
    {
        public EmpresaFuncionarioDao(ISession session) : base(session){}
        public EmpresaFuncionarioDao() : base() { }
        public EmpresaFuncionarioDao(BaseDao previousDao) : base(previousDao) { }

        public override object TestMethod(object obj)
        {
            return null;
        }
    }
}

using System;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.DataAccess;
using NHibernate;

namespace Sample.BusinessServer.DataAccess
{
    public partial class EmpresaDao : BaseDao<Empresa>
    {
        public EmpresaDao() : base() { }
        public EmpresaDao(ISession session) : base(session) { }
        public EmpresaDao(BaseDao previousDao) : base(previousDao) { }
    }
}
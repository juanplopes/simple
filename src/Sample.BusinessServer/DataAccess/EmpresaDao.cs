using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.DataAccess;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace Sample.BusinessServer.DataAccess
{
    public class EmpresaDao : BaseDao<Empresa>
    {
        public EmpresaDao(ISession session) : base(session) { }
        public EmpresaDao() : base() { }
        public EmpresaDao(BaseDao previousDao) : base(previousDao) { }
    }
}

using System;
using Sample.BusinessInterface.Domain;
using Simple.DataAccess;
using NHibernate;

namespace Sample.BusinessServer.DataAccess
{
    public partial class EmpresaDao : BaseDao<Empresa>
    {
        public EmpresaDao() : base() { }
        public EmpresaDao(ISession session) : base(session) { }
        public EmpresaDao(BaseDao previousDao) : base(previousDao) { }

        public override object TestMethod(object obj)
        {
            IQuery q = Session.CreateSQLQuery("select * from empresa");
            q.SetResultTransformer(SimpleTransformers.ToDictionary);
            return null;
        }
    }
}
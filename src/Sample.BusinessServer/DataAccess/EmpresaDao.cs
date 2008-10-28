using System;
using System.Collections.Generic;
using System.Linq;
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

        public IList<Empresa> GetAllWithQuery()
        {
            ISQLQuery query = Session.CreateSQLQuery("select * from empresa where id_empresa=?");

            query.List();

            return null;
        }

        private static void NewMethod(ISQLQuery query)
        {
            query.SetInt32("id_funcionario", 12868);
            query.AddEntity(typeof(Empresa));
            query.AddScalar("cnt", NHibernateUtil.Int32);
        }

        public IList<Empresa> GetAllWithSQLQuery()
        {
            ISQLQuery query = Session.CreateSQLQuery("selecdt empresa.* from empresa");
            query.SetResultTransformer(Transformers.AliasToBean(typeof(TestDTO)));


            return query.List<Empresa>();
        }

        public IList<Empresa> GetAllWithLINQ()
        {
            var query = from e in GetQueryable<Empresa>()
                        where (e.Nome == "Whatever")
                        select e;


            query.Paginate(5, 10);
            return null;
        }

        public IList<Empresa> GetAllWithCriteria()
        {
            ICriteria criteria = CreateCriteria();
            return criteria.List<Empresa>();
        }

        public override object TestMethod(object obj)
        {
            return base.TestMethod(obj);
        }
    }
}

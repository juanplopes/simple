using System;
namespace Simple.DataAccess
{
    interface INHibernateFactory
    {
        NHibernate.Cfg.Configuration Configuration { get; }
        NHibernate.ISession GetSession();
    }
}

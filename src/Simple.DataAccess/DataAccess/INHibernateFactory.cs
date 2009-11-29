using System;
namespace Simple.DataAccess
{
    interface INHibernateFactory
    {
        NHibernate.Cfg.Configuration NHConfiguration { get; }
        NHibernate.ISession OpenNewSession();
    }
}

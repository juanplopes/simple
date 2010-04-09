using System;
namespace Simple.Data
{
    interface INHibernateFactory
    {
        NHibernate.Cfg.Configuration NHConfiguration { get; }
        NHibernate.ISession OpenNewSession();
    }
}

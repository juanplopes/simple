namespace Simple.Data
{
    interface INHibernateFactory
    {
        NHibernate.Cfg.Configuration NHConfiguration { get; set; }
        NHibernate.ISession OpenNewSession();
    }
}

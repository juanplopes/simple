using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using Simple.ConfigSource;

namespace Simple.DataAccess
{
    public class NHibernateSimply : Singleton<NHibernateSimply>, ISimply<NHibernateConfig>
    {
        #region ISimply<NHibernateConfig> Members

        public void Configure(object key, IConfigSource<NHibernateConfig> source)
        {
            SourceManager.RegisterSource(key, new NHibernateConfigSource().Load(source));
        }

        #endregion
    }
}

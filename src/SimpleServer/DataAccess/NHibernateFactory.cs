using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Cfg;
using Simple.ConfigSource;
using NHibernate.Cfg;

namespace Simple.DataAccess
{
    public class NHibernateFactory : Factory<Configuration>
    {
        protected override void Config(Configuration config)
        {
            throw new NotImplementedException();
        }

        public override void InitDefault()
        {
            throw new NotImplementedException();
        }
    }
}

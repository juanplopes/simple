using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using System.Xml;
using NHibernate.Cfg;

namespace Simple.DataAccess
{
    public class NHibernateConfigSource : WrappedConfigSource<Configuration, NHibernateConfig>
    {
        public override Configuration TransformFromInput(NHibernateConfig input)
        {
            Configuration config = new Configuration();
            config.AddXmlString(input.Element.OuterXml);
            return config;
        }
    }
}

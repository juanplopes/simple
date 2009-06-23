using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using System.Xml;
using NHibernate.Cfg;
using Simple.Patterns;

namespace Simple.DataAccess
{
    public class NHibernateConfigSource : WrappedConfigSource<
        TransformationList<Configuration>, NHibernateConfig>
    {
        public override TransformationList<Configuration> TransformFromInput(NHibernateConfig input)
        {
            TransformationList<Configuration> list = new TransformationList<Configuration>();
            list.Add(c=>c.AddXmlString(input.Element.OuterXml));
            return list;
        }
    }
}

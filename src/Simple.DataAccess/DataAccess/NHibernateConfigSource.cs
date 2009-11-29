using System;
using System.Collections.Generic;
using System.Text;
using Simple.Config;
using System.Xml;
using NHibernate.Cfg;
using Simple.Patterns;
using System.Runtime.Serialization;
using System.IO;

namespace Simple.DataAccess
{
    public class NHConfigurator : TransformationList<Configuration>
    {
        public NHConfigurator() : base() { }
        public NHConfigurator(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class NHibernateConfigSource : WrappedConfigSource<
        NHConfigurator, NHibernateConfig>
    {
        NHConfigurator configurators = new NHConfigurator();

        public override NHConfigurator TransformFromInput(NHibernateConfig input)
        {
            NHConfigurator list = new NHConfigurator();
            list.Add(c=> {
                StringReader r = new StringReader(input.Element.OuterXml);
                return c.Configure(XmlReader.Create(r));
            });
            list.AddRange(configurators);

            return list;
        }

        public virtual void AddConfigurator(NHConfigurator config)
        {
            configurators.AddRange(config);
            InvokeReload();
        }

    }
}

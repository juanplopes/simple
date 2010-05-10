using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using NHibernate.Cfg;
using Simple.Config;
using Simple.Patterns;

namespace Simple.Data
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

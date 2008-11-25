using BasicLibrary.Configuration;
using NHibernate.Cfg;
using Sample.BusinessInterface.Domain;
using NHibernate.Mapping;
using SimpleLibrary.DataAccess;
using NHibernate;
using Sample.BusinessInterface;
using SimpleLibrary.Rules;
using System.Threading;

namespace Sample.UserInterface2
{
    [DefaultFile("Test.config")]
    class Test : ConfigRoot<Test>
    {
        public class Test2 : ConfigElement, IStringConvertible
        {
            [ConfigElement("oi", Required = true)]
            public string Oi { get; set; }

            public void LoadFromString(string value)
            {
                Oi = value;
                (this as IConfigElement).NotifyLoad("oi");
            }
        }

        [ConfigElement("test2", Required = true)]
        public TypeConfigElement Test2Prop { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            rules.Persist(new Empresa()
            {
                Nome = "oi"
            });
        }
    }
}


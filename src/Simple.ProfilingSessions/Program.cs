using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;
using System.Threading;
using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.LinFu;
using FluentNHibernate.Mapping;
using NHibernate.Linq;
using Simple.Entities;

namespace Simple.ProfilingSessions
{
    public class Usuario : Entity<Usuario>
    {
        public virtual int Id { get; set; }
        public virtual string NmLogin { get; set; }
    }

    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("tsdrusu0");
            Id(x => x.Id).Column("id_usu");
            Map(x => x.NmLogin).Column("nm_login");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var simply = Simply.Do;
            simply.Configure.NHibernateQuickly(
                @".\sqlexpress", "sdrh", typeof(Program).Assembly);

            simply.Host(typeof(EntityService<Usuario>));

            using (var context = Simply.Do.EnterContext())
            {
                var list = Usuario.ListAll();

                Console.WriteLine(list.Count);
            }

        }
    }
}

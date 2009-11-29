using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Simple.Services;
using Simple.Config;
using Simple.Entities;

namespace Simple.Tests.SampleServer
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Supplier : Entity<Supplier, ISupplierService>
    {
        public virtual int Id { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string ContactTitle { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string HomePage { get; set; }

        public class Map : ClassMap<Supplier>
        {
            public Map()
            {
                Table("Suppliers");
                Not.LazyLoad();

                Id(x => x.Id, "SupplierID");
                Map(x => x.CompanyName);
                Map(x => x.ContactName);
                Map(x => x.ContactTitle);
                Map(x => x.Address);
                Map(x => x.City);
                Map(x => x.Region);
                Map(x => x.PostalCode);
                Map(x => x.Country);
                Map(x => x.Phone);
                Map(x => x.Fax);
                Map(x => x.HomePage);
            }
        }
    }


    public interface ISupplierService : IEntityService<Supplier> { }
    public class SupplierService : EntityService<Supplier>, ISupplierService { }

}

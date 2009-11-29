using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using FluentNHibernate.Mapping;
using Simple.Services;
using Simple.Entities;

namespace Simple.Tests.SampleServer
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Product : Entity<Product, IProductService>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Category Category { get; set; }
        public virtual string QuantityPerUnit { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual int UnitsInStock { get; set; }
        public virtual int UnitsOnOrder { get; set; }
        public virtual int ReorderLevel { get; set; }
        public virtual bool Discontinued { get; set; }

        public class Map : ClassMap<Product>
        {
            public Map()
            {
                Table("Products");
                Not.LazyLoad();

                Id(x => x.Id, "ProductID").GeneratedBy.Increment();
                Map(x => x.Name, "ProductName");
                References(x => x.Supplier, "SupplierID").Not.LazyLoad();
                References(x => x.Category, "CategoryID").Not.LazyLoad() ;
                Map(x => x.QuantityPerUnit);
                Map(x => x.UnitPrice);
                Map(x => x.UnitsInStock);
                Map(x => x.UnitsOnOrder);
                Map(x => x.ReorderLevel);
                Map(x => x.Discontinued);
            }
        }

    }

    public interface IProductService : IEntityService<Product> { }
    public class ProductService : EntityService<Product>, IProductService { }
}

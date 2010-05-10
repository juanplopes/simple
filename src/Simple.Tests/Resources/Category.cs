using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using Simple.Config;
using Simple.Entities;

namespace Simple.Tests.Resources
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Category : Entity<Category, ICategoryService>
    {
       
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual byte[] Picture { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public class Map : ClassMap<Category>
        {
            public Map()
            {
                Table("Categories");
                Id(x => x.Id, "CategoryID").GeneratedBy.Identity();
                Map(x => x.Name, "CategoryName");
                Map(x => x.Description);
                Map(x => x.Picture);
                HasMany(x => x.Products).KeyColumn("ProductID");
            }
        }
    }

    public interface ICategoryService : IEntityService<Category> { }
    public class CategoryService : EntityService<Category>, ICategoryService { }
}

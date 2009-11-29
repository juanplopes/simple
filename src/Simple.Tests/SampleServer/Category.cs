using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Simple.Services;
using Simple.Config;
using NHibernate;
using NHibernate.SqlTypes;
using Simple.Entities;

namespace Simple.Tests.SampleServer
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Category : Entity<Category, ICategoryService>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual byte[] Picture { get; set; }

        public class Map : ClassMap<Category>
        {
            public Map()
            {
                Table("Categories");
                Id(x => x.Id, "CategoryID").GeneratedBy.Identity();
                Map(x => x.Name, "CategoryName");
                Map(x => x.Description);
                Map(x => x.Picture);
            }
        }
    }

    public interface ICategoryService : IEntityService<Category> { }
    public class CategoryService : EntityService<Category>, ICategoryService { }
}

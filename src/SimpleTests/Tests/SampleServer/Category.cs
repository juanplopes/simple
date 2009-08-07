using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Simple.Services;
using Simple.ConfigSource;
using NHibernate;
using NHibernate.SqlTypes;

namespace Simple.Tests.SampleServer
{
    [DefaultConfig(NHConfig1.ConfigKey)]
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
                WithTable("Categories");
                Id(x => x.Id, "[Category ID]").GeneratedBy.Identity();
                Map(x => x.Name, "[Category Name]");
                Map(x => x.Description, "Description");
                Map(x => x.Picture, "Picture");
            }
        }
    }

    public interface ICategoryService : IEntityService<Category> { }
    public class CategoryService : EntityService<Category>, ICategoryService { }
}

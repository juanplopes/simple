using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Entities;
using FluentNHibernate.Mapping;

namespace Simple.Tests.SampleServer
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Territory : Entity<Territory, ITerritoryService>
    {
        public virtual string Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Region Region { get; set; }

        public class Map : ClassMap<Territory>
        {
            public Map()
            {
                WithTable("Territories");
                Not.LazyLoad();

                Id(x => x.Id, "TerritoryID");
                Map(x => x.Description, "TerritoryDescription");
                References(x => x.Region, "RegionID");
            }
        }
    }


    public interface ITerritoryService : IEntityService<Territory> { }
    public class TerritoryService : EntityService<Territory>, ITerritoryService { }
}

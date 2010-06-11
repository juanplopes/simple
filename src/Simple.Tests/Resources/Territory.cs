using System;
using FluentNHibernate.Mapping;
using Simple.Config;
using Simple.Entities;

namespace Simple.Tests.Resources
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Territory : Entity<Territory, ITerritoryService>
    {
        static Territory()
        {
            Identifiers
                .Add(x => x.Id);
        }

        public virtual string Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Region Region { get; set; }

        public class Map : ClassMap<Territory>
        {
            public Map()
            {
                Table("Territories");
                

                Id(x => x.Id, "TerritoryID");
                Map(x => x.Description, "TerritoryDescription");
                References(x => x.Region, "RegionID");
            }
        }
    }


    public interface ITerritoryService : IEntityService<Territory> { }
    public class TerritoryService : EntityService<Territory>, ITerritoryService { }
}

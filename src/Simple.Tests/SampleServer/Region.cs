using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using Simple.Entities;
using FluentNHibernate.Mapping;

namespace Simple.Tests.SampleServer
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Region : Entity<Region, IRegionService>
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }


        public class Map : ClassMap<Region>
        {
            public Map()
            {
                Table("Region");
                Not.LazyLoad();

                Id(x => x.Id, "RegionID");
                Map(x => x.Description, "RegionDescription");
            }
        }
    }


    public interface IRegionService : IEntityService<Region> { }
    public class RegionService : EntityService<Region>, IRegionService { }
}

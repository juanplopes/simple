using System;
using FluentNHibernate.Mapping;
using FluentValidation;
using Simple.Config;
using Simple.Entities;

namespace Simple.Tests.Resources
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
                

                Id(x => x.Id, "RegionID");
                Map(x => x.Description, "RegionDescription");
            }
        }

        public class Validator : AbstractValidator<Region>
        {
            public Validator()
            {
                RuleFor(x=>x.Description).Must((m,x)=>
                    m.Description != null && x.Length < 100);                
            }
        }
    }


    public interface IRegionService : IEntityService<Region> { }
    public class RegionService : EntityService<Region>, IRegionService { }
}

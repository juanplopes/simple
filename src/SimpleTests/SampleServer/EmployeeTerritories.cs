using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Entities;
using FluentNHibernate.Mapping;
using Simple.Reflection;

namespace Simple.Tests.SampleServer
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class EmployeeTerritory : Entity<EmployeeTerritory, IEmployeeTerritoryService>
    {
        static EntityHelper<EmployeeTerritory> _helper =
            new EntityHelper<EmployeeTerritory>(x => x.Territory, x => x.Employee);
        
        public virtual Employee Employee { get; set; }
        public virtual Territory Territory { get; set; }

        public override bool Equals(object obj)
        {
            return _helper.ObjectEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return _helper.ObjectGetHashCode(this);
        }

        public class Map : ClassMap<EmployeeTerritory>
        {
            public Map()
            {
                WithTable("EmployeeTerritories");
                Not.LazyLoad();

                UseCompositeId()
                    .WithKeyReference(x => x.Employee, "EmployeeID")
                    .WithKeyReference(x => x.Territory, "TerritoryID");

                References(x => x.Employee, "EmployeeID");
                References(x => x.Territory, "TerritoryID");
            }
        }
    }


    public interface IEmployeeTerritoryService : IEntityService<EmployeeTerritory> { }
    public class EmployeeTerritoryService : EntityService<EmployeeTerritory>, IEmployeeTerritoryService { }
}

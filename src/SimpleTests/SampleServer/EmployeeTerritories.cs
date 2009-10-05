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
        static EmployeeTerritory()
        {
            Identifiers
                .AddID(x => x.Employee)
                .AddID(x => x.Territory);
        }


        public virtual Employee Employee { get; set; }
        public virtual Territory Territory { get; set; }

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

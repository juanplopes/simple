using System;
using FluentNHibernate.Mapping;
using Simple.Config;
using Simple.Entities;

namespace Simple.Tests.Resources
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class EmployeeTerritory : Entity<EmployeeTerritory, IEmployeeTerritoryService>
    {
        static EmployeeTerritory()
        {
            Identifiers
                .Add(x => x.Employee)
                .Add(x => x.Territory);
        }


        public virtual Employee Employee { get; set; }
        public virtual Territory Territory { get; set; }

        public class Map : ClassMap<EmployeeTerritory>
        {
            public Map()
            {
                Table("EmployeeTerritories");
                

                CompositeId()
                    .KeyReference(x => x.Employee, "EmployeeID")
                    .KeyReference(x => x.Territory, "TerritoryID");

                References(x => x.Employee, "EmployeeID");
                References(x => x.Territory, "TerritoryID");
            }
        }
    }


    public interface IEmployeeTerritoryService : IEntityService<EmployeeTerritory> { }
    public class EmployeeTerritoryService : EntityService<EmployeeTerritory>, IEmployeeTerritoryService { }
}

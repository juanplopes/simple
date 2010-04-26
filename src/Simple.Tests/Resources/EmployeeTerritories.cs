using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using Simple.Entities;
using FluentNHibernate.Mapping;
using Simple.Reflection;

namespace Simple.Tests.Resources
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

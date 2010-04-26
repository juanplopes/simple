using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using Simple.Entities;
using FluentNHibernate.Mapping;

namespace Simple.Tests.Resources
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public class Employee : Entity<Employee, IEmployeeService>
    {
        static Employee()
        {
            Identifiers.AddID(x => x.Id);
        }


        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }


        public class Map : ClassMap<Employee>
        {
            public Map()
            {
                Table("Employees");
                

                Id(x => x.Id, "EmployeeID");
                Map(x => x.FirstName);
                Map(x => x.LastName);
            }
        }
    }


    public interface IEmployeeService : IEntityService<Employee> { }
    public class EmployeeService : EntityService<Employee>, IEmployeeService { }
}

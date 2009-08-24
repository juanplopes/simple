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
    public class Employee : Entity<Employee, IEmployeeService>
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }


        public class Map : ClassMap<Employee>
        {
            public Map()
            {
                WithTable("Employees");
                Not.LazyLoad();

                Id(x => x.Id, "EmployeeID");
                Map(x => x.FirstName);
                Map(x => x.LastName);
            }
        }
    }


    public interface IEmployeeService : IEntityService<Employee> { }
    public class EmployeeService : EntityService<Employee>, IEmployeeService { }
}

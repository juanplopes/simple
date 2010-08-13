using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Simple.Entities;

namespace Simple.Tests.Mvc.ModelBinder
{
    public class Employee
    {
        public ContactInfo Contact { get; set; }
    }

    public class ContactInfo
    {
        public Address HomeAddress { get; set; }
    }

    public class Address : IEntity
    {
        public Address() { }
        public Address(int id) { ID = id; }
        public int ID { get; set; }
    }

    public class Company<T>
        where T : IEnumerable<Address>
    {
        public T Places { get; set; }
    }

    public class CompanyArray : Company<Address[]>
    {
    }

    public class CompanyIList : Company<IList<Address>>
    {
    }

    public class CompanyISet : Company<ISet<Address>>
    {
        public CompanyISet() { Places = new HashedSet<Address>(); }
    }


}

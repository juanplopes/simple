using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Simple.Services;
using Simple.Config;
using NHibernate;
using NHibernate.SqlTypes;
using Simple.Entities;
using Simple.Data;
using FluentValidation;

namespace Simple.Tests.Resources
{
    [DefaultConfig(NHConfig1.ConfigKey), Serializable]
    public partial class Customer : Entity<Customer, ICustomerService>
    {
        //protected override object InternalIdentifier
        //{
        //    get
        //    {
        //        return Id;
        //    }
        //}


        public virtual string Id { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string ContactTitle { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }


        public class Map : ClassMap<Customer>
        {
            public Map()
            {
                Table("Customers");
                

                Id(x => x.Id, "CustomerID");
                Map(x => x.CompanyName);
                Map(x => x.ContactName);
                Map(x => x.ContactTitle);
                Map(x => x.Address);
                Map(x => x.City);
                Map(x => x.Region);
                Map(x => x.PostalCode);
                Map(x => x.Country);
                Map(x => x.Phone);
                Map(x => x.Fax);
            }
        }

        public class Validator : AbstractValidator<Customer>
        {
            public Validator()
            {
                RuleFor(x => x.CompanyName).Length(0, 40);
                RuleFor(x => x.ContactName).Length(0, 30);
            }
        }
    }

    public interface ICustomerService : IEntityService<Customer>
    {
        [RequiresTransaction]
        void DeleteTwoCustomers();
    }
    public class CustomerService : EntityService<Customer>, ICustomerService
    {
        [RequiresTransaction]
        public void DeleteTwoCustomers()
        {
            var two = Query().Take(2).ToList();

            foreach (var cust in two)
            {
                cust.Delete();
            }

            throw new Exception("sample exception");
        }


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Simple.Tests.Validation.ValidationResources
{

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Picture { get; set; }
        public Company Company { get; set; }        
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? CompanyValue { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }        
    }
}


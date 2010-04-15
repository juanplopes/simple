using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Simple.Entities;
using NHibernate.Tool.hbm2ddl;
using Simple.Expressions;

namespace Sample.Project.Tests.Helpers
{
    public class DatabaseFixture : TransactionedFixture
    {
        [Test]
        public void SchemaIsCorrectlyMapped()
        {
            var validator = new SchemaValidator(Simply.Do.GetNHibernateConfig());
            validator.Validate();
        }
    }

}

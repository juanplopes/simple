using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Simple.Entities;
using NHibernate.Tool.hbm2ddl;
using Sample.Project.Migrations.Extensions;

namespace Sample.Project.Tests.Configuration
{
    public class DatabaseFixture : TransactionedFixture
    {
        [Test]
        public void SchemaIsCorrectlyMapped()
        {
            var validator = new SchemaValidator(Simply.Do.GetNHibernateConfig());
            validator.Validate();
        }

        [Test]
        public void AllMigrationsAreConsistent()
        {
            ConfigurationEnsurer.Do.ReMigrate();
            SchemaIsCorrectlyMapped();
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate.Linq;
using Simple.Tests.Resources;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class NHLinqFixture : BaseDataFixture
    {
        [Test]
        public void GroupTerritoriesByEmployee()
        {
            var mapping = Session.Query<EmployeeTerritory>();

            var q = mapping.GroupBy(x => x.Employee.Id)
                .Select(x => new { x.Key, Count = x.Count() });

            var list = q.ToDictionary(x => x.Key);

            Assert.AreEqual(9, list.Count);

            Assert.AreEqual(2, list[1].Count);
            Assert.AreEqual(7, list[2].Count);
            Assert.AreEqual(4, list[3].Count);
            Assert.AreEqual(3, list[4].Count);
            Assert.AreEqual(7, list[5].Count);
            Assert.AreEqual(5, list[6].Count);
            Assert.AreEqual(10, list[7].Count);
            Assert.AreEqual(4, list[8].Count);
            Assert.AreEqual(7, list[9].Count);
        }
    }
}

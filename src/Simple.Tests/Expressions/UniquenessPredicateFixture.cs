using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Entities;

namespace Simple.Tests.Entities
{
    public class UniquenessPredicateFixture
    {
        class SampleSingleStringIdentifier : Entity<SampleSingleStringIdentifier>
        {
            static SampleSingleStringIdentifier() { Identifiers.Add(x => x.Id); }
            public string Id { get; set; }
            public string CompanyName { get; set; }
        }

        class SampleSingleNullableIntIdentifier : Entity<SampleSingleNullableIntIdentifier>
        {
            static SampleSingleNullableIntIdentifier() { Identifiers.Add(x => x.Id); }

            public int? Id { get; set; }
            public string Phone { get; set; }
        }

        class SampleReferenceSingleStringIdentifier : Entity<SampleReferenceSingleStringIdentifier>
        {
            static SampleReferenceSingleStringIdentifier() { Identifiers.Add(x => x.Id); }
            
            public int Id { get; set; }
            public SampleSingleStringIdentifier SampleStringIdentifier { get; set; }
        }

        class SampleNoIdentifierList : Entity<SampleNoIdentifierList>
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class SampleMultipleIdentifiers : Entity<SampleMultipleIdentifiers>
        {
            static SampleMultipleIdentifiers() { Identifiers.Add(x => x.Id).Add(x => x.Name); }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Age { get; set; }
        }

        [Test]
        public void CreateRightExpressionFromSingleIdentifier()
        {
            var sample = new SampleSingleStringIdentifier();
            sample.Id = "Alan";
            sample.CompanyName = "Living";

            var expr = sample.UniqueProperties("q", x => x.CompanyName);

            expr.ToString().Should().Be("q => ((q.Id != \"Alan\") && (q.CompanyName = \"Living\"))");
        }

        [Test]
        public void CreateRightExpressionFromMultipleIdentifiers()
        {
            var sample = new SampleMultipleIdentifiers();
            sample.Id = 123;
            sample.Name = "asd";
            sample.Age = "23,5";

            var expr = sample.UniqueProperties("q", x => x.Age);

            Assert.AreEqual("q => (((q.Id != 123) && (q.Name != \"asd\")) && (q.Age = \"23,5\"))", expr.ToString());
        }


        [Test]
        public void CreateRightExpressionFromSingleNullableIdentifierPopulatedWithValue()
        {
            var sample = new SampleSingleNullableIntIdentifier();
            sample.Id = 123;
            sample.Phone = "asd";

            var expr = sample.UniqueProperties("q", x => x.Phone);

            expr.ToString().Should().Be("q => ((q.Id != 123) && (q.Phone = \"asd\"))");
        }

        [Test]
        public void CreateRightExpressionFromSingleNullableIdentifierPopulatedWithNull()
        {
            var sample = new SampleSingleNullableIntIdentifier();
            sample.Id = null;
            sample.Phone = "asd";

            var expr = sample.UniqueProperties("q", x => x.Phone);

            expr.ToString().Should().Be("q => ((q.Id != null) && (q.Phone = \"asd\"))");
        }

        [Test]
        public void CreateExpressionFromNestedPropertyShouldThrowException()
        {
            var sample = new SampleReferenceSingleStringIdentifier();
            sample.Id = 123;
            sample.SampleStringIdentifier = new SampleSingleStringIdentifier() { CompanyName = "asd" };
            Assert.Throws<InvalidOperationException>(()=>
                 sample.UniqueProperties("q", x => x.SampleStringIdentifier.CompanyName));
        }

        [Test]
        public void CreateExpressionFromClassWithNoIdentifierListShouldThrowException()
        {
            var sample = new SampleNoIdentifierList();
            sample.Id = 123;
            Assert.Throws<InvalidOperationException>(() =>
                 sample.UniqueProperties("q", x => x.Name), "The class must have at least one registered identifier");
        }
    }


}

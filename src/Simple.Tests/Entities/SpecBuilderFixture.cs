using NUnit.Framework;
using SharpTestsEx;
using Simple.Expressions;
using Simple.IO.Serialization;
using Simple.Tests.Resources;
using System.Linq;
using Simple.Entities.QuerySpec;

namespace Simple.Tests.Entities
{
    public class SpecBuilderFixture
    {
        [Test]
        public void NewSpecBuilderShouldHaveZeroItems()
        {
            var spec = new SpecBuilder<int>();
            spec.Items.Should().Have.Count.EqualTo(0);

        }

        [Test]
        public void CanMergeTwoSingleItemSpecBuilders()
        {
            var spec1 = new SpecBuilder<int>().Skip(10);
            var spec2 = new SpecBuilder<int>().Take(20);

            var newSpec = spec1.Merge(spec2);

            newSpec.Items.Should().Have.SameSequenceAs(spec1.Items.First(), spec2.Items.First());
        }


        [Test]
        public void MergedSpecBuilderShouldBeDifferentInstance()
        {
            var spec1 = new SpecBuilder<int>().Skip(10);
            var spec2 = new SpecBuilder<int>().Take(20);

            var newSpec = spec1.Merge(spec2);

            newSpec.Should()
                .Not.Be.SameInstanceAs(spec1)
                .And.Not.Be.SameInstanceAs(spec2);
        }

    }
}

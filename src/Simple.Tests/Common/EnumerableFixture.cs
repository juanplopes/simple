using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using System.Text;
using System.Collections.Generic;

namespace Simple.Tests.Common
{
    public class EnumerableFixture
    {
        [Test]
        public void CanZipTwoSameLengthSequences()
        {
            var seq1 = new[] { 1, 2, 3 };
            var seq2 = new[] { "um", "dois", "três" };
            var seq3 = seq1.Zip(seq2, (x, y) => x + y).ToList();

            seq3.Should().Have.SameSequenceAs("1um", "2dois", "3três");
        }

        [Test]
        public void CanZipTwoDifferentLengthSequences()
        {
            var seq1 = new[] { 1, 2, 3, 4 };
            var seq2 = new[] { "um", "dois", "três" };
            var seq3 = seq1.Zip(seq2, (x, y) => x + y).ToList();

            seq3.Should().Have.SameSequenceAs("1um", "2dois", "3três");
        }

        [Test]
        public void CanZipThreeSequences()
        {
            var seq1 = new[] { 1, 2, 3, 4 };
            var seq2 = new[] { 1, 2 };
            var seq3 = new[] { "um", "dois", "três" };

            var seq4 = seq1.Zip(seq2, (x, y) => x * y).Zip(seq3, (x, y) => x + y).ToList();

            seq4.Should().Have.SameSequenceAs("1um", "4dois");
        }

        [Test]
        public void CanAggregateJoinArrayOfStrings()
        {
            var seq = new[] { "um", "dois", "três" };
            var str = seq.AggregateJoin((x, y) => x + "," + y);

            str.Should().Be("um,dois,três");
        }

        [Test]
        public void CanStringJoinArrayOfStrings()
        {
            var seq = new[] { "um", "dois", "três" };
            var str = seq.StringJoin();

            str.Should().Be("umdoistrês");
        }

        [Test]
        public void CanStringJoinArrayOfStringsWithCommaSeparator()
        {
            var seq = new[] { "um", "dois", "três" };
            var str = seq.StringJoin(",");

            str.Should().Be("um,dois,três");
        
        }

        [Test]
        public void CanEagerForeachWithEmptyCollection()
        {
            var seq = new string[0];

            var builder = new StringBuilder();
            seq.EagerForeach(x => builder.Append(x));

            builder.ToString().Should().Be("");
        }


        [Test]
        public void CanEagerForeachWithoutBetween()
        {
            var seq = new[] { "um", "dois", "três" };
            
            var builder = new StringBuilder();
            seq.EagerForeach(x => builder.Append(x));

            builder.ToString().Should().Be("umdoistrês");
        }

        [Test]
        public void CanEagerForeachWithBetweenInEmptyCollection()
        {
            var seq = new string[0] { };

            var builder = new StringBuilder();
            seq.EagerForeach(x => builder.Append(x), x => builder.Append(","));

            builder.ToString().Should().Be("");
        }

        [Test]
        public void CanEagerForeachWithBetween()
        {
            var seq = new[] { "um", "dois", "três" };

            var builder = new StringBuilder();
            seq.EagerForeach(x => builder.Append(x), x=>builder.Append(","));

            builder.ToString().Should().Be("um,dois,três");
        }

        [Test]
        public void CanStringJoinArrayOfIntsWithCommaSeparator()
        {
            var seq = new[] { 1, 2, 3 };
            var str = seq.StringJoin(",");

            str.Should().Be("1,2,3");
        }

        [Test]
        public void CanSafeGetValueTypeDictionaryValueWhenTheValueExists()
        {
            var seq = new Dictionary<int, int>() { { 2, 3 }, { 4, 5 } };

            seq.SafeGet(2).Should().Be(3);
        }

        [Test]
        public void CanSafeGetValueTypeDictionaryValueWhenTheValueNotExists()
        {
            var seq = new Dictionary<int, int>() { { 2, 3 }, { 4, 5 } };

            seq.SafeGet(3).Should().Be(0);
        }


        [Test]
        public void CanSafeGetReferenceTypeDictionaryValueWhenTheValueExists()
        {
            var seq = new Dictionary<string, string>() { { "2", "3" }, { "4", "5" } };

            seq.SafeGet("2").Should().Be("3");
        }

        [Test]
        public void CanSafeGetReferenceTypeDictionaryValueWhenTheValueNotExists()
        {
            var seq = new Dictionary<string, string>() { { "2", "3" }, { "4", "5" } };

            seq.SafeGet("3").Should().Be(null);
        }
    }
}

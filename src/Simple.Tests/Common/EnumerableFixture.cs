using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using System.Text;
using System.Collections.Generic;
using System;

namespace Simple.Tests.Common
{
    public class EnumerableFixture
    {
        [Test]
        public void CanBatchSelectZero()
        {
            int count = 0;
            var set = new int[0];

            var result = set.BatchSelect(3, x =>
            {
                count++;
                return x.Select(y => y * count);
            }).ToList();

            count.Should().Be(0);
        }


        [Test]
        public void CanBatchSelectExactItems()
        {
            int count = 0;
            var set = Enumerable.Range(1, 9);

            var result = set.BatchSelect(3, x =>
            {
                count++;
                return x.Select(y => y * count);
            }).ToList();

            count.Should().Be(3);
            result.Should().Have.SameSequenceAs(1, 2, 3, 8, 10, 12, 21, 24, 27);
        }

        [Test]
        public void CanBatchSelectNonExactItems()
        {
            int count = 0;
            var set = Enumerable.Range(1, 10);

            var result = set.BatchSelect(3, x =>
            {
                count++;
                return x.Select(y => y * count);
            }).ToList();

            count.Should().Be(4);
            result.Should().Have.SameSequenceAs(1, 2, 3, 8, 10, 12, 21, 24, 27, 40);
        }

        private IEnumerable<int> CountEnumerable(IEnumerable<int> baseEnum, Action count)
        {
            count();
            foreach (var item in baseEnum)
                yield return item;
        }

        [Test]
        public void BatchSelectUsesEnumerableOnlyOnce()
        {
            int count = 0;
            var enumerable = CountEnumerable(Enumerable.Range(1, 10), ()=>count++);

            count.Should().Be(0);
            enumerable.ToList();
            count.Should().Be(1);
            enumerable.ToList();
            count.Should().Be(2);
            

            var result = enumerable.BatchSelect(3, x =>
            {
                return x.Select(y => y + 1);
            }).ToList();

            count.Should().Be(3);
            result.Should().Have.SameSequenceAs(2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
        }

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
            seq.EagerForeach(x => builder.Append(x), x => builder.Append(","));

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

        [Test]
        public void CanDoUnionOnSingleItem()
        {
            var seq = new[] { 1, 2, 3 };
            seq.Union(4).Should().Have.SameSequenceAs(1, 2, 3, 4);
        }

        [Test]
        public void CanDoUnionOnTwoItems()
        {
            var seq = new[] { 1, 2, 3 };
            seq.Union(4, 6).Should().Have.SameSequenceAs(1, 2, 3, 4, 6);
        }

        [Test]
        public void CanDoUnionOnArrayOfItems()
        {
            var seq = new[] { 1, 2, 3 };
            var seq2 = new[] { 4, 5, 6 };
            seq.Union(seq2).Should().Have.SameSequenceAs(1, 2, 3, 4, 5, 6);
        }

        [Test]
        public void CanSafeMaxWithManyTypesOfCollections()
        {
            new int[] { 1, 2, 3, 2, 1 }.SafeAggregate(x => x.Max()).Should().Be(3);
            new int[] { }.SafeAggregate(x => x.Max()).Should().Be(0);
            new int[] { }.SafeAggregate(x => x.Max(), 42).Should().Be(42);
        }

        [Test]
        public void CanSafeMinWithManyTypesOfCollections()
        {
            new int[] { 1, 2, 3, 2, 1 }.SafeAggregate(x => x.Min()).Should().Be(1);
            new int[] { }.SafeAggregate(x => x.Min()).Should().Be(0);
            new int[] { }.SafeAggregate(x => x.Min(), 42).Should().Be(42);
        }

        [Test]
        public void CanSafeSumWithManyTypesOfCollections()
        {
            new int[] { 1, 2, 3, 2, 1 }.SafeAggregate(x => x.Sum()).Should().Be(9);
            new int[] { }.SafeAggregate(x => x.Sum()).Should().Be(0);
            new int[] { }.SafeAggregate(x => x.Sum(), 42).Should().Be(42);
        }
    }
}

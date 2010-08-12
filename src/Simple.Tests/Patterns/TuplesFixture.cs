using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    [TestFixture]
    public class TuplesFixture
    {
        [Test]
        public void CanCompareTwoFiveSizedTuples()
        {
            var tuple1 = new Tuple<int>(1, 2, 3, 4, 5);
            var tuple2 = new Tuple<int>(1, 2, 3, 4, 5);
            var tuple3 = new Tuple<int>(1, 2, 3, 4, 6);

            tuple2.Should().Be(tuple1);
            tuple3.Should().Not.Be(tuple1);
        }

        [Test]
        public void CanCompareTwoFiveSizedStringTuples()
        {
            var tuple1 = new Tuple<string>("1", "2", "3", "4", "5");
            var tuple2 = new Tuple<string>("1", "2", "3", "4", "5");
            var tuple3 = new Tuple<string>("1", "2", "3", "4", "6");

            tuple2.Should().Be(tuple1);
            tuple3.Should().Not.Be(tuple1);
        }

        [Test]
        public void CanHashCodeTwoFiveSizedTuples()
        {
            var tuple1 = new Tuple<int>(1, 2, 3, 4, 5);
            var tuple2 = new Tuple<int>(1, 2, 3, 4, 5);
            var tuple3 = new Tuple<int>(1, 2, 3, 4, 6);

            tuple2.GetHashCode().Should().Be(tuple1.GetHashCode());
            tuple3.GetHashCode().Should().Not.Be(tuple1.GetHashCode());
        }

        [Test]
        public void CannotCompareTwoDifferentSizesTuples()
        {
            var tuple1 = new Tuple<int>(1, 2, 3, 4, 5);
            var tuple2 = new Tuple<int>(1, 2, 3, 4, 5, 6);

            tuple2.Should().Not.Be(tuple1);
        }
        [Test]
        public void CannotHashCodeTwoDifferentSizesTuples()
        {
            var tuple1 = new Tuple<int>(1, 2, 3, 4, 5);
            var tuple2 = new Tuple<int>(1, 2, 3, 4, 5, 6);

            tuple2.GetHashCode().Should().Not.Be(tuple1.GetHashCode());
        }


        [Test]
        public void CanCompareTwoFiveSizedNullableTuples()
        {
            var tuple1 = new Tuple<int?>(1, 2, null, 4, 5);
            var tuple2 = new Tuple<int?>(1, 2, null, 4, 5);
            var tuple3 = new Tuple<int?>(1, 2, 3, 4, null);

            tuple2.Should().Be(tuple1);
            tuple3.Should().Not.Be(tuple1);
        }

        [Test]
        public void CanHashCodeTwoFiveSizedNullableTuples()
        {
            var tuple1 = new Tuple<int?>(1, 2, null, 4, 5);
            var tuple2 = new Tuple<int?>(1, 2, null, 4, 5);
            var tuple3 = new Tuple<int?>(1, 2, 3, 4, null);

            tuple2.GetHashCode().Should().Be(tuple1.GetHashCode());
            tuple3.GetHashCode().Should().Not.Be(tuple1.GetHashCode());
        }

        [Test]
        public void CanCompareTwoNullablePairs()
        {
            var pair1 = new Pair<int?>(1, null);
            var pair2 = new Pair<int?>(1, null);
            var pair3 = new Pair<int?>(null, 2);

            pair2.Should().Be(pair1);
            pair3.Should().Not.Be(pair1);
        }

        [Test]
        public void CanCompareTwoNullableDifferentTypePairs()
        {
            var pair1 = new Pair<string, int?>("1", 2);
            var pair2 = new Pair<string, int?>("1", 2);
            var pair3 = new Pair<string, int?>("1", null);

            pair2.Should().Be(pair1);
            pair3.Should().Not.Be(pair1);
        }

        [Test]
        public void CanHashCodeTwoNullablePairs()
        {
            var pair1 = new Pair<int?>(1, null);
            var pair2 = new Pair<int?>(1, null);
            var pair3 = new Pair<int?>(null, 2);

            pair2.GetHashCode().Should().Be(pair1.GetHashCode());
            pair3.GetHashCode().Should().Not.Be(pair1.GetHashCode());
        }

        [Test]
        public void CanHashCodeTwoNullableDifferentTypePairs()
        {
            var pair1 = new Pair<string, int?>("1", 2);
            var pair2 = new Pair<string, int?>("1", 2);
            var pair3 = new Pair<string, int?>("1", null);

            pair2.GetHashCode().Should().Be(pair1.GetHashCode());
            pair3.GetHashCode().Should().Not.Be(pair1.GetHashCode());
        }

        [Test]
        public void CanCompareTwoNullableTriplets()
        {
            var pair1 = new Triplet<int?>(1, null, 42);
            var pair2 = new Triplet<int?>(1, null, 42);
            var pair3 = new Triplet<int?>(null, 2, null);

            pair2.Should().Be(pair1);
            pair3.Should().Not.Be(pair1);
        }

        [Test]
        public void CanCompareTwoNullableDifferentTypeTriplets()
        {
            var triplet1 = new Triplet<string, int?, Type>("1", 2, typeof(Console));
            var triplet2 = new Triplet<string, int?, Type>("1", 2, typeof(Console));
            var triplet3 = new Triplet<string, int?, Type>("1", null, typeof(Console));

            triplet2.Should().Be(triplet1);
            triplet3.Should().Not.Be(triplet1);
        }

        [Test]
        public void CanHashCodeTwoNullableTriplets()
        {
            var pair1 = new Triplet<int?>(1, null, 42);
            var pair2 = new Triplet<int?>(1, null, 42);
            var pair3 = new Triplet<int?>(null, 2, null);

            pair2.GetHashCode().Should().Be(pair1.GetHashCode());
            pair3.GetHashCode().Should().Not.Be(pair1.GetHashCode());
        }

        [Test]
        public void CanHashCodeTwoNullableDifferentTypeTriplets()
        {
            var triplet1 = new Triplet<string, int?, Type>("1", 2, typeof(Console));
            var triplet2 = new Triplet<string, int?, Type>("1", 2, typeof(Console));
            var triplet3 = new Triplet<string, int?, Type>("1", null, typeof(Console));

            triplet2.GetHashCode().Should().Be(triplet1.GetHashCode());
            triplet3.GetHashCode().Should().Not.Be(triplet1.GetHashCode());
        }


    }
}

using System.Globalization;
using NUnit.Framework;
using SharpTestsEx;
using Simple.IO;

namespace Simple.Tests.IO
{
    [TestFixture]
    public class FileSizeFixture
    {
        [Test]
        public void CanConvertBetweenUnitsWhenSizeIs1060111()
        {
            var originalSize = 1060111;
            var sizeInBytes = new FileSize(originalSize);
            var sizeInKBytes = sizeInBytes.In(FileSizeUnit.KB);
            var sizeInMBytes = sizeInBytes.In(FileSizeUnit.MB);

            sizeInBytes.Equals(originalSize).Should().Be.True();
            sizeInKBytes.Equals(originalSize).Should().Be.True();
            sizeInMBytes.Equals(originalSize).Should().Be.True();

            sizeInKBytes.Equals(sizeInBytes).Should().Be.True();
            sizeInMBytes.Equals(sizeInBytes).Should().Be.True();

            sizeInMBytes.Equals(sizeInKBytes).Should().Be.True();

            sizeInMBytes.ToString(CultureInfo.InvariantCulture).Should().Be("1.01 MB");
            sizeInMBytes.ToString("0.###", CultureInfo.InvariantCulture).Should().Be("1.011 MB");
        }

        [Test]
        public void CanHashCodeWhenSizeIs1034AndCultureIsPT_BR()
        {
            var originalSize = 1034;
            var sizeInBytes = new FileSize(originalSize);
            var sizeInKBytes = sizeInBytes.In(FileSizeUnit.KB);

            sizeInBytes.Equals(originalSize).Should().Be.True();
            sizeInKBytes.Equals(originalSize).Should().Be.True();

            sizeInKBytes.Equals(sizeInBytes).Should().Be.True();


            sizeInKBytes.ToString(CultureInfo.GetCultureInfo("pt-BR")).Should().Be("1,01 KB");
        }

        [Test]
        public void CanFindBestUnitsForSomeSizes()
        {
            new FileSize(1010).InBestUnit().Unit.Should().Be(FileSizeUnit.KB);
            new FileSize(1000).InBestUnit().Unit.Should().Be(FileSizeUnit.KB);
            new FileSize(999).InBestUnit().Unit.Should().Be(FileSizeUnit.B);
            new FileSize(1024000).InBestUnit().Unit.Should().Be(FileSizeUnit.MB);

            new FileSize(1024000).InBestUnit().ToString(CultureInfo.InvariantCulture).Should().Be("0.98 MB");
        }
    }
}

using System.Globalization;
using NUnit.Framework;
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

            Assert.AreEqual(originalSize, sizeInBytes);
            Assert.AreEqual(originalSize, sizeInKBytes);
            Assert.AreEqual(originalSize, sizeInMBytes);

            Assert.AreEqual(sizeInBytes, sizeInKBytes);
            Assert.AreEqual(sizeInBytes, sizeInMBytes);

            Assert.AreEqual(sizeInKBytes, sizeInMBytes);

            Assert.AreEqual("1.01 MB", sizeInMBytes.ToString(CultureInfo.InvariantCulture));
            Assert.AreEqual("1.011 MB", sizeInMBytes.ToString("0.###", CultureInfo.InvariantCulture));
        }

        [Test]
        public void CanHashCodeWhenSizeIs1034AndCultureIsPT_BR()
        {
            var originalSize = 1034;
            var sizeInBytes = new FileSize(originalSize);
            var sizeInKBytes = sizeInBytes.In(FileSizeUnit.KB);

            Assert.AreEqual(originalSize, sizeInBytes);
            Assert.AreEqual(originalSize, sizeInKBytes);

            Assert.AreEqual(sizeInBytes, sizeInKBytes);


            Assert.AreEqual("1,01 KB", sizeInKBytes.ToString(CultureInfo.GetCultureInfo("pt-BR")));
        }

        [Test]
        public void CanFindBestUnitsForSomeSizes()
        {
            Assert.AreEqual(FileSizeUnit.KB, new FileSize(1010).InBestUnit().Unit);
            Assert.AreEqual(FileSizeUnit.KB, new FileSize(1000).InBestUnit().Unit);
            Assert.AreEqual(FileSizeUnit.B, new FileSize(999).InBestUnit().Unit);
            Assert.AreEqual(FileSizeUnit.MB, new FileSize(1024000).InBestUnit().Unit);

            Assert.AreEqual("0.98 MB", new FileSize(1024000).InBestUnit().ToString(CultureInfo.InvariantCulture));
        }
    }
}

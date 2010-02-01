using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Simple.IO
{
    public enum FileSizeUnit : long
    {
        B=1,
        KB=B * 1024,
        MB=KB * 1024,
        GB=MB * 1024,
        TB=GB * 1024,
        EB=TB * 1024,
        ZB=EB * 1024,
    }

    public struct FileSize
    {
        private const string DefaultFormat = "0.##";
        private const long StopSearchingFactor = 1000;

        private decimal _size;
        private FileSizeUnit _unit;

        public decimal Size { get { return _size; } }
        public FileSizeUnit Unit { get { return _unit; } }
        public long SizeInBytes { get { return (long)(_size * (long)_unit); } }

        public FileSize(decimal size, FileSizeUnit unit)
        {
            _size = size;
            _unit = unit;
        }
        
        public FileSize(long size) : this(size, FileSizeUnit.B)
        {  }

        public FileSize In(FileSizeUnit newUnit)
        {
            return new FileSize(_size * UnitDifference(_unit, newUnit), newUnit);
        }

        
        public FileSize InBestUnit()
        {
            FileSize temp = 0;
            foreach (FileSizeUnit value in Enum.GetValues(typeof(FileSizeUnit)))
            {
                temp = this.In(value);
                if (temp.Size < StopSearchingFactor) return temp;
            }
            return temp;
        }

        private decimal UnitDifference(FileSizeUnit x, FileSizeUnit y)
        {
            return (decimal)x / (decimal)y;
        }

        public override string ToString()
        {
            return ToString(DefaultFormat);
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString(DefaultFormat, provider);
        }
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }
        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format("{0} {1}", _size.ToString(format, provider), _unit);
        }

        public override int GetHashCode()
        {
            return SizeInBytes.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is FileSize)) return false;
            return Equals((FileSize)obj);
        }

        public bool Equals(FileSize size)
        {
            return size == this;
        }

        public static bool operator >(FileSize x, FileSize y)
        {
            return x.SizeInBytes > y.SizeInBytes;
        }

        public static bool operator <(FileSize x, FileSize y)
        {
            return x.SizeInBytes < y.SizeInBytes;
        }

        public static bool operator ==(FileSize x, FileSize y)
        {
            return x.SizeInBytes == y.SizeInBytes;
        }

        public static bool operator !=(FileSize x, FileSize y)
        {
            return !(x == y);
        }

        public static implicit operator long(FileSize x)
        {
            return x.SizeInBytes;
        }
        public static implicit operator FileSize(long x)
        {
            return new FileSize(x);
        }
    }

}

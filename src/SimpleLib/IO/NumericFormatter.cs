using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.IO
{
    public class NumericFormatter : DefaultFormatter
    {
        public int DecimalCorrection { get; private set; }

        public NumericFormatter(string format, int decimalCorrection)
            : base(format)
        {
            this.DecimalCorrection = (int)Math.Pow(10, decimalCorrection);
        }

        public override object Parse(string value, Type type, IFormatProvider provider)
        {
            object obj = base.Parse(value, type, provider);

            decimal obj2 = Convert.ToDecimal(obj, provider);
            obj2 /= DecimalCorrection;

            return Convert.ChangeType(obj2, obj.GetType());
        }

        public override string Format(object value, IFormatProvider provider)
        {
            decimal value2 = Convert.ToDecimal(value, provider);


            return base.Format(value2 * DecimalCorrection, provider);
        }
    }
}

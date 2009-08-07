using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Tests.DataAccess
{
    public class BaseDataFixture
    {
        public Simply MySimply
        {
            get
            {
                return NHConfig1.Do.Simply;
            }
        }
    }
}

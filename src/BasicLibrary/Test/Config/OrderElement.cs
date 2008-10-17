using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    public class OrderElement : ConfigElement
    {
        [ConfigElement("maxExtraNumberOrders")]
        public int MaxExtraNumberOrders { get; set; }
    }
}

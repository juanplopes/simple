using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    [DefaultFile("BackOffice.config")]
    public class BackOfficeConfig : ConfigRoot<BackOfficeConfig>
    {
        [ConfigElement("fulfillment")]
        public FulfillmentElement Fulfillment { get; set; }

        [ConfigElement("payment")]
        public PaymentElement Payment { get; set; }

        [ConfigElement("order")]
        public OrderElement Order { get; set; }

        [ConfigElement("creditChecking")]
        public CreditCheckingElement CreditChecking { get; set; }

        [ConfigElement("oi")]
        public int? OI { get; set; }
    }
}

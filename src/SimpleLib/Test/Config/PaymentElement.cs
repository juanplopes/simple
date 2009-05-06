using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    public class PaymentElement : ConfigElement
    {
        [ConfigElement("merchantCode")]
        public string MerchantCode { get; set; }
        [ConfigElement("maxCreditCardUses")]
        public int MaxCreditCardUses { get; set; }
    }
}

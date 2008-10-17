using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    public class FulfillmentElement: ConfigElement
    {
        [ConfigElement("BNSAuthCode")]
        public string BNSAuthCode { get; set; }
        [ConfigElement("BNSClientID")]
        public int BNSClientID { get; set; }
    }
}

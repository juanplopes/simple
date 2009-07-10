using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    public class CreditCheckingConditionElement : ConfigElement
    {
        public enum TypeEnum
        {
            NewCustomer,
            AddOnOrders,
            NewOriginalCLI
        }

        [ConfigElement("type")]
        public TypeEnum Type { get; set; }

        [ConfigElement("minimumHits")]
        public int MinimumHits { get; set; }
        [ConfigElement("minimumAccountAge")]
        public int MinimumAccountAge { get; set; }
        [ConfigElement("minimumBalanceThreshold")]
        public decimal MinimumBalanceThreshold { get; set; }

        [ConfigAcceptsParent("properties")]
        [ConfigElement("group")]
        public List<CreditCheckPropertyGroupElement> PropertyGroups { get; set; }

    }
}

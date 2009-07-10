using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    public class CreditCheckingElement : ConfigElement
    {
        [ConfigDictionaryKeyName("type")]
        [ConfigElement("condition")]
        public Dictionary<CreditCheckingConditionElement.TypeEnum, CreditCheckingConditionElement> Conditions { get; set; }
    }
}

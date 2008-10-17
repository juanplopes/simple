using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using SimpleLibrary.ServiceModel;

namespace SimpleLibrary.Rules
{
    public static class RulesFactory
    {
        public static T Create<T>() where T : class
        {
            IRulesProvider<T> provider = new ServiceRulesProvider<T>();
            return provider.Create();
        }
    }
}

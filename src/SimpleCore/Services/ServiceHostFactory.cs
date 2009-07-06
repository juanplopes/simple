using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Reflection;
using Simple.Client;
using log4net;
using System.Reflection;

namespace Simple.Services
{
    public class ServiceHostFactory : Factory<IServiceHostProvider>, Simple.Services.IServiceHostFactory
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());

        protected override void OnConfig(IServiceHostProvider config)
        {
        }

        protected override void OnClearConfig()
        {
            ConfigCache = new NullServiceHostProvider();
        }

        public void Host(Type type)
        {
            ConfigCache.Host(type, GetContractFromType(type));
        }

        protected Type GetContractFromType(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            Type selectedOne = null;
            
            foreach (Type inter in interfaces)
            {
                if (inter.IsDefined(typeof(MainContractAttribute), false))
                {
                    if (selectedOne != null)
                    {
                        if (inter.IsAssignableFrom(selectedOne))
                        {
                            logger.Debug("Found multiple assignable interface types. Inheritance. Skipping...");
                            continue;
                        }

                        logger.Warn("Found multiple assignable interface types. Going on...");
                    }

                    selectedOne = inter;
                }
            }

            if (selectedOne == null)
                throw new ApplicationException("MainContract not found for type: " + type.Name);

            return selectedOne;
        }
    }
}

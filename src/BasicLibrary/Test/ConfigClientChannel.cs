using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace Test
{
    public class ConfigClientChannel<T> : ChannelFactory<T>
    {
        string configurationPath;



        /// <summary>

        /// Constructor

        /// </summary>

        /// <param name="configurationPath"></param>

        public ConfigClientChannel(string configurationPath)
            : base(typeof(T))
        {

            this.configurationPath = configurationPath;

            base.InitializeEndpoint((string)null, null);

        }


        /// <summary>

        /// Loads the serviceEndpoint description from the specified configuration file

        /// </summary>

        /// <returns></returns>

        protected override ServiceEndpoint CreateDescription()
        {

            ServiceEndpoint serviceEndpoint = base.CreateDescription();



            ExeConfigurationFileMap map = new ExeConfigurationFileMap();

            map.ExeConfigFilename = this.configurationPath;



            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            ServiceModelSectionGroup group = ServiceModelSectionGroup.GetSectionGroup(config);



            ChannelEndpointElement selectedEndpoint = null;



            foreach (ChannelEndpointElement endpoint in group.Client.Endpoints)
            {

                if (endpoint.Contract == serviceEndpoint.Contract.ConfigurationName)
                {

                    selectedEndpoint = endpoint;

                    break;

                }

            }



            if (selectedEndpoint != null)
            {

                if (serviceEndpoint.Binding == null)
                {

                    serviceEndpoint.Binding = CreateBinding(selectedEndpoint.Binding, group);

                }



                if (serviceEndpoint.Address == null)
                {

                    serviceEndpoint.Address = new EndpointAddress(selectedEndpoint.Address, GetIdentity(selectedEndpoint.Identity), selectedEndpoint.Headers.Headers);

                }



                if (serviceEndpoint.Behaviors.Count == 0 && selectedEndpoint.BehaviorConfiguration != null)
                {

                    AddBehaviors(selectedEndpoint.BehaviorConfiguration, serviceEndpoint, group);

                }



                serviceEndpoint.Name = selectedEndpoint.Contract;

            }



            return serviceEndpoint;

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class DataConfigElement : ConfigElement
    {
        [ConfigElement("options",Default=InstanceType.New)]
        public DataConfigOptionsElement Options { get; set; }
        [ConfigElement("defaultSessionFactory", Required = true)]
        public SessionFactoryElement DefaultSessionFactory { get; set; }
        [ConfigElement("sessionFactory")]
        public List<SessionFactoryElement> SessionFactories { get; set; }

        protected override void CheckConstraints()
        {
            base.CheckConstraints();
            foreach (SessionFactoryElement factory in SessionFactories)
            {
                if (string.IsNullOrEmpty( factory.Name)) throw new InvalidConfigurationException("All additional session factories must have a name");
            }
        }
    }
}
using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Configuration;
using SimpleLibrary.DataAccess;

namespace SimpleLibrary.Config
{
    public class ExceptionHandlingElement : ConfigElement
    {
        [ConfigElement("type", Default = InstanceType.New)]
        public List<TypeConfigElement> Types { get; set; }

        public IEnumerable<IExceptionHandler> GetHandlers()
        {
            foreach (TypeConfigElement typeElement in Types)
            {
                Type type = typeElement.LoadType();
                if (!typeof(IExceptionHandler).IsAssignableFrom(type)) throw new InvalidConfigurationException("Unable to create " + typeElement.Name + ". It's not IExceptionHandler.");
                yield return (IExceptionHandler)Activator.CreateInstance(type);
            }
        }

    }
}

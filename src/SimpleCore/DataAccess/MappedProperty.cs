using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.DataAccess
{
    public class MappedProperty
    {
        public object Entity { get; protected set; }
        public object PropertyValue { get; set; }
        public MappedProperty(object entity, object propertyValue)
        {
            Entity = entity;
            PropertyValue = propertyValue;
        }

        public override string ToString()
        {
            if (PropertyValue == null) return null;

            return PropertyValue.ToString();
        }

    }
}

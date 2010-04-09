using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Entities
{
    [Serializable]
    public class ValidationItem
    {
        public string Message { get; protected set; }
        public string PropertyName { get; protected set; }
        public string PropertyPath { get; protected set; }

        public ValidationItem RootedBy(string baseProp)
        {
            return new ValidationItem(Message, PropertyName, baseProp + "." + PropertyPath);
        }

        public ValidationItem(string message, string propertyName, string propertyPath)
        {
            this.Message = message;
            this.PropertyName = propertyName;
            this.PropertyPath = propertyPath;
        }
    }
}

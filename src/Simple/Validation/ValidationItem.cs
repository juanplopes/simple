using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Validation
{
    [Serializable]
    public class ValidationItem
    {
        public ValidationItem(string propertyName, string message)
        {
            this.PropertyName = propertyName;
            this.Message = message;
        }

        public ValidationItem(string propertyName, string message, object attemptedValue)
            : this(propertyName, message)
        {
            this.AttemptedValue = attemptedValue;
        }


        public string PropertyName { get; protected set; }
        public string Message { get; protected set; }
        public object AttemptedValue { get; set; }
    }
}

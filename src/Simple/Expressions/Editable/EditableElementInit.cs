using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public class EditableElementInit
    {
        public EditableExpressionCollection Arguments { get; set; }

        [IgnoreDataMember]
        public MethodInfo AddMethod
        {
            get
            {
                return ReflectionExtensions.FromMethodSerializableForm(AddMethodName);
            }
            set
            {
                AddMethodName = value.ToSerializableForm();
            }
        }
        public string AddMethodName { get; set; }




        // Ctors
        public EditableElementInit()
        {
            Arguments = new EditableExpressionCollection();
        }

        public EditableElementInit(ElementInit elmInit)
            : this()
        {
            AddMethod = elmInit.AddMethod;
            foreach (Expression ex in elmInit.Arguments)
            {
                Arguments.Add(EditableExpression.Create(ex));
            }
        }

        // Methods
        public ElementInit ToElementInit()
        {
            return Expression.ElementInit(AddMethod, Arguments.GetExpressions());
        }
    }
}

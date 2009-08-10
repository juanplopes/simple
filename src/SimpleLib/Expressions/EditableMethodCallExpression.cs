using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Simple.Expressions
{
    [Serializable]
    public class EditableMethodCallExpression : EditableExpression
    {                        
        // Properties
        public EditableExpressionCollection Arguments
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public MethodInfo Method
        {
            get;
            set;
        }

        public string MethodName
        {
            get
            {
                return Method.ToSerializableForm();
            }
            set
            {
                Method = Method.FromSerializableForm(value);
            }
        }

        public EditableExpression Object
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get;
            set;
        }

        // Ctors
        public EditableMethodCallExpression()
        {
        }

        public EditableMethodCallExpression(EditableExpressionCollection arguments, MethodInfo method, EditableExpression theObject, ExpressionType nodeType)
        {
            Arguments = arguments;
            Method = method;
            Object = theObject;
            NodeType = nodeType;
        }

        public EditableMethodCallExpression(IEnumerable<EditableExpression> arguments, MethodInfo method, Expression theObject, ExpressionType nodeType) :
            this(new EditableExpressionCollection(arguments), method, EditableExpression.CreateEditableExpression(theObject), nodeType)
        { 
        }
        
        public EditableMethodCallExpression(MethodCallExpression callEx) :
            this(new EditableExpressionCollection(callEx.Arguments),callEx.Method,EditableExpression.CreateEditableExpression(callEx.Object),callEx.NodeType)
        {
        }

        // Methods
        public override Expression ToExpression()
        {
            Expression instanceExpression = null;
            if (Object != null)
                instanceExpression = Object.ToExpression();

            return Expression.Call(instanceExpression, Method, Arguments.GetExpressions().ToArray<Expression>());
        }
    }
}

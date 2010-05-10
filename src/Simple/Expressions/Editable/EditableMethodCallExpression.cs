using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableMethodCallExpression : EditableExpressionImpl<MethodCallExpression>
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
            get
            {
                return ReflectionExtensions.FromMethodSerializableForm(MethodName);
            }
            set
            {
                MethodName = value.ToSerializableForm();
            }
        }

        public string MethodName { get; set; }

        

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
            this(new EditableExpressionCollection(arguments), method, EditableExpression.Create(theObject), nodeType)
        { 
        }
        
        public EditableMethodCallExpression(MethodCallExpression callEx) :
            this(new EditableExpressionCollection(callEx.Arguments),callEx.Method,EditableExpression.Create(callEx.Object),callEx.NodeType)
        {
        }

        // Methods
        public override MethodCallExpression ToTypedExpression()
        {
            Expression instanceExpression = null;
            if (Object != null)
                instanceExpression = Object.ToExpression();

            return Expression.Call(instanceExpression, Method, Arguments.GetExpressions().ToArray<Expression>());
        }

    }
}

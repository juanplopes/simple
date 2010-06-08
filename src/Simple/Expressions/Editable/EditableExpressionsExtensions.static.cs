using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;



namespace Simple.Expressions.Editable
{
    public static partial class EditableExpressionsExtensions
    {
        public static EditableConstantExpression ToEditableExpression(this ConstantExpression ex)
        {
            return new EditableConstantExpression(ex);
        }
        
        public static EditableBinaryExpression ToEditableExpression(this BinaryExpression ex)
        {
            return new EditableBinaryExpression(ex);
        }
        
        public static EditableConditionalExpression ToEditableExpression(this ConditionalExpression ex)
        {
            return new EditableConditionalExpression(ex);
        }
        
        public static EditableInvocationExpression ToEditableExpression(this InvocationExpression ex)
        {
            return new EditableInvocationExpression(ex);
        }
        
        public static EditableLambdaExpression ToEditableExpression(this LambdaExpression ex)
        {
            return new EditableLambdaExpression(ex);
        }
        
        public static EditableParameterExpression ToEditableExpression(this ParameterExpression ex)
        {
            return new EditableParameterExpression(ex);
        }
        
        public static EditableListInitExpression ToEditableExpression(this ListInitExpression ex)
        {
            return new EditableListInitExpression(ex);
        }
        
        public static EditableMemberExpression ToEditableExpression(this MemberExpression ex)
        {
            return new EditableMemberExpression(ex);
        }
        
        public static EditableMemberInitExpression ToEditableExpression(this MemberInitExpression ex)
        {
            return new EditableMemberInitExpression(ex);
        }
        
        public static EditableMethodCallExpression ToEditableExpression(this MethodCallExpression ex)
        {
            return new EditableMethodCallExpression(ex);
        }
        
        public static EditableNewArrayExpression ToEditableExpression(this NewArrayExpression ex)
        {
            return new EditableNewArrayExpression(ex);
        }
        
        public static EditableNewExpression ToEditableExpression(this NewExpression ex)
        {
            return new EditableNewExpression(ex);
        }
        
        public static EditableTypeBinaryExpression ToEditableExpression(this TypeBinaryExpression ex)
        {
            return new EditableTypeBinaryExpression(ex);
        }
        
        public static EditableUnaryExpression ToEditableExpression(this UnaryExpression ex)
        {
            return new EditableUnaryExpression(ex);
        }
        
    }
}

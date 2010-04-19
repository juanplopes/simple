using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    public partial class EditableExpression
    {
        public static EditableConstantExpression CreateTyped(ConstantExpression ex)
        {
            return new EditableConstantExpression(ex);
        }
        
        public static EditableBinaryExpression CreateTyped(BinaryExpression ex)
        {
            return new EditableBinaryExpression(ex);
        }
        
        public static EditableConditionalExpression CreateTyped(ConditionalExpression ex)
        {
            return new EditableConditionalExpression(ex);
        }
        
        public static EditableInvocationExpression CreateTyped(InvocationExpression ex)
        {
            return new EditableInvocationExpression(ex);
        }
        
        public static EditableLambdaExpression CreateTyped(LambdaExpression ex)
        {
            return new EditableLambdaExpression(ex);
        }
        
        public static EditableParameterExpression CreateTyped(ParameterExpression ex)
        {
            return new EditableParameterExpression(ex);
        }
        
        public static EditableListInitExpression CreateTyped(ListInitExpression ex)
        {
            return new EditableListInitExpression(ex);
        }
        
        public static EditableMemberExpression CreateTyped(MemberExpression ex)
        {
            return new EditableMemberExpression(ex);
        }
        
        public static EditableMemberInitExpression CreateTyped(MemberInitExpression ex)
        {
            return new EditableMemberInitExpression(ex);
        }
        
        public static EditableMethodCallExpression CreateTyped(MethodCallExpression ex)
        {
            return new EditableMethodCallExpression(ex);
        }
        
        public static EditableNewArrayExpression CreateTyped(NewArrayExpression ex)
        {
            return new EditableNewArrayExpression(ex);
        }
        
        public static EditableNewExpression CreateTyped(NewExpression ex)
        {
            return new EditableNewExpression(ex);
        }
        
        public static EditableTypeBinaryExpression CreateTyped(TypeBinaryExpression ex)
        {
            return new EditableTypeBinaryExpression(ex);
        }
        
        public static EditableUnaryExpression CreateTyped(UnaryExpression ex)
        {
            return new EditableUnaryExpression(ex);
        }
        
    }
}

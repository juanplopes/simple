using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Simple.Expressions.Editable
{
    public class EditableExpression<TDelegate> : EditableLambdaExpression
    {
        public EditableExpression() : base() { }
        public EditableExpression(Expression<TDelegate> lambda) : base(lambda) { }
    }

    [Serializable]
    public abstract partial class EditableExpression
    {
        public abstract ExpressionType NodeType { get; set; }

        [IgnoreDataMember]
        public Type Type
        {
            get
            {
                return ReflectionExtensions.FromTypeSerializableForm(TypeName);
            }
            set
            {
                TypeName = value.ToSerializableForm();
            }
        }

        public string TypeName { get; set; }
     
        // Ctors
        public EditableExpression() { } //allow for non parameterized creation for all expressions

        public EditableExpression(Type type)
        {
            Type = type;
        }

        // Methods
        public static EditableExpression<TDelegate> CreateTyped<TDelegate>(Expression<TDelegate> ex)
        {
            return new EditableExpression<TDelegate>(ex);
        }


        public static EditableExpression Create(Expression ex)
        {
            return Create(ex, false);
        }

        public static EditableExpression Create(Expression ex, bool funcletize)
        {
            if (ex == null) return null;

            if (funcletize) ex = Evaluator.PartialEval(ex);
            switch (ex.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    return CreateTyped(ex as UnaryExpression);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    return CreateTyped(ex as BinaryExpression);
                case ExpressionType.TypeIs:
                    return CreateTyped(ex as TypeBinaryExpression);
                case ExpressionType.Conditional:
                    return CreateTyped(ex as ConditionalExpression);
                case ExpressionType.Constant:
                    return CreateTyped(ex as ConstantExpression);
                case ExpressionType.Parameter:
                    return CreateTyped(ex as ParameterExpression);
                case ExpressionType.MemberAccess:
                    return CreateTyped(ex as MemberExpression);
                case ExpressionType.Call:
                    return CreateTyped(ex as MethodCallExpression);
                case ExpressionType.Lambda:
                    return CreateTyped(ex as LambdaExpression);
                case ExpressionType.New:
                    return CreateTyped(ex as NewExpression);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    return CreateTyped(ex as NewArrayExpression);
                case ExpressionType.Invoke:
                    return CreateTyped(ex as InvocationExpression);
                case ExpressionType.MemberInit:
                    return CreateTyped(ex as MemberInitExpression);
                case ExpressionType.ListInit:
                    return CreateTyped(ex as ListInitExpression);
                default:
                    throw new ArgumentException("How could this happen? Did microsoft create new expression types?");
            }
        }

        public abstract Expression ToExpression();
    }
}

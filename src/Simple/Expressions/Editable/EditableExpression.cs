using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableExpression<TDelegate> : EditableLambdaExpression
    {
        public EditableExpression() : base() { }
        public EditableExpression(Expression<TDelegate> lambda) : base(lambda) { }

        public Expression<TDelegate> ToTypedLambda()
        {
            return (Expression<TDelegate>)ToTypedExpression();
        }
    }

    [Serializable]
    public abstract partial class EditableExpressionImpl<T> : EditableExpression, IEditableImplementation<T>
        where T : Expression
    {
          // Ctors
        public EditableExpressionImpl() : base() { } //allow for non parameterized creation for all expressions

        public EditableExpressionImpl(Type type)
            : base(type)
        {
        }

        public abstract T ToTypedExpression();

        public override Expression ToExpression()
        {
            return ToTypedExpression();
        }
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

        public static EditableExpression Create(Expression ex)
        {
            if (ex == null) return null;

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
                    return (ex as UnaryExpression).ToEditableExpression();
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
                    return (ex as BinaryExpression).ToEditableExpression();
                case ExpressionType.TypeIs:
                    return (ex as TypeBinaryExpression).ToEditableExpression();
                case ExpressionType.Conditional:
                    return (ex as ConditionalExpression).ToEditableExpression();
                case ExpressionType.Constant:
                    return (ex as ConstantExpression).ToEditableExpression();
                case ExpressionType.Parameter:
                    return (ex as ParameterExpression).ToEditableExpression();
                case ExpressionType.MemberAccess:
                    return (ex as MemberExpression).ToEditableExpression();
                case ExpressionType.Call:
                    return (ex as MethodCallExpression).ToEditableExpression();
                case ExpressionType.Lambda:
                    return (ex as LambdaExpression).ToEditableExpression();
                case ExpressionType.New:
                    return (ex as NewExpression).ToEditableExpression();
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    return (ex as NewArrayExpression).ToEditableExpression();
                case ExpressionType.Invoke:
                    return (ex as InvocationExpression).ToEditableExpression();
                case ExpressionType.MemberInit:
                    return (ex as MemberInitExpression).ToEditableExpression();
                case ExpressionType.ListInit:
                    return (ex as ListInitExpression).ToEditableExpression();
                default:
                    throw new ArgumentException("How could this happen? Did microsoft create new expression types?");
            }
        }

        public abstract Expression ToExpression();
    }
}

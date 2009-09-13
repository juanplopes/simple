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
    public abstract class EditableExpression
    {
        public abstract ExpressionType NodeType { get; set; }

        [IgnoreDataMember]
        public Type Type { get; set; }
        public string TypeName
        {
            get
            {
                if (Type == null)
                    return null;

                return Type.ToSerializableForm();
            }
            set
            {
                if (value != null)
                    Type = Type.FromSerializableForm(value);
            }
        }

        // Ctors
        public EditableExpression() { } //allow for non parameterized creation for all expressions

        public EditableExpression(Type type)
        {
            Type = type;
        }

        // Methods
        public static EditableExpression CreateEditableExpression<TResult>(Expression<Func<TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }

        public static EditableExpression CreateEditableExpression<TArg0, TResult>(Expression<Func<TArg0, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }


        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TResult>(Expression<Func<TArg0, TArg1, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }


        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TArg2, TResult>(Expression<Func<TArg0, TArg1, TArg2, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TArg2, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }


        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TArg2, TArg3, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }

        internal static EditableExpression CreateEditableExpression(Expression ex)
        {
            return CreateEditableExpression(ex, false);
        }

        public static EditableExpression CreateEditableExpression(Expression ex, bool funcletize)
        {
            if (funcletize) ex = Evaluator.PartialEval(ex);

            if (ex is ConstantExpression) return new EditableConstantExpression(ex as ConstantExpression);
            else if (ex is BinaryExpression) return new EditableBinaryExpression(ex as BinaryExpression);
            else if (ex is ConditionalExpression) return new EditableConditionalExpression(ex as ConditionalExpression);
            else if (ex is InvocationExpression) return new EditableInvocationExpression(ex as InvocationExpression);
            else if (ex is LambdaExpression) return new EditableLambdaExpression(ex as LambdaExpression);
            else if (ex is ParameterExpression) return new EditableParameterExpression(ex as ParameterExpression);
            else if (ex is ListInitExpression) return new EditableListInitExpression(ex as ListInitExpression);
            else if (ex is MemberExpression) return new EditableMemberExpression(ex as MemberExpression);
            else if (ex is MemberInitExpression) return new EditableMemberInitExpression(ex as MemberInitExpression);
            else if (ex is MethodCallExpression) return new EditableMethodCallExpression(ex as MethodCallExpression);
            else if (ex is NewArrayExpression) return new EditableNewArrayExpression(ex as NewArrayExpression);
            else if (ex is NewExpression) return new EditableNewExpression(ex as NewExpression);
            else if (ex is TypeBinaryExpression) return new EditableTypeBinaryExpression(ex as TypeBinaryExpression);
            else if (ex is UnaryExpression) return new EditableUnaryExpression(ex as UnaryExpression);
            else return null;
        }

        public abstract Expression ToExpression();
    }
}

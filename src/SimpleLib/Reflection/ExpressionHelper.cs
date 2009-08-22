using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;

namespace Simple.Reflection
{
    public class ExpressionHelper
    {
        public static string GetPropertyName<T, P>(Expression<Func<T, P>> expr)
        {
            if (!(expr.Body is MemberExpression)) throw new ArgumentException("Expression must be MemberExpression");
            return GetPropertyName(expr.Body as MemberExpression);
        }

        public static string GetPropertyName(MemberExpression expr)
        {
            if (expr.Member.MemberType != System.Reflection.MemberTypes.Property)
                throw new ArgumentException("MemberExpression must be PropertyExpression");

            string test = expr.Member.Name;
            if (expr.Expression is MemberExpression) test = GetPropertyName(expr.Expression as MemberExpression) + "." + test;
            return test;
        }

        public static object SetValue(MemberExpression expr, object target, object value, bool newInstance)
        {
            try
            {
                Expression exprChild = expr.Expression;
                if (exprChild is MemberExpression)
                    target = SetValue(exprChild as MemberExpression , target, null, true);

                PropertyInfo prop = (PropertyInfo)expr.Member;
                if (newInstance)
                    value = Activator.CreateInstance(prop.PropertyType);
                else
                    value = Convert.ChangeType(value, prop.PropertyType);

                InvokerFactory.Do.Create(prop.GetSetMethod()).Invoke(target, value, null);
                return value;
            }
            catch (FormatException)
            {
                return null;
            }
        }
        
    }
}

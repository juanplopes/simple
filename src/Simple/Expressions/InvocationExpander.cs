using System.Linq;
using System.Linq.Expressions;

namespace Simple.Expressions
{
    public class InvocationExpander : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;
        private readonly Expression _expansion;
        private readonly InvocationExpander _previous;


        public static T Expand<T>(T expr)
            where T : LambdaExpression
        {
            return (T)new InvocationExpander().Visit(expr);
        }

        public InvocationExpander()
        {

        }
        public InvocationExpander(ParameterExpression parameter, Expression expansion, InvocationExpander previous)
        {
            _parameter = parameter;
            _expansion = expansion;
            _previous = previous;
        }

        protected override Expression VisitInvocation(InvocationExpression iv)
        {
            if (iv.Expression.NodeType == ExpressionType.Lambda)
            {
                LambdaExpression lambda = (LambdaExpression)iv.Expression;
                return lambda
                    .Parameters
                    .Select((x, i) => new { Parameter = x, Expansion = iv.Arguments[i] })
                    // add to the stack of available parameters bindings (this class doubles as an immutable stack)
                    .Aggregate(this, (previous, pair) => new InvocationExpander(pair.Parameter, pair.Expansion, previous))
                    // visit the body of the lambda using an expander including the new parameter bindings
                    .Visit(lambda.Body);
            }
            return base.VisitInvocation(iv);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            InvocationExpander expander = this;
            while (null != expander)
            {
                if (expander._parameter == p)
                {
                    return base.Visit(expander._expansion);
                }
                expander = expander._previous;
            }
            return base.VisitParameter(p);
        }

    }
}

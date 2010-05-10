using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableLambdaExpression : EditableExpressionImpl<LambdaExpression>
    {
        public EditableExpression Body { get; set;}
        public EditableExpressionCollection Parameters { get; set;}
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Lambda; }
            set { }
        }

        // Ctors
        public EditableLambdaExpression()
        {
            Parameters = new EditableExpressionCollection();
        }      

        public EditableLambdaExpression(LambdaExpression lambEx)
            : base(lambEx.Type) 
        {
            Parameters = new EditableExpressionCollection();
            Body = EditableExpression.Create(lambEx.Body);
            foreach (ParameterExpression param in lambEx.Parameters)
                Parameters.Add(EditableExpression.Create(param));
        }

        // Methods
        public override LambdaExpression ToTypedExpression()
        {
            Expression body = Body.ToExpression();
            List<ParameterExpression> parameters = new List<ParameterExpression>(Parameters.GetParameterExpressions());

            var bodyParameters = from edX in body.Nodes()
                                 where edX is ParameterExpression
                                 select edX;
            for (int i = 0; i < parameters.Count; i++)
            {
                var matchingParm = from parm in bodyParameters
                                   where (parm as ParameterExpression).Name == parameters[i].Name
                                      && (parm as ParameterExpression).Type == parameters[i].Type
                                   select parm as ParameterExpression;
                if (matchingParm.Count<ParameterExpression>() == 1)
                    parameters[i] = matchingParm.First<ParameterExpression>() as ParameterExpression;
            }

            return Expression.Lambda(Type, body, parameters);
        }
    }
}

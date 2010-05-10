using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableListInitExpression : EditableExpressionImpl<ListInitExpression>
    {
        public EditableExpression NewExpression { get; set;}
        public EditableElementInitCollection Initializers { get; set;}
        public override ExpressionType NodeType
        {
            get { return ExpressionType.ListInit; }
            set { }
        }

        // Ctors
        public EditableListInitExpression()
        {
            Initializers = new EditableElementInitCollection();
        }

        public EditableListInitExpression(ListInitExpression listInit)
            : this()
        {
            NewExpression = EditableExpression.Create(listInit.NewExpression);
            foreach (ElementInit e in listInit.Initializers)
            {
                Initializers.Add(new EditableElementInit(e));
            }
        }

        public EditableListInitExpression(EditableExpression newEx, IEnumerable<EditableElementInit> initializers)
        {
            Initializers = new EditableElementInitCollection(initializers);
            NewExpression = newEx;
        }

        // Methods
        public override ListInitExpression ToTypedExpression()
        {
            return Expression.ListInit(NewExpression.ToExpression() as NewExpression, Initializers.GetElementsInit().ToArray<ElementInit>());
        }
    }
}

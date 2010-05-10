using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableMemberInitExpression : EditableExpressionImpl<MemberInitExpression>
    {
        // Properties
        public EditableNewExpression NewExpression
        {
            get;
            set;
        }

        public EditableMemberBindingCollection Bindings
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.MemberInit; }
            set { }
        }

        // Ctors
        public EditableMemberInitExpression()
        {
            Bindings = new EditableMemberBindingCollection();
        }

        public EditableMemberInitExpression(MemberInitExpression membInit)
            : this(EditableExpression.Create(membInit.NewExpression) as EditableNewExpression, membInit.Bindings)
        {
        }

        public EditableMemberInitExpression(EditableNewExpression newEx, IEnumerable<MemberBinding> bindings)
        {
            Bindings = new EditableMemberBindingCollection(bindings);
            NewExpression = newEx;
        }

        public EditableMemberInitExpression(NewExpression newRawEx, IEnumerable<MemberBinding> bindings)
            : this(EditableExpression.Create(newRawEx) as EditableNewExpression, bindings)
        {
        }

        // Methods
        public override MemberInitExpression ToTypedExpression()
        {
            return Expression.MemberInit(NewExpression.ToExpression() as NewExpression, Bindings.GetMemberBindings());
        }
    }
}

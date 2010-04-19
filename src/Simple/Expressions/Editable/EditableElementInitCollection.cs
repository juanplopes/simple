using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableElementInitCollection : List<EditableElementInit>
    {
        public EditableElementInitCollection() : base() { }
        public EditableElementInitCollection(IEnumerable<EditableElementInit> source) : base(source) { }
        public EditableElementInitCollection(IEnumerable<ElementInit> source) 
        {
            foreach (ElementInit ex in source)
                this.Add(new EditableElementInit(ex));
        }

        public IEnumerable<ElementInit> GetElementsInit()
        {
            foreach (EditableElementInit editEx in this)
                yield return editEx.ToElementInit();
        }       
    }
}

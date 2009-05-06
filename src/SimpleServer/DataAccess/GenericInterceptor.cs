using System;
using System.Collections.Generic;

using System.Text;
using NHibernate;

namespace Simple.DataAccess
{
    public class GenericInterceptor : EmptyInterceptor
    {
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            return base.OnSave(entity, id, state, propertyNames, types);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Event;

namespace Sample.BusinessServer.Infra
{
    public class DefaultSaveListener : IPostInsertEventListener, IPostUpdateEventListener
    {

        #region IPostInsertEventListener Members

        public void OnPostInsert(PostInsertEvent @event)
        {
            
        }

        #endregion

        #region IPostUpdateEventListener Members

        public void OnPostUpdate(PostUpdateEvent @event)
        {

        }

        #endregion
    }
}

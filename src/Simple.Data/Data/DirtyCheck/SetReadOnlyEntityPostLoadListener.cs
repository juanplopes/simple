using System;
using NHibernate.Engine;
using NHibernate.Event;

namespace Simple.Data.DirtyCheck
{
	[Serializable]
	public class SetReadOnlyEntityPostLoadListener : IPostLoadEventListener, ILockEventListener
	{
		#region IPostLoadEventListener Members

		public void OnPostLoad(PostLoadEvent @event)
		{
			SetReadOnlyEntity(@event.Session, @event.Entity);
		}

		#endregion

		#region Implementation of ILockEventListener

		public void OnLock(LockEvent @event)
		{
			SetReadOnlyEntity(@event.Session, @event.Entity);
		}

		#endregion

		private void SetReadOnlyEntity(IEventSource session, object entity)
		{
			if (session.FetchProfile != "merge")
			{
				EntityEntry entry = session.PersistenceContext.GetEntry(entity);
                entry.Status = Status.ReadOnly;
			}
		}
	}
}
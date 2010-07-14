using System;
using System.Collections;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace Simple.Data.DirtyCheck
{
	[Serializable]
	public class ResetReadOnlyEntityListener : ISaveOrUpdateEventListener, IMergeEventListener, IPersistEventListener
	{
		public static readonly CascadingAction ResetReadOnly = new ResetReadOnlyCascadeAction();

		#region ISaveOrUpdateEventListener Members

		public void OnSaveOrUpdate(SaveOrUpdateEvent @event)
		{
			ResetReadOnlyEntity(@event.Session, @event.Entity);
		}

		#endregion

		#region Implementation of IMergeEventListener

		public void OnMerge(MergeEvent @event)
		{
			OnMerge(@event, null);
		}

		public void OnMerge(MergeEvent @event, IDictionary copiedAlready)
		{
			ResetReadOnlyEntity(@event.Session, @event.Original);
		}

		#endregion

		#region Implementation of IPersistEventListener

		public void OnPersist(PersistEvent @event)
		{
			OnPersist(@event, null);
		}

		public void OnPersist(PersistEvent @event, IDictionary createdAlready)
		{
			ResetReadOnlyEntity(@event.Session, @event.Entity);
		}

		#endregion

		private void ResetReadOnlyEntity(IEventSource session, object entity)
		{
			EntityEntry entry = session.PersistenceContext.GetEntry(entity);
			if (entry != null && entry.Persister.IsMutable && entry.Status == Status.ReadOnly)
			{
                entry.Status = Status.Loaded;
				CascadeOnUpdate(session, entry.Persister, entity);
			}
		}

		private static void CascadeOnUpdate(IEventSource source, IEntityPersister persister, object entity)
		{
			source.PersistenceContext.IncrementCascadeLevel();
			try
			{
				new Cascade(ResetReadOnly, CascadePoint.BeforeFlush, source).CascadeOn(persister, entity);
			}
			finally
			{
				source.PersistenceContext.DecrementCascadeLevel();
			}
		}
	}
}
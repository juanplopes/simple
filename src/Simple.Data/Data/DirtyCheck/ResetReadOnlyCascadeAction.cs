using System;
using System.Collections;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Type;

namespace Simple.Data.DirtyCheck
{
	[Serializable]
	public class ResetReadOnlyCascadeAction : CascadingAction
	{
		public override void Cascade(IEventSource session, object child, string entityName, object anything, bool isCascadeDeleteEnabled)
		{
			if (NHibernateUtil.IsInitialized(child))
			{
				object instance = session.PersistenceContext.Unproxy(child);
				EntityEntry entry = session.PersistenceContext.GetEntry(instance);
				if (entry != null && entry.Persister.IsMutable && entry.Status == Status.ReadOnly)
				{
                    entry.Status = Status.Loaded;
				}
			}
		}

		public override IEnumerable GetCascadableChildrenIterator(IEventSource session, CollectionType collectionType, object collection)
		{
			return GetLoadedElementsIterator(session, collectionType, collection);
		}

		public override bool DeleteOrphans
		{
			get { return true; }
		}
	}
}
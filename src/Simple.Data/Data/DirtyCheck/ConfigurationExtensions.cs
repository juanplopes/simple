using System.Linq;
using NHibernate.Cfg;
using NHibernate.Event;

namespace Simple.Data.DirtyCheck
{
	public static class ConfigurationExtensions
	{
		public static Configuration RegisterDisableAutoDirtyCheckListeners(this Configuration configuration)
		{
			EventListeners listeners = configuration.EventListeners;
			var readOnlyEntityListener = new ResetReadOnlyEntityListener();
			var readOnlyEntityLoadListener = new SetReadOnlyEntityPostLoadListener();
			listeners.PersistEventListeners =
				new[] { readOnlyEntityListener }.Concat(listeners.PersistEventListeners).ToArray();
			listeners.MergeEventListeners =
				new[] { readOnlyEntityListener }.Concat(listeners.MergeEventListeners).ToArray();
			listeners.UpdateEventListeners =
				new[] {readOnlyEntityListener}.Concat(listeners.UpdateEventListeners).ToArray();
			listeners.SaveOrUpdateEventListeners =
				new[] { readOnlyEntityListener }.Concat(listeners.SaveOrUpdateEventListeners).ToArray();
			listeners.DeleteEventListeners =
				new[] {new ResetReadOnlyEntityDeleteListener()}.Concat(listeners.DeleteEventListeners).ToArray();
			listeners.PostLoadEventListeners =
				new[] {readOnlyEntityLoadListener}.Concat(listeners.PostLoadEventListeners).ToArray();
			listeners.LockEventListeners =
				listeners.LockEventListeners.Concat(new[] { readOnlyEntityLoadListener }).ToArray();

			return configuration;
		}
	}
}
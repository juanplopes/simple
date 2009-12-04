using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simple.Common
{
    /// <summary>
    /// Provides helper methods for managing wait handles.
    /// </summary>
    public static class NamedEvents
    {
        /// <summary>
        /// Close any open handles with <paramref name="name"/> and open a new one.
        /// </summary>
        /// <param name="name">The handle name.</param>
        /// <param name="initialState">Initial state.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>The new EventWaitHandle created.</returns>
        public static EventWaitHandle OpenOrCreate(string name, bool initialState, EventResetMode mode)
        {
            try
            {
                EventWaitHandle.OpenExisting(name).Close();
            }
            catch (WaitHandleCannotBeOpenedException) { }

            return new EventWaitHandle(initialState, mode, name);
        }


        /// <summary>
        /// Try opening a new EventWaitHandle or wait for one to be created.
        /// </summary>
        /// <param name="name">The handle name.</param>
        /// <returns>The returned handle.</returns>
        public static EventWaitHandle OpenOrWait(string name)
        {
            EventWaitHandle ewh = null;

            while (ewh == null)
            {
                try
                {
                    ewh = EventWaitHandle.OpenExisting(name);
                }
                catch (WaitHandleCannotBeOpenedException)
                {
                    Thread.Sleep(50);
                }
            }

            return ewh;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simple.Common
{
    public static class NamedEvents
    {

        public static EventWaitHandle OpenOrCreate(string name, bool initialState, EventResetMode mode)
        {

            EventWaitHandle ewh = null;

            try
            {

                ewh = EventWaitHandle.OpenExisting(name);
                ewh.Close();
                ewh = new EventWaitHandle(initialState, mode, name);
            }

            catch (WaitHandleCannotBeOpenedException)
            {

                //Handle does not exist, create it.

                ewh = new EventWaitHandle(initialState, mode, name);

            }



            return ewh;

        }



        public static EventWaitHandle OpenOrWait(string name)
        {

            EventWaitHandle ewh = null;



            while (null == ewh)
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

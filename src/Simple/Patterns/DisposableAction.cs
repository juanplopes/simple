using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Patterns
{
    public class DisposableAction : IDisposable
    {
        protected Action WhenDisposing { get; set; }

        public DisposableAction(Action whatToDo)
        {
            WhenDisposing = whatToDo;
        }

        #region IDisposable Members

        public void Dispose()
        {
            WhenDisposing();
        }

        #endregion
    }
}

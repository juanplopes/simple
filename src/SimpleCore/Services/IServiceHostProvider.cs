using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Services
{
    public interface IServiceHostProvider
    {
        void Add(Type type);
    }

    public class NullServiceHostProvider : IServiceHostProvider
    {

        #region IServiceHost Members

        public void Add(Type type)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Services
{
    public interface IServiceHostProvider
    {
        void Host(object server, Type contract);
        void Start();
        void Stop();
    }

    public class NullServiceHostProvider : IServiceHostProvider
    {

        public void Host(object server, Type contract)
        {
        }

        public void Start()
        {
        }

        public void Stop()
        {

        }

    }
}

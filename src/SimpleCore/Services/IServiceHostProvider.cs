using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Services
{
    public interface IServiceHostProvider
    {
        void Host(Type type, Type contract);
        void Start();
        void Stop();
    }

    public class NullServiceHostProvider : IServiceHostProvider
    {

        public void Host(Type type, Type contract)
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

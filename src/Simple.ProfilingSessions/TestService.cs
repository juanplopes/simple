using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services;

namespace Simple.ProfilingSessions
{
    public interface IBaseService : IService
    {
        void A00();
        void A01();
        void A02();
        void A03();
        void A04();
        void A05();
        void A06();
        void A07();
        void A08();
        void A09();

        void A10();
        void A11();
        void A12();
        void A13();
        void A14();
        void A15();
        void A16();
        void A17();
        void A18();
        void A19();

        void A20();
        void A21();
        void A22();
        void A23();
        void A24();
        void A25();
        void A26();
        void A27();
        void A28();
        void A29();
    }

    public interface ITestService : IBaseService, IService
    {
        void A30();
    }

    public class TestService : MarshalByRefObject, ITestService
    {
        public void A30() { }

        #region IBaseService Members

        public void A00()
        {

        }

        public void A01()
        {
            throw new NotImplementedException();
        }

        public void A02()
        {
            throw new NotImplementedException();
        }

        public void A03()
        {
            throw new NotImplementedException();
        }

        public void A04()
        {
            throw new NotImplementedException();
        }

        public void A05()
        {
            throw new NotImplementedException();
        }

        public void A06()
        {
            throw new NotImplementedException();
        }

        public void A07()
        {
            throw new NotImplementedException();
        }

        public void A08()
        {
            throw new NotImplementedException();
        }

        public void A09()
        {
            throw new NotImplementedException();
        }

        public void A10()
        {
            throw new NotImplementedException();
        }

        public void A11()
        {
            throw new NotImplementedException();
        }

        public void A12()
        {
            throw new NotImplementedException();
        }

        public void A13()
        {
            throw new NotImplementedException();
        }

        public void A14()
        {
            throw new NotImplementedException();
        }

        public void A15()
        {
            throw new NotImplementedException();
        }

        public void A16()
        {
            throw new NotImplementedException();
        }

        public void A17()
        {
            throw new NotImplementedException();
        }

        public void A18()
        {
            throw new NotImplementedException();
        }

        public void A19()
        {
            throw new NotImplementedException();
        }

        public void A20()
        {
            throw new NotImplementedException();
        }

        public void A21()
        {
            throw new NotImplementedException();
        }

        public void A22()
        {
            throw new NotImplementedException();
        }

        public void A23()
        {
            throw new NotImplementedException();
        }

        public void A24()
        {
            throw new NotImplementedException();
        }

        public void A25()
        {
            throw new NotImplementedException();
        }

        public void A26()
        {
            throw new NotImplementedException();
        }

        public void A27()
        {
            throw new NotImplementedException();
        }

        public void A28()
        {
            throw new NotImplementedException();
        }

        public void A29()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

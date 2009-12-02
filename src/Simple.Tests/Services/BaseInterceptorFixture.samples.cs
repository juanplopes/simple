using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services;
using System.Globalization;
using NUnit.Framework;
using Simple.DynamicProxy;
using System.Linq.Expressions;
using Simple.Expressions;

namespace Simple.Tests.Services
{
    public partial class BaseInterceptorFixture
    {
        public static Func<CallHookArgs, ICallHook> HookFunc<T>(Func<CallHookArgs, ICallHook> hook)
        {
            return x=>typeof(T).IsAssignableFrom(x.Method.DeclaringType) ? hook(x) : null;
        }

        public class FindMeAttribute : Attribute { }

        public class AttributeFinderCallHook : BaseCallHook
        {
            public AttributeFinderCallHook(CallHookArgs args) : base(args) { }

            public override void AfterSuccess()
            {
                if (CallArgs.Method.IsDefined(typeof(FindMeAttribute), true) || CallArgs.Client)
                    CallArgs.Return = true;
            }
        }

        public class TestCallHook : BaseCallHook
        {
            public TestCallHook(CallHookArgs args) : base(args) { }

            public override void AfterSuccess()
            {
                if (CallArgs.Return is int)
                {
                    CallArgs.Return = 42;
                }
            }
        }


        public class TestCallHookString : BaseCallHook
        {
            public TestCallHookString(CallHookArgs args) : base(args) { }

            public override void AfterSuccess()
            {
                if (CallArgs.Return is string)
                {
                    CallArgs.Return = "123456";
                }
                else if (CallArgs.Return is double)
                {
                    throw new ArgumentException();
                }
            }

            public override void Finally()
            {
                if (CallArgs.Return is DateTime)
                {
                    throw new ArithmeticException();
                }
            }
        }

        public interface IFindMeService : IService
        {
            bool FindMe();
        }

        public interface ITestService : IService
        {
            void ReturnVoid();
            void ReturnVoid(int i);
            void ReturnVoidG<T>(T value) where T : IConvertible;
            void ReturnVoid(out string test);

            void ThrowException();

            int ReturnInt();
            int ReturnIntG<T>(T value) where T : IConvertible;
            int ReturnInt(int i);

            string ReturnString();
            string ReturnString(int prim, params string[] ult);
            string ReturnString(int i);

            double ReturnDoubleG<T>(T value) where T : IConvertible;
            double ReturnDouble(ref string test);

            string ReturnIdentityString();
            string ReturnIdentityStringG<T>(T data);
        }

        public interface IOtherService : IService
        {
            string ReturnString();
            double ThrowException();
            DateTime ThrowExceptionOnFinally();
        }

        public class FindMeService : MarshalByRefObject, IFindMeService
        {
            [FindMe]
            public bool FindMe() { return false; }
        }

        public class OtherService : MarshalByRefObject, IOtherService
        {
            public string ReturnString()
            {
                return "NOT123456";
            }

            public double ThrowException()
            {
                return 42.42f;
            }

            public DateTime ThrowExceptionOnFinally()
            {
                return new DateTime(2009, 09, 09);
            }
        }

        public class TestService : MarshalByRefObject, ITestService, ICloneable
        {
            public void ReturnVoid() { }
            public int ReturnInt() { return 10; }
            public string ReturnString() { return "10"; }
            public void ReturnVoid(int i) { }
            public int ReturnInt(int i) { return i; }
            public string ReturnString(int i) { return i.ToString(); }
            public void ThrowException() { throw new ApplicationException("AAA"); }

            public void ReturnVoidG<T>(T value) where T : IConvertible
            {
                Assert.IsFalse(this is IDynamicProxy);
                value.ToDouble(CultureInfo.InvariantCulture);
            }
            public double ReturnDoubleG<T>(T value) where T : IConvertible
            {
                return value.ToDouble(CultureInfo.InvariantCulture);
            }

            public double ReturnDouble(ref string test) { int res = int.Parse(test); test = "42"; return res; }
            public void ReturnVoid(out string test) { test = "42"; }

            public string ReturnString(int prim, params string[] ult)
            {
                return prim.ToString() + ult[ult.Length - 1];
            }

            public string ReturnIdentityString()
            {
                return SimpleContext.Get().Username;
            }

            public string ReturnIdentityStringG<T>(T data)
            {
                return SimpleContext.Get().Username + data.ToString();
            }

            public int ReturnIntG<T>(T value) where T : IConvertible
            {
                return Convert.ToInt32(value);
            }

            #region ICloneable Members

            public object Clone()
            {
                return this.MemberwiseClone();
            }

            #endregion
        }
    }
}

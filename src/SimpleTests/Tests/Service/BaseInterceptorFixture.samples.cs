using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services;
using System.Globalization;

namespace Simple.Tests.Service
{
    public partial class BaseInterceptorFixture 
    {
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
            public static Func<CallHookArgs, ICallHook> MyFunc = x =>
            {
                if (typeof(IOtherService).IsAssignableFrom(x.Method.DeclaringType))
                {
                    return new TestCallHookString(x);
                }
                else
                {
                    return null;
                }
            };

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
        
        public interface ITestService : IService
        {
            void TestVoid();
            int TestInt();
            string TestString();
            void TestVoidInt(int i);
            int TestIntInt(int i);
            string TestStringInt(int i);
            void TestException();

            void TestGenericsVoid<T>(T value) where T : IConvertible;
            double TestGenerics<T>(T value) where T : IConvertible;
            double TestRefParams(ref string test);
            void TestOutParams(out string test);
            string TestParams(int prim, params string[] ult);
        }

        public interface IOtherService : IService
        {
            string SomeStringFunction();
            double ExceptionFunction();
            DateTime ThrowOnFinally();
        }

        public class OtherService : MarshalByRefObject, IOtherService
        {
            public string SomeStringFunction()
            {
                return "NOT123456";
            }

            public double ExceptionFunction()
            {
                return 42.42f;
            }

            public DateTime ThrowOnFinally()
            {
                return new DateTime(2009, 09, 09);
            }
        }

        public class TestService : MarshalByRefObject, ITestService
        {
            public void TestVoid() { }
            public int TestInt() { return 10; }
            public string TestString() { return "10"; }
            public void TestVoidInt(int i) { }
            public int TestIntInt(int i) { return i; }
            public string TestStringInt(int i) { return i.ToString(); }
            public void TestException() { throw new ApplicationException("AAA"); }

            public void TestGenericsVoid<T>(T value) where T : IConvertible
            {
                value.ToDouble(CultureInfo.InvariantCulture);
            }
            public double TestGenerics<T>(T value) where T : IConvertible
            {
                return value.ToDouble(CultureInfo.InvariantCulture);
            }

            public double TestRefParams(ref string test) { int res = int.Parse(test); test = "42"; return res; }
            public void TestOutParams(out string test) { test = "42"; }

            public string TestParams(int prim, params string[] ult)
            {
                return prim.ToString() + ult[ult.Length - 1];
            }


        }
    }
}

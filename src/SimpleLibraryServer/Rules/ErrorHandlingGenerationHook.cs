using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace SimpleLibrary.Rules
{
    public class ErrorHandlingGenerationHook : IProxyGenerationHook
    {
        #region IProxyGenerationHook Members

        public void MethodsInspected()
        {

        }

        public void NonVirtualMemberNotification(Type type, System.Reflection.MemberInfo memberInfo)
        {
            throw new InvalidOperationException("All business rules methods must be virtual.");
        }

        public bool ShouldInterceptMethod(Type type, System.Reflection.MethodInfo memberInfo)
        {
            return (memberInfo.IsPublic);
        }

        #endregion
    }
}

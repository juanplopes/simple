using System;
using System.Reflection;
using System.Reflection.Emit;
using Simple.Patterns;
using System.Linq;

namespace Simple.Reflection
{
    public delegate object InvocationDelegate(object target, params object[] args);

    public class InvokerFactory : Singleton<InvokerFactory>
    {
        public InvocationDelegate Create(MethodBase method)
        {
            return GetMethodInvoker(method);
        }

        
        private static InvocationDelegate GetMethodInvoker(MethodBase methodBase)
        {
            var dynamicMethod = CreateDynamicMethod(methodBase);
            var il = dynamicMethod.GetILGenerator();
            var parameters = methodBase.GetParameters();

            var paramTypes = parameters
                .Select(x => x.ParameterType)
                .Select(x => x.IsByRef ? x.GetElementType() : x)
                .ToArray();

            var locals = paramTypes.Select(x => il.DeclareLocal(x, true)).ToArray();

            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(il, paramTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }

            if (!methodBase.IsStatic && !methodBase.IsConstructor)
            {
                il.Emit(OpCodes.Ldarg_0);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                    il.Emit(OpCodes.Ldloca_S, locals[i]);
                else
                    il.Emit(OpCodes.Ldloc, locals[i]);
            }


            if (methodBase is MethodInfo)
            {
                var methodInfo = methodBase as MethodInfo;

                EmitCallMethod(il, methodInfo);
                HandleReturn(il, methodInfo);
            }
            else if (methodBase is ConstructorInfo)
            {
                var ctorInfo = methodBase as ConstructorInfo;

                il.Emit(OpCodes.Newobj, ctorInfo);
            }

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                {
                    il.Emit(OpCodes.Ldarg_1);
                    EmitFastInt(il, i);
                    il.Emit(OpCodes.Ldloc, locals[i]);
                    if (locals[i].LocalType.IsValueType)
                        il.Emit(OpCodes.Box, locals[i].LocalType);
                    il.Emit(OpCodes.Stelem_Ref);
                }
            }

            il.Emit(OpCodes.Ret);
            InvocationDelegate invoder = (InvocationDelegate)dynamicMethod.CreateDelegate(typeof(InvocationDelegate));
            return invoder;
        }

        private static void EmitCallMethod(ILGenerator il, MethodInfo methodInfo)
        {
            if (methodInfo.IsStatic)
                il.EmitCall(OpCodes.Call, methodInfo, null);
            else
                il.EmitCall(OpCodes.Callvirt, methodInfo, null);
        }

        private static void HandleReturn(ILGenerator il, MethodInfo methodInfo)
        {
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                EmitBoxIfNeeded(il, methodInfo.ReturnType);
        }

        private static DynamicMethod CreateDynamicMethod(MethodBase methodBase)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object),
                new Type[] { typeof(object), typeof(object[]) }, methodBase.DeclaringType.Module, true);
            return dynamicMethod;
        }

        private static void EmitCastToReference(ILGenerator il, System.Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                il.Emit(OpCodes.Castclass, type);
            }
        }

        private static void EmitBoxIfNeeded(ILGenerator il, System.Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
        }

        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }
    }
}

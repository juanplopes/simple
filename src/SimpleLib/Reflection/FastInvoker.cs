using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Simple.Reflection
{
    public class FastInvoker
    {
        public MethodInfo Method { get; protected set; }
        protected FastInvokeHandler Handler { get; set; }
        protected delegate object FastInvokeHandler(object target, object[] paramters);
           

        public FastInvoker(MethodBase method)
        {
            if (method is MethodInfo)
            {
                Method = (MethodInfo)method;
                Construct();
            }
            else
            {
                throw new ArgumentException("Method must be real to be proxied.");
            }
        }

        public FastInvoker(MethodInfo method)
        {
            Method = method;
            Construct();
        }

        protected void Construct()
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty,
                     typeof(object), new Type[] { typeof(object), 
                     typeof(object[]) },
                     Method.DeclaringType.Module, true);
            ILGenerator il = dynamicMethod.GetILGenerator();
            ParameterInfo[] ps = Method.GetParameters();
            Type[] paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = ps[i].ParameterType;
            }
            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i]);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(il, paramTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }
            il.Emit(OpCodes.Ldarg_0);
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldloc, locals[i]);
            }
            il.EmitCall(OpCodes.Call, Method, null);
            if (Method.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                EmitBoxIfNeeded(il, Method.ReturnType);
            il.Emit(OpCodes.Ret);
            FastInvokeHandler invoder =
              (FastInvokeHandler)dynamicMethod.CreateDelegate(
              typeof(FastInvokeHandler));
            Handler = invoder;
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

        public object Invoke(object target, params object[] parameters)
        {
            return Handler(target, parameters);
        }
    }
}

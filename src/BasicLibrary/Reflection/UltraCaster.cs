using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public class UltraCaster
    {

        public static object TryCast(object value, Type toType)
        {
            if (value == null) return null;
            
            Type fromType = value.GetType();

            if (toType.IsAssignableFrom(fromType)) return value;

            try
            {
                return Convert.ChangeType(value, toType);
            }
            catch (Exception)
            {
                MethodInfo bestImplicit = null, bestExplicit = null;

                foreach (MethodInfo method in fromType.GetMethods())
                {
                    if (method.Name == "op_Implicit")
                    {
                        if (toType.IsAssignableFrom(method.ReturnType))
                        {
                            bestImplicit = method;
                        }
                    }

                    if (method.Name == "op_Explicit")
                    {
                        if (toType.IsAssignableFrom(method.ReturnType))
                        {
                            bestExplicit = method;
                        }
                    }
                }

                if (bestImplicit != null) return bestImplicit.Invoke(null, new object[] {value});
                if (bestExplicit != null) return bestExplicit.Invoke(null, new object[] { value });

                foreach (MethodInfo method in toType.GetMethods())
                {
                    if (method.Name == "op_Implicit")
                    {
                        ParameterInfo[] parameters = method.GetParameters();
                        if (parameters.Length == 1 &&  parameters[0].ParameterType.IsAssignableFrom(fromType))
                        {
                            bestImplicit = method;
                        }
                    }

                    if (method.Name == "op_Explicit")
                    {
                        ParameterInfo[] parameters = method.GetParameters();
                        if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(fromType))
                        {
                            bestExplicit = method;
                        }
                    }
                }

                if (bestImplicit != null) return bestImplicit.Invoke(null, new object[] { value });
                if (bestExplicit != null) return bestExplicit.Invoke(null, new object[] { value });

                return null;
            }


        }
    }
}

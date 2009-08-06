using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using System.Collections;

namespace Simple.Services
{
    public class CallHeaders : Hashtable
    {
        public static CallHeaders Do
        {
            get
            {
                return Singleton<CallHeaders>.Do;
            }
        }
        public static void Force(CallHeaders headers)
        {
            Singleton<CallHeaders>.ForceInstance(headers);
        }
    }
}

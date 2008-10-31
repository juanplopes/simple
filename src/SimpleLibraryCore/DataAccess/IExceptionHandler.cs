using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.Rules
{
    public interface IExceptionHandler
    {
        bool Handle(Exception e);
    }
}

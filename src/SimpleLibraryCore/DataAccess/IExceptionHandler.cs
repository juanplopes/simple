using System;
using System.Collections.Generic;

using System.Text;

namespace SimpleLibrary.DataAccess
{
    public interface IExceptionHandler
    {
        bool Handle(Exception e);
    }
}

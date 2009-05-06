using System;
using System.Collections.Generic;

using System.Text;

namespace Simple.DataAccess
{
    public interface IExceptionHandler
    {
        bool Handle(Exception e);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Simple.Web.Mvc
{
    public class NullTempDataProvider : ITempDataProvider
    {
        #region ITempDataProvider Members

        public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            return new Dictionary<string, object>();
        }

        public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
        }

        #endregion
    }
}

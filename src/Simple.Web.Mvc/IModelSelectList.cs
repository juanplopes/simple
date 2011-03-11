using System;
using System.Web.Mvc;
using System.Collections.Generic;
namespace Simple.Web.Mvc
{
    public interface IModelSelectList : IList<SelectListItem>
    {
        IModelSelectList ClearSelection();
        IModelSelectList SelectValues(params object[] selectedValues);
        IModelSelectList Sort();
        IModelSelectList Select(params object[] models);
    }

    public interface IModelSelectList<T> : IModelSelectList
    {
        IModelSelectList<T> Select(params T[] models);
        Func<T, object> TextSelector { get; set; }
        Func<T, object> ValueSelector { get; }
    }
}

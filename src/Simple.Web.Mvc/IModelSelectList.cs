using System;
using System.Web.Mvc;
using System.Collections.Generic;
namespace Simple.Web.Mvc
{
    public interface IModelSelectList : IList<SelectListItem>
    {
        IModelSelectList ClearSelection();
        IModelSelectList SelectValue(params object[] selectedValues);
        IModelSelectList Sort();
        IModelSelectList<T> Select<T>(params T[] models);
        IModelSelectList<T> As<T>();
    }

    public interface IModelSelectList<T> : IModelSelectList
    {
        IModelSelectList<T> Select(params T[] models);
        Func<T, object> TextSelector { get; set; }
        Func<T, object> ValueSelector { get; }
    }
}

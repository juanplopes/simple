using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using BasicLibrary.Common;

namespace BasicLibrary.Persistence
{
    //[Serializable]
    //public abstract class PageController<S,PType, TType,T> : S,IDisposable
    //    where T : PageController<S,T>, new()
    //    where S : GenericInstanceState<S,PType, TType>
    //{
    //    protected string CurrentPage { get; set; }
    //    public SafeDictionary<string, object> UserData { get; set; }
    //    [NonSerialized]
    //    public Page ControlledPage;
        
    //    public abstract string InitialPage { get; }

    //    public Stack<string> NavigatedPagesStack { get; set; }

    //    public void NavigateBack()
    //    {
    //        NavigatedPagesStack.Pop();
    //        string last = NavigatedPagesStack.Pop();
    //        Navigate(last);
    //    }

    //    public static T Get(int id, Page page)
    //    {
    //        T control = Get(id);
    //        if (control.CurrentPage == null) control.CurrentPage = control.InitialPage;
    //        if (control.UserData == null) control.UserData = new SafeDictionary<string, object>();
    //        if (control.NavigatedPagesStack == null) control.NavigatedPagesStack = new Stack<string>();
    //        control.ControlledPage = page;
    //        return control;
    //    }

    //    public Q GetData<Q>(string key)
    //    {
    //        return (Q)UserData[key];
    //    }

    //    public void SetData(string key, object value)
    //    {
    //        UserData[key] = value;
    //    }


    //    public void Navigate(string pageUrl)
    //    {
    //        CurrentPage = pageUrl;
    //        NavigatedPagesStack.Push(CurrentPage);
    //        RedirectToCurrentPage();
    //    }
    //    protected string GetNoQueryPath(string url)
    //    {
    //        if (url == null) return null;
    //        return url.Split('?')[0];
    //    }
    //    public void RedirectToCurrentPage()
    //    {
    //        ControlledPage.Response.Redirect(ControlledPage.ResolveUrl(CurrentPage));
    //    }
    //    public void Ensure()
    //    {
    //        string lstrUri1 = GetNoQueryPath(ControlledPage.ResolveUrl(CurrentPage));
    //        string lstrUri2 = GetNoQueryPath(ControlledPage.ResolveUrl(ControlledPage.Request.Url.PathAndQuery));
    //        if (!lstrUri1.Equals(lstrUri2, StringComparison.InvariantCultureIgnoreCase))
    //        {
    //            RedirectToCurrentPage();
    //        }
    //    }

    //    public override void Init()
    //    {
    //        base.Init();
    //        NavigatedPagesStack = new Stack<string>();
    //    }

    //    #region IDisposable Members

    //    public override void Dispose()
    //    {
    //        this.Persist();
    //    }

    //    #endregion
    //}
}

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.PageTitle("Welcome", "Select an option below.") %>
    <div class="home_actions">
        <div class="jsClickable home_action">
            <a href='http://simpledotnet.googlecode.com' class="home_action"></a>
            <h3>
                Where do I go next?</h3>
            <p>
                Follow these instructions to get your project up and running quickly.</p>
        </div>
        <div class="jsClickable home_action">
            <a href='http://simpledotnet.googlecode.com' class="home_action"></a>
            <h3>
                How do I...?</h3>
            <p>
                Check this FAQ section that answers most of questions you might ask.</p>
        </div>
        <div class="jsClickable home_action">
            <a href='http://simpledotnet.googlecode.com' class="home_action"></a>
            <h3>
                Can I help?</h3>
            <p>
                Made something useful? Send us a patch request and see your code shipped in the
                next Simple.Net version.</p>
        </div>
        <div class="jsClickable home_action">
            <a href='http://simpledotnet.googlecode.com' class="home_action"></a>
            <h3>
                More people should know about this...</h3>
            <p>
                Spread the word. Talk with your friends about Simple.Net and agile web development.
                We count on you.</p>
        </div>
    </div>
    <h3>
        Controllers</h3>
    <ul>
        <%  foreach (var url in ViewData["Urls"] as IEnumerable<string>)
            { %>
        <li><a href='<%= Url.RouteUrl(new { controller = url }) %>'>
            <%= url %></a></li>
        <%} %>
    </ul>
</asp:Content>

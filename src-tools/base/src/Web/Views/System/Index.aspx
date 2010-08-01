<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Simple.Patterns.TaskRunner+Result>>" %>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.Stylesheet("system_check.css") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.PageTitle("System check", "System functions status") %>
    <% foreach (var item in Model)
       { %>
    <div class="system_check_<%=item.ResultTypeTag%>">
        <span><strong>
            <%=Html.Encode(item.Description) %></strong></span>
        <div>
            <%=Html.Encode(item.Message) %></div>
    </div>
    <% } %>
</asp:Content>

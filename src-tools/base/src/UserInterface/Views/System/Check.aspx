<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Simple.Patterns.TaskRunner+Result>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Check
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SystemCheck</h2>

    <% foreach (var item in Model) { %>
        <div class="system_check_<%=item.ResultTypeTag%>">
            <span><strong><%=Html.Encode(item.Description) %></strong></span>
            <div><%=Html.Encode(item.Message) %></div>
        </div>
    <% } %>
</asp:Content>


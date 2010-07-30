<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.PageTitle("Welcome", "Select an option below.") %>
    <a href='<%= Url.Action("Index", "WorkScripts") %>' class="home_action action_work_script">
        <h3>Gerenciar programas de trabalho.</h3>
        <p>Insira, remova ou modifique os programas de trabalho cadastrados.</p>
    </a>
    <a href='<%= Url.Action("Index", "AnswerTypes") %>' class="home_action action_work_script">
        <h3>Gerenciar tipos de questões.</h3>
        <p>Insira, remova ou modifique os tipos de questões cadastrados.</p>
    </a>
</asp:Content>

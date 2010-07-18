<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="http_error">
        <div class="default">
            <h1>
                <%= Html.Encode(ViewData["code"]) %></h1>
            <span>
                <%= Html.Encode(ViewData["message"]) %></span>
        </div>
        <% if (Request.IsLocal)
           { %>
        <div class="stacktrace" style="display: none">
            <pre><%= Html.Encode(ViewData["stacktrace"]) %></pre>
        </div>
        <a href="#" id="show_stacktrace">toggle stacktrace</a>
        <%} %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $('#show_stacktrace').click(function() {
                $('.http_error .stacktrace').slideToggle();
                $('.http_error .default').slideToggle();
                return false;
            });
        });
    </script>

</asp:Content>

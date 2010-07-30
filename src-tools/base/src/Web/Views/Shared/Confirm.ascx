<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="confirm">
    <% using (Html.BeginForm())
       { %>
    <span>
        <%= ViewData["confirm-message"] %></span>
    <div class="buttons">
        <%= Html.Submit("yes") %>
        <a href="javascript:$.fancybox.close()" class="button">no</a>
    </div>
    <% } %>
</div>

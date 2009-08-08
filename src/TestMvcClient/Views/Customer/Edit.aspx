<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Simple.Tests.SampleServer.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Id">Id:</label>
                <%= Html.Hidden("Id") %>
                <%= Html.ValidationMessage("Id", "*") %>
            </p>
            <p>
                <label for="CompanyName">CompanyName:</label>
                <%= Html.TextBox("CompanyName", Model.CompanyName) %>
                <%= Html.ValidationMessage("CompanyName", "*") %>
            </p>
            <p>
                <label for="ContactName">ContactName:</label>
                <%= Html.TextBox("ContactName", Model.ContactName) %>
                <%= Html.ValidationMessage("ContactName", "*") %>
            </p>
            <p>
                <label for="ContactTitle">ContactTitle:</label>
                <%= Html.TextBox("ContactTitle", Model.ContactTitle) %>
                <%= Html.ValidationMessage("ContactTitle", "*") %>
            </p>
            <p>
                <label for="Address">Address:</label>
                <%= Html.TextBox("Address", Model.Address) %>
                <%= Html.ValidationMessage("Address", "*") %>
            </p>
            <p>
                <label for="City">City:</label>
                <%= Html.TextBox("City", Model.City) %>
                <%= Html.ValidationMessage("City", "*") %>
            </p>
            <p>
                <label for="Region">Region:</label>
                <%= Html.TextBox("Region", Model.Region) %>
                <%= Html.ValidationMessage("Region", "*") %>
            </p>
            <p>
                <label for="PostalCode">PostalCode:</label>
                <%= Html.TextBox("PostalCode", Model.PostalCode) %>
                <%= Html.ValidationMessage("PostalCode", "*") %>
            </p>
            <p>
                <label for="Country">Country:</label>
                <%= Html.TextBox("Country", Model.Country) %>
                <%= Html.ValidationMessage("Country", "*") %>
            </p>
            <p>
                <label for="Phone">Phone:</label>
                <%= Html.TextBox("Phone", Model.Phone) %>
                <%= Html.ValidationMessage("Phone", "*") %>
            </p>
            <p>
                <label for="Fax">Fax:</label>
                <%= Html.TextBox("Fax", Model.Fax) %>
                <%= Html.ValidationMessage("Fax", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>


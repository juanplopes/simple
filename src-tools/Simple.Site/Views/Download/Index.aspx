<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Download</h1>
    <p>
        Simple.Net is continuously built from source. You can always find the latest version
        at <a href="http://teamcity.codebetter.com/viewLog.html?buildTypeId=bt219&buildId=lastSuccessful&tab=artifacts&guest=1">
            Last Successful build on TeamCity</a>.</p>

    <script type="text/javascript" src="http://teamcity.codebetter.com/externalStatus.html?js=1&projectId=project87&withCss=1&guest=1"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
        "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
    <title>Simple.Net framework by Living Consultoria. Develop great web applications with
        little effort.</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8;FF=3;OtherUA=4" />
    <%= Html.Stylesheet("simple.css") %>
    <%= Html.Stylesheet("nivo-slider.css") %>
    <%= Html.Stylesheet("custom-nivo-slider.css")%>
    <%= Html.Script("jquery-1.4.1.min.js") %>
    <%= Html.Script("jquery.nivo.slider.pack.js")%>

    <script type="text/javascript">
        $(window).load(function() {
            $('.slider').nivoSlider({
                effect: 'fold'
            });
        });
    </script>

</head>
<body>
    <div class="container">
        <div class='header'>
            <a href='http://www.livingnet.com.br' class='logo'></a>
            <ul>
                <li>
                    <%= Html.ActionLink("contributions", "Contributions", "Project") %></li>
                <li>
                    <%= Html.ActionLink("license", "License", "Project") %></li>
                <li>
                    <%= Html.ActionLink("download", "Index", "Download")%></li>
            </ul>
        </div>
        <div class='bar'>
            <div class="content">
                <div class="slider">
                    <%= Html.Image("new-project.png", "").With("title", "Easy project setup")  %>
                    <%= Html.Image("default-layout.png", "").With("title", "Default web interface") %>
                    <%= Html.Image("migrations.png", "").With("title", "Active database management") %>
                </div>
                <div class="text">
                    <h1>
                        Simple</h1>
                    <span class="code">web.Dev.Made(easy)</span>
                    <%= Html.ActionLink(" ", "Latest", "Download", null, new { @class = "download" }) %>
                </div>
            </div>
        </div>
        <div class='items'>
            <div class='item with_separator'>
                <h1>
                    Easy project setup</h1>
                <p>
                    Creating a new project using Simple is as simple as a double click. With the new
                    project template fixture, you can starting using it with no work at all.
                </p>
                <%= Html.ActionLink("read more", "QuickStart", "Documentation")%>
            </div>
            <div class='item with_separator'>
                <h1>
                    Painless web development</h1>
                <p>
                    Using the MVC pattern, Simple.Net combines the most powerful features that allows
                    creating and maintaining web applications to be easier than ever.
                </p>
                <%= Html.ActionLink("read more", "Patterns", "Documentation")%>
            </div>
            <div class='item '>
                <h1>
                    Great open-source libraries</h1>
                <p>
                    Simple is built upon the greatest open-source libraries, providing the best support
                    from community for the core features of the framework.
                </p>
                <%= Html.ActionLink("read more", "Libraries", "Documentation")%>
            </div>
        </div>
    </div>
</body>
</html>

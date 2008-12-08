<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Portal.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Layout Preview</title>
    <link href="CSS/Header.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Main.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.treeview.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Buttons.css" rel="stylesheet" type="text/css" />
    <link href="CSS/FormView.css" rel="stylesheet" type="text/css" />

    <script src="JScript/jQuery.js" type="text/javascript"></script>

    <script src="JScript/jquery_helper.js" type="text/javascript"></script>

    <script src="JScript/jquery_maskEdit.js" type="text/javascript"></script>

    <script src="JScript/jquery.treeview.min.js" type="text/javascript"></script>

    <script src="JScript/jquery-ui-personalized-1.6rc2.js" type="text/javascript"></script>

    <%
        if (false)
        {
    %>

    <script src="JScript/jquery_vsdoc.js" type="text/javascript"></script>

    <%
        }

    %>
</head>
<body>

    <script type="text/javascript">
    $(document).ready(
    function() {
    $("#LeftColumnController").click(
    function() {
        HideShow(".LeftColumn");
        ChangeImage("#leftImage", $("#leftImage").attr("src"));
    });
    $("#RightColumnController").click(
    function() {
        HideShow(".RightColumn");
        ChangeImage("#rightImage", $("#rightImage").attr("src"));
    });
    $("#LinkPreferencias").draggable();
    $(".MascaraCPF").mask("999.999.999-99");
    Resize();
    $(window).resize(function() {
        Resize();
    });

    // Menu TreeView
    $("#menu").treeview();
});

function ChangeImage(img, src) {
    if (src == "Imagens/SetaEsquerda.png") {
        $(img).attr("src", "Imagens/SetaDireita.png");
    }
    else {
        $(img).attr("src", "Imagens/SetaEsquerda.png");
    }
}

function HideShow(selector) {
    $(selector).toggle();
    Resize();
}
function Resize() {
    // Configuração Dinâmica do tamanho do WebSite        
    total = parseInt($("#Top").css("width"));
    diff = parseInt($("#TopBannerLeft").css("width")) + parseInt($("#TopBannerRight").css("width"));
    sub = total - diff;
    $("#TopBannerCenter").css("width", sub);

    // Caixa abaixo do Logo, com os Dados de seção do usuário
    position = $("#TopBannerRight").position();
    $("#DadosUsuario").css("top", 82 + position.top); // 82 aqui, é um valor da altura menos o retangulo colorido
    // Caixa flutuante de preferências
    pref = parseInt($("#LinkPreferencias").width());
    $("#LinkPreferencias").css("left", sub - pref - 12); // 12 Para não ficar muito colado com o Logo da Globo
    $("#LinkPreferencias").css("top", 5); // 5 É o Valor do padding
    // Organizar a Exibição em 3 Colunas Dinamicamente
    total = parseInt($("#Content").width());
    esq = 0;
    dir = 0;
    if ($(".LeftColumn").is(":visible")) {
        esq = parseInt($(".LeftColumn").width());
    }

    if ($(".RightColumn").is(":visible")) {
        dir = parseInt($(".RightColumn").width());
    }
    diff = esq + dir;
    sub = total - diff;
    $(".CenterColumn").width(sub - 2); // 2 É o valor das bordas !
    // Organizar os Controles de Exibição das Colunas
    $("#LeftColumnController").width(sub / 2);
    $("#RightColumnController").width((sub / 2) - 2); // Problemas entre arredondamento de Real pra Int
}
    </script>

    <form id="form1" runat="server">
    <div id="Tudo">
        <div id="Top">
            <div id="TopBannerLeft">
            </div>
            <div id="TopBannerCenter">
                <div id="LinkPreferencias">
                    <a href="#">
                        <img src="Imagens/house.png" alt="Home" />&nbsp;Home</a>&nbsp;&nbsp;&nbsp; <a href="#">
                            <img src="Imagens/cog.png" alt="Preferências" />&nbsp;Preferências</a>
                </div>
            </div>
            <div id="TopBannerRight">
            </div>
            <div id="DadosUsuario">
                Lincoln Quinan Junior&nbsp;&nbsp;&nbsp;<a href="#">Sair</a>
            </div>
        </div>
        <div id="Content">
            <div class="LeftColumn">
                <ul id="menu" class="filetree">
                    <li><span class="folder">Whatever</span>
                        <ul>
                            <li><span class="file">Item de Menu</span></li>
                        </ul>
                    </li>
                    <li><span class="folder">Pasta</span>
                        <ul>
                            <li><span class="folder">Organizar</span>
                                <ul id="folder21">
                                    <li><span class="file">Especial</span></li>
                                    <li><span class="file">Descartar</span></li>
                                </ul>
                            </li>
                            <li><span class="file">Anotações</span></li>
                        </ul>
                    </li>
                    <li class="closed"><span class="folder">Hidden</span>
                        <ul>
                            <li><span class="file">Alterações</span></li>
                        </ul>
                    </li>
                    <li><span class="file">Info</span></li>
                </ul>
            </div>
            <div class="CenterColumn">
                <div id="ColumnController">
                    <div id="LeftColumnController">
                        <img id="leftImage" alt="Fechar" src="Imagens/SetaEsquerda.png" /></div>
                    <div id="RightColumnController">
                        <img id="rightImage" alt="Fechar" src="Imagens/SetaDireita.png" /></div>
                </div>
                <center>
                    <asp:FormView ID="frmCadastro" runat="server" DefaultMode="ReadOnly" HeaderText="Cadastro de Prestadores de Serviços">
                        <ItemTemplate>
                            <label class="FormView" for="txtNome">
                                Nome:
                                <asp:TextBox ID="txtNome" Text="<%# Bind('Nome') %>" runat="server"></asp:TextBox>
                            </label>
                            <label class="FormView" for="txtSobrenome">
                                Sobrenome:
                                <asp:TextBox ID="txtSobrenome" Text="<%# Bind('Sobrenome') %>" runat="server"></asp:TextBox>
                            </label>
                            <label class="FormView" for="txtCPF">
                                CPF:
                                <asp:TextBox CssClass="MascaraCPF" ID="txtCPF" Text="<%# Bind('CPF') %>" runat="server"></asp:TextBox>
                            </label>
                            <label class="FormView" for="txtEndereco1">
                                Endereço:
                                <asp:TextBox ID="txtEndereco1" runat="server" Text="<%# Bind('Endereco1') %>"></asp:TextBox>
                            </label>
                            <label class="FormView" for="txtEndereco2">
                                Complemento:
                                <asp:TextBox ID="txtEndereco2" runat="server" Text="<%# Bind('Endereco2') %>"></asp:TextBox>
                            </label>
                            <label class="FormView FormViewBreakLine" for="rblSexo">
                                Sexo:
                                <asp:RadioButtonList ID="rblSexo" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Feminino" Value="F"></asp:ListItem>
                                </asp:RadioButtonList>
                            </label>
                            <label class="FormView" for="txtPais">
                                País:
                                <asp:TextBox ID="txtPais" Text="<%# Bind('Pais') %>" runat="server"></asp:TextBox>
                            </label>
                            <label class="FormView" for="cboEstado">
                                Estado:
                                <asp:DropDownList ID="cboEstado" runat="server">
                                    <asp:ListItem>Escolha um estado ...</asp:ListItem>
                                    <asp:ListItem Text="Amazonas" Value="AM"></asp:ListItem>
                                    <asp:ListItem Text="Pará" Value="PA"></asp:ListItem>
                                    <asp:ListItem Text="Mato Grosso" Value="MT"></asp:ListItem>
                                    <asp:ListItem Text="Minas Gerais" Value="MG"></asp:ListItem>
                                    <asp:ListItem Text="Bahia" Value="BA"></asp:ListItem>
                                    <asp:ListItem Text="Mato Grosso do Sul" Value="MS"></asp:ListItem>
                                    <asp:ListItem Text="Goiás" Value="GO"></asp:ListItem>
                                    <asp:ListItem Text="Maranhão" Value="MA"></asp:ListItem>
                                    <asp:ListItem Text="Rio Grande do Sul" Value="RS"></asp:ListItem>
                                    <asp:ListItem Text="Tocantins" Value="TO"></asp:ListItem>
                                    <asp:ListItem Text="Piauí" Value="PI"></asp:ListItem>
                                    <asp:ListItem Text="São" Paulo Value="SP"></asp:ListItem>
                                    <asp:ListItem Text="Rondônia" Value="RO"></asp:ListItem>
                                    <asp:ListItem Text="Roraima" Value="RR"></asp:ListItem>
                                    <asp:ListItem Text="Paraná" Value="PR"></asp:ListItem>
                                    <asp:ListItem Text="Acre" Value="AC"></asp:ListItem>
                                    <asp:ListItem Text="Ceará" Value="CE"></asp:ListItem>
                                    <asp:ListItem Text="Amapá" Value="AP"></asp:ListItem>
                                    <asp:ListItem Text="Pernambuco" Value="PE"></asp:ListItem>
                                    <asp:ListItem Text="Santa Catarina" Value="SC"></asp:ListItem>
                                    <asp:ListItem Text="Paraíba" Value="PB"></asp:ListItem>
                                    <asp:ListItem Text="Rio Grande do Norte" Value="RN"></asp:ListItem>
                                    <asp:ListItem Text="Espírito Santo" Value="ES"></asp:ListItem>
                                    <asp:ListItem Text="Rio de Janeiro" Value="RJ"></asp:ListItem>
                                    <asp:ListItem Text="Alagoas" Value="AL"></asp:ListItem>
                                    <asp:ListItem Text="Sergipe" Value="SE"></asp:ListItem>
                                    <asp:ListItem Text="Distrito Federal" Value="DF"></asp:ListItem>
                                </asp:DropDownList>
                            </label>
                            <label class="FormView" for="txtCidade">
                                Cidade:
                                <asp:TextBox ID="txtCidade" Text="<%# Bind('Cidade') %>" runat="server"></asp:TextBox>
                            </label>
                            <div class="FormButtons">
                                <span class="button">
                                    <asp:Button ID="btnOK" runat="server" Text="Salvar" />
                                </span><span class="button">
                                    <asp:Button ID="btnCancelar" OnClientClick="HideShow('#frmCadastro');return false;" runat="server" Text="Cancelar" />
                                </span>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>
                </center>
            </div>
            <div class="RightColumn">
                <div class="RightColumnHeader">
                    Filtros de Busca:
                </div>
                <div class="SearchButtons">
                    <span class="button">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" 
                        onclick="btnSearch_Click" />
                    </span><span class="button">
                        <asp:Button ID="btnClear" OnClientClick="return false;" runat="server" Text="Limpar" />
                    </span>
                </div>
            </div>
        </div>
        <div id="Bottom">
            CopyRight 2008 Living Consultoria e Sistemas&nbsp;&nbsp;
        </div>
    </div>
    </form>
</body>
</html>

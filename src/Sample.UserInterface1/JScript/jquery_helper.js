/// <reference path="jquery_vsdoc.js" />
/// <reference path="jquery_param.js" />
var Valid = true;

function OpenPopUp(botao, panel, paneldrag, speed) {
    /// <summary>Registra a abertura da Pop pelo clique de um botão.</summary>
    /// <param name="botao" type="string">Nome do botão que dispara a abertura do PopUp.</param>
    /// <param name="panel" type="string">Nome do panel que é a PopUp.</param>
    /// <param name="paneldrag" type="string">Nome do panel que possui o evento de Drag.</param>
    /// <param name="speed" type="string">Velocidade de abertura da PopUp (slow, fast ou vazio).</param>
    $("[@id*=" + botao + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + panel + "]").each(function(z) {
                var Panel = $(this);
                
                var objIFrame = document.createElement('iframe');
                $(objIFrame).width(defaultWidth);
                $(objIFrame).height(defaultHeight);
                $(objIFrame).addClass('iframe');
                objIFrame.scrolling="no";
                objIFrame.frameBorder = "0";
                objIFrame.id = this.id + "_Iframe";
                Panel.before(objIFrame);
                
                Panel.show(speed);
                Panel.css('left', (defaultWidth - this.scrollWidth) / 2 + 'px'); //1003
                Panel.css('top', (defaultHeight - this.scrollHeight) / 2 + 'px');
                Panel.css('position', 'absolute');

                $("[@id*=" + paneldrag + "]").each(function(n) {
                    Panel.draggable({
                        handle: $(this)
                    });
                });
            });

        });
    });
}

function ShowPopUp(panel, paneldrag) {
    /// <summary>Registra a abertura da Pop sem a necessidade de um clique de botão. Utilizado por retornos de servidor</summary>
    /// <param name="panel" type="string">Nome do panel que é a PopUp.</param>
    /// <param name="paneldrag" type="string">Nome do panel que possui o evento de Drag.</param>
     
    $("[@id*=" + panel + "]").each(function(z) {
        var Panel = $(this);
        
        var objIFrame = document.createElement('iframe');
        $(objIFrame).width(defaultWidth);
        $(objIFrame).height(defaultHeight);
        $(objIFrame).addClass('iframe');
        objIFrame.scrolling="no";
        objIFrame.frameBorder = "0";
        objIFrame.id = this.id + "_Iframe";
        Panel.before(objIFrame);
        
        Panel.show();
        Panel.css('left', (defaultWidth - this.scrollWidth) / 2 + 'px');
        Panel.css('top', (defaultHeight - this.scrollHeight) / 2 + 'px');
        Panel.css('position', 'absolute');

        $("[@id*=" + paneldrag + "]").each(function(n) {
            Panel.draggable({
                handle: $(this)
            });
        });
    });
}

function HidePopUp(botao, panel, speed) {
    /// <summary>Registra o evento de esconder  a Pop pelo clique de um clique de um botão.</summary>
    /// <param name="botao" type="string">Nome do botão que dispara a abertura do PopUp.</param>
    /// <param name="panel" type="string">Nome do panel que é a PopUp.</param>
    /// <param name="speed" type="string">Velocidade de abertura da PopUp (slow, fast ou vazio).</param>
    $("[@id*=" + botao + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + panel + "]").each(function(z) {
                $(this).hide(speed);
            });
            $("[@id*=" + panel + "_Iframe]").each(function(z) {
                this.parentNode.removeChild(this);
            });
        });
    });
}

function HidePopUpDirect(panel, speed) {
    /// <summary>Registra o evento de esconder a Pop sem a necessidade do clique de um clique de um botão. Utilizado no retorno do servidor.</summary>
    /// <param name="panel" type="string">Nome do panel que é a PopUp.</param>
    /// <param name="speed" type="string">Velocidade de abertura da PopUp (slow, fast ou vazio).</param>
    $("[@id*=" + panel + "]").each(function(z) {
        $(this).hide(speed);
    });
    $("[@id*=" + panel + "_Iframe]").each(function(z) {
                this.parentNode.removeChild(this);
            });
}

function HidePanelFilters(img, pnlsnippet) {
    ///<summary>Registra a chamada para a Função para esconder o Panel e mudar o botão do Snippet</summary>
    /// <param name="img" type="string">ImageButton que dispara o evento.</param>
    /// <param name="pnlsnippet" type="string">Panel que recebe o efeito aparece/desaparece.</param>
    $("[@id*=" + img + "]").each(function(i) {
        $(this).click(function() {
            var ImgSnippet = this;

            $("[@id*=" + pnlsnippet + "]").each(function(j) {
                var pnl = this;
                var isVisible = $(pnl).is(':visible');
                if (isVisible) {
                    HideDdl(pnl);
                    $(pnl).hide("slow");
                    ImgSnippet.src = window.dhx_globalImgPath + "fora.gif";
                }
                else {
                    $(pnl).show("slow");
                    ImgSnippet.src = window.dhx_globalImgPath + "dentro.gif";
                    ShowDdl(pnl);
                }
            });
        });
    });
}

function HideDdl(parent) {
    ///<summary>Função Interna.</summary>
    if (parent.tagName == "SPAN" && parent.className == "dropdown") {
        $(parent).hide("slow");
    }
    for (var count = 0; count < parent.children.length; count++) {
        HideDdl(parent.children[count]);
    }
}

function ShowDdl(parent) {
    ///<summary>Função Interna.</summary>
    if (parent.tagName == "SPAN" && parent.className == "dropdown") {
        $(parent).show("slow");
    }
    for (var count = 0; count < parent.children.length; count++) {
        ShowDdl(parent.children[count]);
    }
}

function ChangePanel(sender, target, speed) {
    ///<summary>Função para esconder o Panel pelo LinkButton de filtros.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="target" type="string">Panel que recebe o efeito aparece/desaparece.</param>
    /// <param name="speed" type="string">Velocidade do efeito.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + target + "]").each(function(j) {
                var isVisible = $(this).is(':visible');
                if (isVisible) {
                    HideDdl(this);
                    $(this).hide(speed);
                }
                else {
                    $(this).show(speed);
                    ShowDdl(this);
                }
            });
        });
    });
}

function Change2Panels(sender, target1, target2, speed) {
    ///<summary>Função para mudar o display 2 Panels.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="target1" type="string">1º Panel que recebe o efeito aparece/desaparece.</param>
    /// <param name="target2" type="string">2º Panel que recebe o efeito aparece/desaparece.</param>
    /// <param name="speed" type="string">Velocidade do efeito.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            var t1;
            var t2;
            $("[@id*=" + target1 + "]").each(function(j) {
                t1 = this;
            });
            $("[@id*=" + target2 + "]").each(function(k) {
                t2 = this;
            });

            var isVisible = $(t1).is(':visible');
            if (isVisible) {
                $(t1).hide(speed);
                $(t2).show(speed);
            }
            else {
                $(t2).hide(speed);
                $(t1).show(speed);
            }
        });
    });
}

function Change4Panels(sender, target1, target2, target3, target4, speed) {
    ///<summary>Função para mudar o display 4 Panels. O 1º Panel é ligado ao 2º, e o 3º ligado ao 4º.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="target1" type="string">1º Panel que recebe o efeito aparece/desaparece.</param>
    /// <param name="target2" type="string">2º Panel que recebe o efeito aparece/desaparece.</param>
    /// <param name="target3" type="string">3º Panel que recebe o efeito aparece/desaparece.</param>
    /// <param name="target4" type="string">4º Panel que recebe o efeito aparece/desaparece.</param>
    /// <param name="speed" type="string">Velocidade do efeito.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            RemoveValidators();
            var t1;
            var t2;
            var t3;
            var t4;
            $("[@id*=" + target1 + "]").each(function(j) {
                t1 = this;
            });
            $("[@id*=" + target2 + "]").each(function(k) {
                t2 = this;
            });
            $("[@id*=" + target3 + "]").each(function(j) {
                t3 = this;
            });
            $("[@id*=" + target4 + "]").each(function(k) {
                t4 = this;
            });

            var isVisible = $(t1).is(':visible');
            if (isVisible) {
                $(t1).hide(speed);
                $(t2).hide(speed);
                $(t3).show(speed);
                $(t4).show(speed);
            }
            else {
                $(t1).show(speed);
                $(t2).show(speed);
                $(t3).hide(speed);
                $(t4).hide(speed);
            }
        });
    });
}

function CheckBox(sender, target) {
    ///<summary>Função para selecionar checkboxs.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="target" type="string">Objeto (checkbox) que será selecionado.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + target + "]").each(function(k) {
                this.checked = true;
                this.fireEvent("onclick");
            });
        });
    });
}

function ShowPanel(sender, target, speed) {
    /// <summary>Função para exibir um Panel.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="panel" type="string">Panel que será exibido.</param>
    /// <param name="speed" type="string">Velocidade do efeito.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + target + "]").each(function(j) {
                $(this).show(speed);
            });
        });
    });
}

function ShowPanelDirect(target, speed) {
    /// <summary>Função para exibir um Panel.</summary>
    /// <param name="target" type="string">Panel que será exibido.</param>
    /// <param name="speed" type="string">Velocidade do efeito.</param>
    $("[@id*=" + target + "]").each(function(j) {
        $(this).show(speed);
    });
}
function HidePanel(sender, target, speed) {
    /// <summary>Função para esconder um Panel.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="panel" type="string">Panel que será escondido.</param>
    /// <param name="speed" type="string">Velocidade do efeito.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + target + "]").each(function(j) {
                $(this).hide(speed);
            });
        });
    });
}

function HidePanelDirect(target, speed) {
    /// <summary>Função para esconder um Panel.</summary>
    /// <param name="target" type="string">Panel que será escondido.</param>
    /// <param name="speed" type="string">Velocidade do efeito.</param>
    $("[@id*=" + target + "]").each(function(j) {
        $(this).hide(speed);
    });
}

function AutoComplete(ddl) {
    /// <summary>Função para transformar uma combo normal em autocomplete com filtro.</summary>
    /// <param name="ddl" type="string">Padrão do nome da Drop que será transformada.</param>
    $("[@id*=" + ddl + "]").each(function(i) {
        if (this.style.display == "none" || this.tagName != "SELECT") return;
        var z = dhtmlXComboFromSelect(this);
        z.enableFilteringMode(true);
        z.disable(this.disabled);
    });
}

function ClearFields(botao, parent, divclear) {
    /// <summary>Função para limpar campos.</summary>
    /// <param name="botao" type="string">Objeto que irá disparar o evento.</param>
    /// <param name="parent" type="string">Panel que contém os objetos a serem limpos.</param>
    $("[@id*=" + botao + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + parent + "]").each(function(i) {
                ClearFilterHelper(this);
            });
            if (divclear)
            {
                $("[@id*=" + divclear + "]").each(function(j) { 
                    this.innerHTML = "";
                });
            }
            RemoveValidators();
        });
    });
}

function ClearFilterHelper(parent) {
    /// <summary>Funcão Interna.</summary>
    switch (parent.tagName) {
    case "INPUT":
        if (parent.type == "text") {
            parent.value = "";
        }
        else if (parent.type == "checkbox") {
            parent.checked = false;
        }
        else if (parent.type == "radio") {
            parent.checked = false;
        }
        else if (parent.type == "hidden") {
            parent.value == "";
        }
        break;

    case "TEXTAREA":
        parent.value = "";
        break;

    case "SELECT":
        if (parent.length > 0)
            parent[0].selected = true;
        break;
    }

    if (parent.nodeType == 1) {
        for (var count = 0; count < parent.children.length; count++) {
            ClearFilterHelper(parent.children[count]);
        }
    }
}

function ConfigureMask(target) {
    /// <summary>Função para configurar máscaras em campos.</summary>
    /// <param name="target" type="string">Objeto que irá receber a máscara.</param>
    $("[@id*=" + target + "]").each(function(i) {
        if (this.Mask) {
            if (this.MaskType == "Date") {
                $(this).mask("99/99/9999");
            }
            else if (this.MaskType == "Integer") {
                var an;
                if (this.AllowNegative) {
                    an = true;
                    this.maxLength = this.maxLength + 1;
                }
                else {
                    an = false;
                }

                $(this).numeric("µ", an);
            }
            else if (this.MaskType == "Hour") {
                $(this).mask("99:99");
            }
            else if (this.MaskType.substring(0, 7) == "Decimal") {
                var n = this.MaskType.lastIndexOf(",");
                if (n == 0) {
                    n = this.MaskType.lastIndexOf(")");
                }
                var d = this.MaskType.substr(n + 1, 1);
                var l = this.MaskType.substring(8, n);

                $(this).maskMoney({
                    symbol: "",
                    decimal: ",",
                    thousands: "",
                    precision: Number(d),
                    integer: Number(l)
                });
            }
            else {
                $(this).mask(this.MaskType);
            }
        }
    });
}

function HoverMenu(target, panel, div) {
    /// <summary>Função para configurar CliqueHoverMenu em grids. O Grid deve estar dentro de uma div que não possua mais nada, apenas o grid.</summary>
    /// <param name="target" type="string">Grid que possue o HoverMenu.</param>
    /// <param name="panel" type="string">Índice da coluna que possue o panel.</param>
    /// <param name="div" type="string">Div na qual a grid está inserida (deve possuir um id).</param>
    var valLeft = 0;
    var valHorScroll = 0;
    var valTop = 0;
    var valVertScroll = 0;
    var valTamHor = 0;
    var valTamVert = 0;

    $("[@id*=" + target + "] tr").each(function(i) {
        var hovermenu = this.children[panel].children[1];

        $(this).mouseover(function(k) {
            $(this).css('background-color', '#e6e6e6');
            $(this).css('cursor', 'hand');
        });

        $(this).mouseout(function(k) {
            $(this).css('background-color', '');
            $(this).css('cursor', 'default');
        });

        if (hovermenu != null) {
            var linha = $(this);

            $(this).click(function(i) {
                $(this).css('background-color', '#C0C0C0');

                $(hovermenu).show();
                $("[@id*=" + div + "]").each(function(h) {
                    valLeft = this.offsetLeft;
                    valHorScroll = this.scrollLeft;
                    valTop = this.offsetTop;
                    valVertScroll = this.scrollTop;
                    valTamHor = this.offsetWidth + valLeft - 20;
                    valTamVert = this.offsetHeight + valTop - 20;
                });

                if (i.pageX + hovermenu.clientWidth > valTamHor) {
                    $(hovermenu).css('left', i.pageX - valLeft + valHorScroll - hovermenu.clientWidth);
                }
                else {
                    $(hovermenu).css('left', i.pageX - valLeft + valHorScroll);
                }

                if (i.pageY + hovermenu.clientHeight > valTamVert) {
                    $(hovermenu).css('top', i.pageY - valTop + valVertScroll - hovermenu.clientHeight);
                }
                else {
                    $(hovermenu).css('top', i.pageY - valTop + valVertScroll);
                }
            });

            $(hovermenu).mouseover(function(i) {
                linha.css('background-color', '#C0C0C0');
                $(hovermenu).show();
            });

            $(this).mouseout(function(j) {
                $(hovermenu).hide();
                $(this).css('background-color', '');
            });
        }
    });
}

function RemoveValidators() {
    /// <summary>Função interna.</summary>
    $("[@id*=_validator]").each(function(d) {
        this.parentNode.removeChild(this);
    });
}

function CreateValidate(button, target, pnl, resource) {
    /// <summary>Função para configurar a validação de campos.</summary>
    /// <param name="button" type="string">Objeto que dispara a validação.</param>
    /// <param name="target" type="string">Panel que possue os controles a serem validados.</param>
    /// <param name="pnl" type="string">Nome do PopUp que deve ser escondido, caso a validação seja 
    /// feita em um PopUp e se deseje que este seja
    /// fechado se a validação for bem sucedida. Caso não esteje em um PopUp, passar vazio ('').</param>
    /// <param name="resource" type="string">Preencher com a culture que está sendo usada.</param>
    $("[@id*=" + button + "]").each(function(i) {
        $(this).click(function() {
            return Validate(target, pnl, resource);
        });
    });
}

function Validate(target, pnl, resource) {
    /// <summary>Função interna.</summary>
    Valid = true;
    RemoveValidators();
    $("[@id*=" + target + "]").each(function() { (ValidateControls(this, resource));
    });
    if (pnl != "" && Valid) {
        $("[@id*=" + pnl + "]").each(function(pnl) {
            $(this).hide();
        });
        $("[@id*=" + pnl + "_Iframe]").each(function(iframe) {
            this.parentNode.removeChild(this);
        });
    }
    return Valid;
}

function ValidateControls(parent, resource) {
    /// <summary>Função interna.</summary>
    switch (parent.tagName) {
    case "INPUT":
    case "TEXTAREA":
        if ((parent.type == "text" || parent.type == "textarea" || parent.type == "file") && parent.style.display != "none") {
            var objImg = document.createElement('img');
            objImg.src = window.dhx_globalImgPath + "exclamation_.png";
            objImg.id = parent.id + "_img_validator";
            $(objImg).css('margin-top', '-6px').css('margin-left', '2px');
            var TextValidation = "";
            var _valid = true;

            if (parent.Required && Trim(parent.value) == "") {
                TextValidation = ValidatorText(0, resource);
                Valid = false;
                _valid = false;
            }
            if (parent.LRange && (parent.MinLRange != null || parent.MaxLRange != null)) {
                parent.Range = true;
                var TextRange = "";

                if (parent.MinLRange != null) {
                    $("[@id*=" + parent.MinLRange + "]").each(function() {
                        var obj = this;
                        parent.MinRange = obj.value;
                        TextRange = ValidatorText(3, resource) + parent.MinRange + ".";
                    });
                }
                if (parent.MaxLRange != null) {
                    $("[@id*=" + parent.MaxLRange + "]").each(function() {
                        var obj = this;
                        parent.MaxRange = obj.value;
                        if (TextRange != "") {
                            TextRange += "\n";
                        }
                        TextRange = ValidatorText(4, resource) + parent.MaxRange + ".";
                    });
                }
                parent.RangeMessage = TextRange;
            }
            if (parent.Range && (Number(parent.value) < Number(parent.MinRange) || Number(parent.value) > Number(parent.MaxRange)) && parent.RangeMessage != null) {
                if (TextValidation != "") {
                    TextValidation += "\n"
                }

                TextValidation += parent.RangeMessage;

                Valid = false;
                _valid = false;
            }
            if (parent.MinValue != null && parent.value.length < parent.MinValue) {
                if (TextValidation != "") {
                    TextValidation += "\n"
                }

                TextValidation += ValidatorText(1, resource) + parent.MinValue + ".";

                Valid = false;
                _valid = false;
            }
            if (parent.MaxValue != null && parent.value.length > parent.MaxValue) {
                if (TextValidation != "") {
                    TextValidation += "\n"
                }

                TextValidation += ValidatorText(2, resource) + parent.MaxValue + ".";

                Valid = false;
                _valid = false;
            }

            if (!_valid) {
                objImg.alt = TextValidation;

                if (parent.className == "dhx_combo_input") {
                    $(parent.parentNode.parentNode).after(objImg);
                }
                else {
                    $(parent).after(objImg);
                }
            }
        }
        else if (parent.type == "checkbox") {
            //parent.checked = false;
        }
        break;

    case "SELECT":
        //parent[0].selected = true;
        if (parent.Required && parent.style.display != "none") {
            for (var c = 0; c < parent.children.length; c++) {
                if (parent[c].selected) {
                    if (parent[c].value == "") {
                        var objImg = document.createElement('img');
                        objImg.src = window.dhx_globalImgPath + "exclamation_.png";
                        objImg.id = parent.id + "_img_validator";
                        $(objImg).css('margin-top', '-6px');
                        objImg.alt = ValidatorText(0, resource);
                        $(parent).after(objImg);
                        Valid = false;
                    }
                    break;
                }
            }
        }
        break;
    }

    if (parent.nodeType == 1) {
        for (var count = 0; count < parent.children.length; count++) {
            ValidateControls(parent.children[count], resource);
        }
    }
}

function EnableControl(sender, target)
{
    /// <summary>Função para habilitar campos.</summary>
    /// <param name="sender" type="string">Objeto que disparao o evento.</param>
    /// <param name="target" type="string">Objeto(s) que serão habilitados.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + target + "]").each(function(j) {
                this.disabled = false;
            });
        });
    });
}

function DisableControl(sender, target)
{
    /// <summary>Função para desabilitar campos.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="target" type="string">Objeto(s) que serão desabilitados.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + target + "]").each(function(j) {
                this.disabled = true;
            });
        });
    });
}

function ReShowDropDown(sender, target)
{
    /// <summary>Função para reexibir uma dropdown e setar seu autocomplete.</summary>
    /// <param name="sender" type="string">Objeto que dispara o evento.</param>
    /// <param name="target" type="string">Objeto(s) que serão desabilitados.</param>
    $("[@id*=" + sender + "]").each(function(i) {
        $(this).click(function() {
            $("[@id*=" + target + "]").each(function(j) {
                if (this.tagName != "SELECT") return;
                $(this).show();
                var z = dhtmlXComboFromSelect(this);
                z.enableFilteringMode(true);
                z.disable(this.disabled);
            });
        });
    });
}

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}
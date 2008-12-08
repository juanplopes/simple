var defaultWidth = 1003;
var defaultHeight = 600;

function ValidatorText(type, resource) {
    /// <summary>Função para configurar a validação de campos.</summary>
    /// <param name="type" type="string">Tipos de resources 
    /// (0 = Required; 1 = MinValue; 2 = MaxValue; 3 = MinRange; 4 = MaxRange;).</param>
    /// <param name="resource" type="string">Resource recebido da função para escolha de linguagem.</param>
    if (type == 0) {
        if (resource == null || resource == "pt-BR" || resource == "") {
            return (" - Campo Obrigatório.");
        }
        else if (resource == "en-US") {
            return (" - Required Field.");
        }
        else if (resource == "fr-FR") {
            return (" - Campo Obrigatório.");
        }
    }
    else if (type == 1) //MinValue
    {
        if (resource == null || resource == "pt-BR" || resource == "") {
            return (" - Tamanho mínimo: ");
        }
        else if (resource == "en-US") {
            return (" - Min. field length: ");
        }
        else if (resource == "fr-FR") {
            return (" - Tamanho mínimo: ");
        }
    }
    else if (type == 2) //MaxValue
    {
        if (resource == null || resource == "pt-BR" || resource == "") {
            return (" - Tamanho máximo: ");
        }
        else if (resource == "en-US") {
            return (" - Max. field length: ");
        }
        else if (resource == "fr-FR") {
            return (" - Tamanho máximo: ");
        }
    }
    else if (type == 3) //Range Linkado Min
    {
        if (resource == null || resource == "pt-BR" || resource == "") {
            return (" - Valor mínimo: ");
        }
        else if (resource == "en-US") {
            return (" - Min. value: ");
        }
        else if (resource == "fr-FR") {
            return (" - Valor mínimo: ");
        }
    }
    else if (type == 4) //Range Linkado Max
    {
        if (resource == null || resource == "pt-BR" || resource == "") {
            return (" - Valor máximo: ");
        }
        else if (resource == "en-US") {
            return (" - Max. value: ");
        }
        else if (resource == "fr-FR") {
            return (" - Valor máximo: ");
        }
    }
}
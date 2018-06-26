$(document).ready(function () {

    $("#btnAdicionarEmail").click(function () {
        var idCliente = $("input[id='Id']").val();
        if (idCliente == "")
            idCliente = 0;
        var txtEmail = $("#txtEmail").val();
        var tamanhoTabela = $("#tblEmail").find('tbody').find("tr").length;
        var inputIdentificadorEmailClienteId = "<input class='identificadorEmailCliente' type='hidden' value='" + idCliente + "' name='Email[" + tamanhoTabela + "].Cliente.Id' />";
        var inputIdentificadorEmailClienteCPF = "<input class='identificadorEmailClienteCPF' type='hidden' value='' name='Email[" + tamanhoTabela + "].Cliente.CPF' />";
        var inputIdentificadorEmailClienteRG = "<input class='identificadorEmailClienteRG' type='hidden' value='' name='Email[" + tamanhoTabela + "].Cliente.RG' />";
        var inputIdentificadorEmailClienteNome = "<input class='identificadorEmailClienteNome' type='hidden' value='' name='Email[" + tamanhoTabela + "].Cliente.Nome' />";
        $("#tblEmail").find('tbody').append("<tr>" + inputIdentificadorEmailClienteId + inputIdentificadorEmailClienteCPF + inputIdentificadorEmailClienteRG + inputIdentificadorEmailClienteNome + "<input class='identificadorEmail' type='hidden' value='0' name='Email[" + tamanhoTabela + "].Id' />" + "<td class='email' >" + txtEmail + "<input class='email' type='hidden' name='Email[" + tamanhoTabela + "].EnderecoEmail' value='" + txtEmail + "'/></td > <td class='acoes'><a onclick='AlterarEmail(this)' href='#' >Editar</a> | <a onclick='ApagarEmail(this)' href='#'>Apagar</a> </td></tr > ");
        $("#tblEmail").show();
        $("#txtEmail").val('');
    });

    $("#btnAdicionarTelefone").click(function () {
        var idCliente = $("input[id='Id']").val();
        if (idCliente == "")
            idCliente = 0;
        var txtTelefone = $("#txtTelefone").val();
        var tamanhoTabela = $("#tblTelefone").find('tbody').find("tr").length;
        var inputIdentificadorTelefoneClienteId = "<input class='identificadorTelefoneCliente' type='hidden' value='" + idCliente + "' name='Telefone[" + tamanhoTabela + "].Cliente.Id' />";
        var inputIdentificadorTelefoneClienteCPF = "<input class='identificadorTelefoneClienteCPF' type='hidden' value='' name='Telefone[" + tamanhoTabela + "].Cliente.CPF' />";
        var inputIdentificadorTelefoneClienteRG = "<input class='identificadorTelefoneClienteRG' type='hidden' value='' name='Telefone[" + tamanhoTabela + "].Cliente.RG' />";
        var inputIdentificadorTelefoneClienteNome = "<input class='identificadorTelefoneClienteRG' type='hidden' value='' name='Telefone[" + tamanhoTabela + "].Cliente.Nome' />";
        var inputIdentificadortelefone = "<input class='identificadorTelefone' type='hidden' value='0' name='Telefone[" + tamanhoTabela + "].Id' />";

        $("#tblTelefone").find('tbody').append("<tr>" + inputIdentificadorTelefoneClienteId + inputIdentificadorTelefoneClienteCPF + inputIdentificadorTelefoneClienteRG + inputIdentificadorTelefoneClienteNome + inputIdentificadortelefone + "<td class='telefone'> " + txtTelefone + "<input class='numero' type='hidden' value='" + txtTelefone + "' name='Telefone[" + tamanhoTabela + "].Numero'/></td > <td class='tipoTelefone' >" + "Celular<input class='tipoTelefone' type='hidden' value='Celular' name='Telefone[" + tamanhoTabela + "].Tipo'/> " + "</td> <td class='acoes'><a onclick='AlterarTelefone(this)' href='#' >Editar</a> | <a onclick='ApagarTelefone(this)' href='#'>Apagar</a> </td></tr > ");
        $("#tblTelefone").show();
        $("#txtTelefone").val('');
    });

    if ($("#tblEmail tbody>tr").length > 0)
        $("#tblEmail").show();

    if ($("#tblTelefone tbody>tr").length > 0)
        $("#tblTelefone").show();

    /*$("#btnSalvar").click(function () {
        SalvarFormulario();
    })*/


});

function SalvarFormulario() {
    $.ajax({
        url: $("#hdnActionSalvar").val(),
        data: ObterViewModel(),
        type: "POST",
        dataType: "json",
        success: function (result) {
            alert("Dados salvos com sucesso");
        },
        error: function (result) {
            alert("Erro ao salvar os dados");
        }
    });
}

function ObterViewModel() {
    var Cliente = {
        Nome: $("#Nome").val(),
        CPF: $("#CPF").val(),
        RG: $("#RG").val(),
        Email: ObterEmailsCadastrados(),
        Telefone: ObterTelefonesCadastrados()
    }

    return Cliente;
}

function ObterEmailsCadastrados() {
    var emails = [];
    $("#tblEmail").find('tbody').find("tr").each(function () {
        var email = $(this).find("td[class='email']").html();
        emails.push({ EnderecoEmail: email });
    });

    return emails;
}

function ObterTelefonesCadastrados() {
    var telefones = [];
    $("#tblTelefone").find('tbody').find("tr").each(function () {
        var telefone = $(this).find("td[class='telefone']").html();
        var tipoTelefone = $(this).find("td[class='tipoTelefone']").html();

        telefones.push({ Numero: telefone, Tipo: tipoTelefone })
    });

    return telefones;
}

function AlterarEmail(celula) {

    var linha = $(celula).closest("tr");
    var celulaEmail = $(linha).find("td[class='email']");
    var textoEmail = $(celulaEmail).find("input[class='email']").val();
    $(celulaEmail).html("<input id='inputEmail' type='text' value ='" + textoEmail + "'/>");

    var celulaAcoes = $(linha).find("td[class='acoes']");
    $(celulaAcoes).html("<a onclick='SalvarEmail(this)' href='#' >Confirmar</a> | <a onclick='CancelarEmail(this)' href='#'>Cancelar</a> </td>");
}

function SairModoEdicaoEmail(celula) {
    var linha = $(celula).closest("tr");
    var celulaEmail = $(linha).find("td[class='email']");
    var inputEmail = $(celulaEmail).find("input[id='inputEmail']");
    var textoEmail = $(inputEmail).val();
    $(celulaEmail).html(textoEmail + "<input type='hidden' class='email' name=Email[" + 0 + "].EnderecoEmail value='" + textoEmail + "'/>");

    var celulaAcoes = $(linha).find("td[class='acoes']");
    $(celulaAcoes).html("<a onclick='AlterarEmail(this)' href='#' >Editar</a> | <a onclick='ApagarEmail(this)' href='#'>Apagar</a> ");

    IndexarTabelaEmail();
}

function SalvarEmail(celula) {
    SairModoEdicaoEmail(celula);
}

function CancelarEmail(celula) {
    SairModoEdicaoEmail(celula);
}

function ApagarEmail(celula) {

    var linha = $(celula).closest("tr");
    var corpoTabela = $(celula).closest("tbody");
    linha.remove();
    if ($(corpoTabela).find("tr").length == 0)
        $("#tblEmail").hide();
    else {
        IndexarTabelaEmail();
    }

}

function AlterarTelefone(celula) {

    var linha = $(celula).closest("tr");
    var celulaTelefone = linha.find("td[class='telefone']");
    var inputTelefone = $(celulaTelefone).find("input[class='numero']");
    var textoTelefone = $(inputTelefone).val();
    $(celulaTelefone).html("<input id='inputTelefone' type='text' value ='" + textoTelefone + "'/>");

    var celulaTipoTelefone = linha.find("td[class='tipoTelefone']");
    var inputTipoTelefone = $(celulaTipoTelefone).find("input[class='tipoTelefone']");
    var valorTipoTelefone = $(inputTipoTelefone).val();
    $(celulaTipoTelefone).html("<select id='cbTipoTelefone'><option value='Casa'>Casa</option><option value='Celular'>Celular</option></select>")
    $("#cbTipoTelefone").val(valorTipoTelefone);

    var celulaAcoes = linha.find("td[class='acoes']");
    $(celulaAcoes).html("<a onclick='SalvarTelefone(this)' href='#' >Confirmar</a> | <a onclick='CancelarTelefone(this)' href='#'>Cancelar</a> </td>");
}

function SairModoEdicaoTelefone(celula) {
    var linha = $(celula).closest("tr");
    var celulaTelefone = $(linha).find("td[class='telefone']");
    var inputTelefone = $(celulaTelefone).find("input[id='inputTelefone']");
    var textoTelefone = $(inputTelefone).val();
    $(celulaTelefone).html(textoTelefone + "<input type='hidden' class ='numero' value='" + textoTelefone + "' name='Telefone[" + 0 + "].Numero' />");

    var celulaTipoTelefone = $(linha).find("td[class='tipoTelefone']");
    var tipoTelefoneSelecionado = $("#cbTipoTelefone option:selected").val();
    $(celulaTipoTelefone).html(tipoTelefoneSelecionado + "<input type='hidden' class='tipoTelefone' value='" + tipoTelefoneSelecionado + "' name='Telefone[" + 0 + "].Tipo' />");

    var celulaAcoes = $(linha).find("td[class='acoes']");
    $(celulaAcoes).html("<a onclick='AlterarTelefone(this)' href='#' >Editar</a> | <a onclick='ApagarTelefone(this)' href='#'>Apagar</a> ");

    IndexarTabelaTelefone();
}

function SalvarTelefone(celula) {
    SairModoEdicaoTelefone(celula);
}

function CancelarTelefone(celula) {
    SairModoEdicaoTelefone(celula);
}

function ApagarTelefone(celula) {

    var linha = $(celula).closest("tr");
    var corpoTabela = $(linha).closest("tbody");
    linha.remove();
    if ($(corpoTabela).find("tr").length == 0)
        $("#tblTelefone").hide();
    else {
        IndexarTabelaTelefone();
    }

}

function IndexarTabelaTelefone() {
    var int = 0;
    $("#tblTelefone").find("tbody").find("tr").find("td[class='telefone']").each(function () {
        $(this).find("input").each(function () {
            $(this).attr("name", "Telefone[" + int + "].Numero");
            var linha = $(this).closest("tr");
            $(linha).find("input[class='identificadorTelefone']").attr("name", "Telefone[" + int + "].Id");
            $(linha).find("input[class='identificadorTelefoneCliente']").attr("name", "Telefone[" + int + "].Cliente.Id");
            $(linha).find("input[class='identificadorTelefoneClienteCPF']").attr("name", "Telefone[" + int + "].Cliente.CPF");
            $(linha).find("input[class='identificadorTelefoneClienteRG']").attr("name", "Telefone[" + int + "].Cliente.RG");
            $(linha).find("input[class='identificadorTelefoneClienteNome']").attr("name", "Telefone[" + int + "].Cliente.Nome");
        });
        int++;
    });

    int = 0;
    $("#tblTelefone").find("tbody").find("tr").find("td[class='tipoTelefone']").each(function () {
        $(this).find("input").each(function () {
            $(this).attr("name", "Telefone[" + int + "].Tipo");
        });
        int++;
    });
}

function IndexarTabelaEmail() {
    var int = 0;
    $("#tblEmail").find("tbody").find("tr").find("td[class='email']").each(function () {

        $(this).find("input").each(function () {
            $(this).attr("name", "Email[" + int + "].EnderecoEmail");
            var linha = $(this).closest("tr");
            $(linha).find("input[class='identificadorEmail']").attr("name", "Email[" + int + "].Id");
            $(linha).find("input[class='identificadorEmailCliente']").attr("name", "Email[" + int + "].Cliente.Id");
            $(linha).find("input[class='identificadorEmailClienteCPF']").attr("name", "Email[" + int + "].Cliente.CPF");
            $(linha).find("input[class='identificadorEmailClienteRG']").attr("name", "Email[" + int + "].Cliente.RG");
            $(linha).find("input[class='identificadorEmailClienteNome']").attr("name", "Email[" + int + "].Cliente.Nome");
        });
        int++;
    });
}

const API_BASE_URL = "https://localhost:7260/api";
let tiposTelefone = [];

$(document).ready(function () {
    console.log("DOM pronto. Carregando tipos...");
    carregarTiposTelefone().then(() => {
        console.log("Tipos carregados. Carregando clientes...");
        carregarClientes();
    });

    $("#formCliente").submit(function (e) {
        e.preventDefault();
        salvarCliente();
    });
});

function carregarTiposTelefone() {
    return $.get(`${API_BASE_URL}/tipostelefone`, function (data) {
        tiposTelefone = data;
    });
}

function adicionarTelefone(telefone = {}) {
    const index = Date.now();
    const tipoOptions = tiposTelefone.map(t => {
        const selected = telefone.codigoTipoTelefone === t.codigoTipoTelefone ? "selected" : "";
        return `<option value="${t.codigoTipoTelefone}" ${selected}>${t.descricaoTipoTelefone}</option>`;
    }).join("");

    $("#tabelaTelefones tbody").append(`
        <tr data-index="${index}">
            <td><input type="text" class="form-control" value="${telefone.numeroTelefone || ""}" data-field="numeroTelefone"></td>
            <td><input type="text" class="form-control" value="${telefone.operadora || ""}" data-field="operadora"></td>
            <td>
                <select class="form-select" data-field="codigoTipoTelefone">
                    <option value="">Selecione...</option>
                    ${tipoOptions}
                </select>
            </td>
            <td><button type="button" class="btn btn-danger" onclick="removerTelefone(${index})">X</button></td>
        </tr>
    `);
}

function removerTelefone(index) {
    $(`#tabelaTelefones tr[data-index='${index}']`).remove();
}

function coletarTelefones() {
    const telefones = [];
    $("#tabelaTelefones tbody tr").each(function () {
        const linha = $(this);
        telefones.push({
            numeroTelefone: linha.find("[data-field='numeroTelefone']").val(),
            operadora: linha.find("[data-field='operadora']").val(),
            codigoTipoTelefone: parseInt(linha.find("[data-field='codigoTipoTelefone']").val()),
            ativo: true,
            dataInsercao: new Date().toISOString(),
            usuarioInsercao: "sistema"
        });
    });
    return telefones;
}

function salvarCliente() {
    if (!validarFormulario()) return;

    const cliente = {
        codigoCliente: parseInt($("#CodigoCliente").val()) || 0,
        razaoSocial: $("#RazaoSocial").val(),
        nomeFantasia: $("#NomeFantasia").val(),
        tipoPessoa: $("#TipoPessoa").val(),
        documento: $("#Documento").val(),
        endereco: $("#Endereco").val(),
        complemento: $("#Complemento").val(),
        bairro: $("#Bairro").val(),
        cidade: $("#Cidade").val(),
        cep: $("#CEP").val(),
        uf: $("#UF").val(),
        dataInsercao: new Date().toISOString(),
        usuarioInsercao: "sistema",
        telefones: coletarTelefones()
    };

    console.log("Enviando cliente:", cliente);

    const metodo = cliente.codigoCliente && cliente.codigoCliente != 0 ? "PUT" : "POST";
    const url = `${API_BASE_URL}/clientes` + (metodo === "PUT" ? `/${cliente.codigoCliente}` : "");

    $.ajax({
        url: url,
        type: metodo,
        data: JSON.stringify(cliente),
        contentType: "application/json",
        success: function () {
            alert("Cliente salvo com sucesso!");
            limparFormulario();
            carregarClientes();
        }
    });
}

function carregarClientes() {
    $.get(`${API_BASE_URL}/clientes`, function (data) {
        let html = "";
        data.forEach(function (c) {
            html += `<tr>
                        <td>${c.codigoCliente}</td>
                        <td>${c.razaoSocial}</td>
                        <td>
                            <button onclick="editarCliente(${c.codigoCliente})" class="btn btn-sm btn-warning">Editar</button>
                            <button onclick="deletarCliente(${c.codigoCliente})" class="btn btn-sm btn-danger">Excluir</button>
                        </td>
                     </tr>`;
        });
        $("#tbodyClientes").html(html);
    });
}

function editarCliente(id) {
    $.get(`${API_BASE_URL}/clientes/${id}`, function (c) {
        $("#CodigoCliente").val(c.codigoCliente);
        $("#RazaoSocial").val(c.razaoSocial);
        $("#NomeFantasia").val(c.nomeFantasia);
        $("#TipoPessoa").val(c.tipoPessoa);
        $("#Documento").val(c.documento);
        $("#Endereco").val(c.endereco);
        $("#Complemento").val(c.complemento);
        $("#Bairro").val(c.bairro);
        $("#Cidade").val(c.cidade);
        $("#CEP").val(c.cep);
        $("#UF").val(c.uf);
        $("#tabelaTelefones tbody").empty();
        c.telefones.forEach(t => adicionarTelefone(t));
    });
}

function deletarCliente(id) {
    if (confirm("Deseja excluir este cliente?")) {
        $.ajax({
            url: `${API_BASE_URL}/clientes/${id}`,
            type: "DELETE",
            success: function () {
                alert("Excluído com sucesso!");
                carregarClientes();
            }
        });
    }
}

function limparFormulario() {
    $("#formCliente")[0].reset();
    $("#CodigoCliente").val("");
    $("#tabelaTelefones tbody").empty();
}

function validarFormulario() {
    let erros = [];

    if (!$("#RazaoSocial").val().trim()) {
        erros.push("Razão Social é obrigatória.");
    }

    if (!$("#NomeFantasia").val().trim()) {
        erros.push("Nome Fantasia é obrigatória.");
    }

    if (!$("#TipoPessoa").val()) {
        erros.push("Tipo Pessoa é obrigatória.");
    }

    if (!$("#Documento").val().trim()) {
        erros.push("Documento é obrigatório.");
    }

    // Ex: UF com exatamente 2 caracteres
    const uf = $("#UF").val().trim();
    if (!uf || uf.length !== 2) {
        erros.push("UF deve ter exatamente 2 caracteres.");
    }

    // Telefones
    const telefoneRows = $("#tabelaTelefones tbody tr");
    if (telefoneRows.length === 0) {
        erros.push("Ao menos um telefone deve ser informado.");
    }

    telefoneRows.each(function (i, row) {
        const numero = $(row).find("[data-field='numeroTelefone']").val();
        const tipo = $(row).find("[data-field='codigoTipoTelefone']").val();

        if (!numero || numero.trim().length < 8) {
            erros.push(`Telefone ${i + 1}: número inválido.`);
        }

        if (!tipo) {
            erros.push(`Telefone ${i + 1}: tipo não selecionado.`);
        }
    });

    if (erros.length > 0) {
        alert("Erros encontrados:\n\n" + erros.join("\n"));
        return false;
    }

    return true;
}

$(document).ready(function () {
    carregarClientes();
});

function carregarClientes() {
    $.get("https://localhost:5001/api/clientes", function (data) {
        let html = "";
        data.forEach(function (c) {
            html += `<tr><td>${c.codigoCliente}</td><td>${c.razaoSocial}</td></tr>`;
        });
        $("#tbodyClientes").html(html);
    });
}

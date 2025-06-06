# GrupoColorado# Grupo Colorado - Desafio Desenvolvedor Fullstack

Este repositório contém a implementação do desafio proposto pela Grupo Colorado, utilizando a stack ASP.NET Core 6 com MVC, Web API, arquitetura limpa e consumo via jQuery.

---

## 📦 Estrutura do Projeto

- `GrupoColorado.Domain`: Entidades e regras de negócio.
- `GrupoColorado.Application`: Casos de uso, DTOs, serviços e interfaces.
- `GrupoColorado.Infrastructure`: Persistência de dados com EF Core.
- `GrupoColorado.WebAPI`: API RESTful com Swagger.
- `GrupoColorado.WebAppMVC`: Interface web MVC com jQuery.
- `run-local.bat`: Script para iniciar WebAPI e MVC simultaneamente.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 6
- Entity Framework Core (InMemory)
- AutoMapper
- Swagger
- jQuery (para chamadas AJAX)

---

## ▶️ Como Executar Localmente

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/GrupoColorado.Clientes.git
   ```

2. Restaure os pacotes:
   ```bash
   dotnet restore
   ```

3. Execute a solução:
   - Manual: inicie `GrupoColorado.WebAPI` e `GrupoColorado.WebAppMVC` separadamente
   - Automático: execute `run-local.bat`

4. Acesse:
   - Swagger: `https://localhost:5001/swagger`
   - MVC: `https://localhost:5002`

---

## 🛠️ Funcionalidades Implementadas

- CRUD completo de clientes
- Cadastro de telefones com tipos (residencial, comercial, WhatsApp)
- Interface visual consumindo a API com jQuery
- Arquitetura em camadas e uso de padrão repositório

---

## 🧪 Testes

> Este projeto está pronto para inclusão de testes com xUnit + Moq (não obrigatório neste desafio).

---

## 📝 Observações

- Banco de dados InMemory para facilitar testes e portabilidade.
- Estrutura preparada para migração fácil para SQL Server.
- Solução alinhada aos princípios de Clean Architecture.

---

## 📅 Prazo

Entrega em até 5 dias corridos após recebimento do desafio.

---

Feito com dedicação para a avaliação técnica 💼🚀
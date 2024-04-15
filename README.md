# Ponto Eletrônico

O projeto consiste em uma aplicação web responsável por registrar a jornada de trabalho dos funcionários de uma empresa. A aplicação foi desenvolvida principalmente utilizando a linguagem C# com o framework ASP.NET Core, e segue uma arquitetura em camadas conhecida como Clean Architecture.

## Tecnologias utilizadas

- **Linguagem de Programação: C#**
- **Framework: ASP.NET Core**
- **Arquitetura: Clean Architecture**
- **Banco de Dados: PostgreSQL 16.2**

## Bibliotecas e Ferramentas

- **.NET SDK 5.0.408**
- **EntityFramework Core**
- **Identity**
- **AutoMapper**
- **XUnit**
- **Razor Pages**
- **Visual Studio 2022**

## Funcionalidades implementadas

- Cadastro de funcionários
- Edição de funcionários
- Visualizar histórico de batidas de ponto e informações da jornada de funcionário por data
- Batida de ponto de funcionário
- Implementação de autenticação via login
- Implementação de autorização de funcionalidades a partir de papéis
- Implementação de dois tipos de usuário: "Admin" e "Funcionário"

## Funcionalidades idealizadas mas não implementadas

- Solicitação de edição/exclusão de batidas de ponto por funcionários ao administrador

## Como executar este projeto

1. Abra o arquivo `appsettings.json`, na linha 12 insira as credenciais (Port, Id e Password) do seu banco na string de conexão da propriedade `"DefaultConnection"`.

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Pooling=true;Database=PontoEletronicoDB;User Id=postgres;Password=sua_senha;"
   }
   ```

2. Caso queira executar pelo terminal: dentro da pasta PontoEletronico, abra o terminal e execute o comando: `dotnet watch run -p PontoEletronico.Web`

## Diagrama ER

![PontoEletronicoBD](https://github.com/Wanderleyps/ponto-eletronico-app/assets/105169695/a8904dba-cc5f-454c-87e0-fced1a34c1f2)

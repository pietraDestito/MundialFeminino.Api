# MundialFeminino.Api

API REST desenvolvida em C# com ASP.NET Core para consulta de dados históricos da Copa do Mundo Feminina de Futebol.

## Sobre o projeto

O projeto tem como objetivo organizar e disponibilizar informações históricas das Copas do Mundo Femininas, permitindo consultar as edições do torneio e os dados das artilheiras.

A aplicação disponibiliza dados por meio de endpoints HTTP e utiliza um banco de dados relacional para armazenar as informações.

## Funcionalidades

- Consulta das edições históricas da Copa do Mundo Feminina.
- Consulta do ranking histórico de artilheiras.
- Busca de artilheiras por nome.
- Consulta das artilheiras de uma edição específica.
- Cadastro e atualização de gols para a edição de 2027.
- Validação das requisições para impedir que o endpoint de atualização de 2027 altere dados de edições anteriores.
- Importação de dados históricos e leitura de arquivos CSV.

## Tecnologias utilizadas

- **C#** — linguagem de programação.
- **.NET 10** — plataforma de desenvolvimento.
- **ASP.NET Core Web API** — desenvolvimento dos endpoints REST.
- **Entity Framework Core** — mapeamento objeto-relacional e acesso aos dados.
- **SQL Server Express** — banco de dados relacional.
- **Swagger** — documentação e teste dos endpoints.
- **Git e GitHub** — versionamento e publicação do código.

## Executando o projeto

### Pré-requisitos

- .NET 10 SDK.
- SQL Server Express instalado e em execução.
- Git instalado.
- Ferramenta `dotnet-ef` em versão compatível com o Entity Framework Core utilizado no projeto.

### 1. Clone o repositório

```bash
git clone https://github.com/pietraDestito/MundialFeminino.Api.git
cd MundialFeminino.Api
```

### 2. Configure a conexão com o banco de dados

Copie o arquivo `appsettings.example.json` para `appsettings.json` na pasta principal do projeto.

No arquivo `appsettings.json`, substitua o valor de `ConexaoMundial` pela string de conexão da sua instância do SQL Server.

Exemplo para uma instância local do SQL Server Express:

```json
{
  "ConnectionStrings": {
    "ConexaoMundial": "Server=localhost\\SQLEXPRESS;Database=MundialFemininoDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Ajuste o nome do servidor conforme sua instalação. Essa configuração é um exemplo para ambiente local.

### 3. Aplique as migrations

```bash
dotnet ef database update
```

Esse comando aplica as migrations existentes ao banco de dados configurado.

Se o comando `dotnet ef` não estiver disponível, instale a ferramenta em uma versão compatível com o Entity Framework Core utilizado no projeto.

### 4. Execute a API

```bash
dotnet run
```

Acesse o Swagger pelo endereço informado no terminal. Durante o desenvolvimento, a interface foi acessada em:

[http://localhost:5250/swagger/index.html](http://localhost:5250/swagger/index.html)

## Endpoints

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/Edicoes` | Consulta as edições do torneio. |
| GET | `/api/Artilheiras` | Consulta o ranking histórico de artilheiras. |
| GET | `/api/Artilheiras/buscar?nome=Marta` | Busca artilheiras por nome. |
| GET | `/api/Artilheiras/edicao/{ano}` | Consulta as artilheiras de uma edição específica. |
| PUT | `/api/Artilheiras/2027` | Cadastra ou atualiza dados de artilheiras de 2027. |

### Atualização de dados de 2027

O endpoint `PUT /api/Artilheiras/2027` permite cadastrar uma jogadora ou atualizar sua quantidade de gols na edição de 2027.

- Se a jogadora já estiver cadastrada para 2027, seus gols são atualizados.
- Se ainda não estiver cadastrada, um novo registro é criado.
- Requisições que informam um ano diferente de 2027 são rejeitadas com `400 Bad Request`.

Essa validação impede que esse endpoint seja utilizado para modificar os registros históricos de edições anteriores.

Os endpoints podem ser explorados e testados pelo Swagger.

## Importação de dados

O projeto contém recursos para leitura e importação de dados históricos, além de funcionalidades para validar e importar registros por CSV.

O arquivo `Importacoes/artilheiras-2027.csv` contém **dados fictícios de teste**, utilizados para validar o processo de importação. Esses registros não representam resultados oficiais da Copa do Mundo.

## Aprendizados

Este projeto permite praticar:

- Desenvolvimento de APIs REST.
- Programação com C# e ASP.NET Core.
- Integração com banco de dados relacional.
- Uso do Entity Framework Core e de migrations.
- Consultas e agrupamentos de dados.
- Validação de requisições HTTP.
- Importação de dados estruturados por CSV.
- Versionamento de código com Git e GitHub.

## Autora

Desenvolvido por [pietraDestito](https://github.com/pietraDestito).

Projeto acadêmico e de portfólio.

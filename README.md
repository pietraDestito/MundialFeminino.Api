# MundialFeminino.Api

API REST desenvolvida em C# com ASP.NET Core para consulta de dados históricos da Copa do Mundo Feminina de Futebol.

## Sobre o projeto

O projeto tem como objetivo organizar e disponibilizar informações históricas das Copas do Mundo Femininas, permitindo consultar as edições do torneio e os dados das artilheiras.

A aplicação utiliza uma API para disponibilizar os dados por meio de endpoints HTTP e um banco de dados relacional para armazenar as informações.

## Funcionalidades

- Consulta das edições históricas da Copa do Mundo Feminina.
- Consulta de dados das artilheiras.
- Busca de artilheiras por nome.
- Consulta das artilheiras de uma edição específica.
- Atualização de gols por meio de endpoint próprio.
- Importação de dados históricos e de arquivos CSV.

## Tecnologias utilizadas

- **C#** — linguagem de programação.
- **ASP.NET Core Web API** — desenvolvimento dos endpoints REST.
- **Entity Framework Core** — mapeamento objeto-relacional e acesso aos dados.
- **SQL Server Express** — banco de dados relacional.
- **Swagger** — documentação e teste dos endpoints.
- **Git e GitHub** — versionamento e publicação do código.

## Executando o projeto

### Pré-requisitos

- .NET SDK compatível com o projeto.
- SQL Server Express instalado e em execução.
- Git, para clonar o repositório.

### 1. Clone o repositório

```bash
git clone https://github.com/pietraDestito/MundialFeminino.Api.git
cd MundialFeminino.Api
```

### 2. Configure a conexão com o banco de dados

Crie um arquivo chamado `appsettings.json` na pasta principal do projeto, utilizando `appsettings.example.json` como referência.

Configure a conexão com sua instância local do SQL Server. Exemplo:

```json
{
  "ConnectionStrings": {
    "ConexaoMundial": "Server=localhost\\SQLEXPRESS;Database=MundialFemininoDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Ajuste o servidor conforme sua instalação. Essa configuração é apenas um exemplo para ambiente local.

### 3. Aplique as migrations

```bash
dotnet ef database update
```

Se o comando `dotnet ef` não estiver disponível, será necessário instalar a ferramenta do Entity Framework Core e verificar se a versão corresponde à utilizada pelo projeto.

### 4. Execute a API

```bash
dotnet run
```

Acesse a interface do Swagger pelo endereço informado no terminal. No ambiente local configurado durante o desenvolvimento, ela pode estar disponível em:

`http://localhost:5250/swagger/index.html`

## Endpoints

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/Edicoes` | Consulta as edições do torneio |
| GET | `/api/Artilheiras` | Consulta o ranking histórico de artilheiras |
| GET | `/api/Artilheiras/buscar?nome=Marta` | Busca artilheiras por nome |
| GET | `/api/Artilheiras/edicao/{ano}` | Consulta artilheiras por edição |
| PUT | `/api/Artilheiras/2027` | Atualiza ou registra dados de artilheiras de 2027 |

Os endpoints podem ser explorados e testados pelo Swagger. Os caminhos e métodos devem corresponder às rotas implementadas no código.

## Importação de dados

O projeto contém recursos para leitura e importação de dados históricos, além de importação de registros por CSV.

O arquivo `Importacoes/artilheiras-2027.csv` contém **dados fictícios de teste**, utilizados para validar o processo de importação. Eles não representam os resultados oficiais de uma edição da Copa do Mundo.

## Aprendizados

Este projeto permite praticar o desenvolvimento de APIs REST, a integração com banco de dados relacional, o uso do Entity Framework Core, as migrations e a importação de dados estruturados.

## Autora

Desenvolvido por [pietraDestito](https://github.com/pietraDestito).

Projeto acadêmico e de portfólio.
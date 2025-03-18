# Sistema de Tarefas API

Este é um projeto de API para um **Sistema de Tarefas**, construído com **.NET 6** utilizando a arquitetura MVC e JWT para autenticação. A API oferece funcionalidades para o gerenciamento de tarefas com operações de **CRUD** e integração com banco de dados utilizando o **Entity Framework**.


## Dependências

Este projeto utiliza as seguintes dependências:

- **Microsoft.AspNetCore.Authentication.JwtBearer**: Autenticação JWT.
- **Microsoft.EntityFrameworkCore**: Framework ORM para acessar o banco de dados.
- **Microsoft.EntityFrameworkCore.SqlServer**: Provedor SQL Server para Entity Framework.
- **Microsoft.EntityFrameworkCore.Tools**: Ferramentas de migração e geração de código para Entity Framework.
- **Refit**: Biblioteca para simplificação de chamadas HTTP.
- **Swashbuckle.AspNetCore**: Geração automática de documentação Swagger para a API.
- **System.IdentityModel.Tokens.Jwt**: Manipulação de tokens JWT.

## Como Executar

### Pré-requisitos

Certifique-se de que você tenha os seguintes softwares instalados:

- **.NET 6 SDK**: [Baixar .NET 6](https://dotnet.microsoft.com/download)
- **SQL Server** ou outro banco de dados compatível com o Entity Framework Core.

### Passos para Rodar a Aplicação

1. **Clone o repositório**:

    ```bash
    git clone https://github.com/seu-usuario/SistemaDeTarefas.git
    cd SistemaDeTarefas
    ```

2. **Restaure as dependências**:

    ```bash
    dotnet restore
    ```

3. **Crie e aplique as migrações**:

    - Para criar as migrações:
      ```bash
      dotnet ef migrations add Inicial
      ```
    - Para aplicar as migrações ao banco de dados:
      ```bash
      dotnet ef database update
      ```

4. **Inicie a aplicação**:

    ```bash
    dotnet run
    ```

    A aplicação estará disponível em `http://localhost:5000` por padrão.

## Endpoints

### Tarefas

- **GET /api/tarefas**: Retorna todas as tarefas.
- **GET /api/tarefas/{id}**: Retorna uma tarefa específica pelo ID.
- **POST /api/tarefas**: Cria uma nova tarefa.
- **PUT /api/tarefas/{id}**: Atualiza uma tarefa existente.
- **DELETE /api/tarefas/{id}**: Deleta uma tarefa pelo ID.

### Autenticação

- **POST /api/auth/login**: Realiza o login e retorna um token JWT para autenticação.

## Estrutura de Autenticação

A autenticação é realizada por meio de **tokens JWT**. Para acessar rotas protegidas, você deve incluir o token JWT no cabeçalho da requisição:

```http
Authorization: Bearer {seu-token-jwt}

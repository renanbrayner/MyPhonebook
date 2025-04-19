# Phonebook API (Backend)

Este diretório contém a API em .NET 8.0 para o sistema Phonebook, implementando um CRUD de contatos com arquitetura em camadas e padrão CQRS.

## Arquitetura e camadas

- **Phonebook.API**
  Projeto ASP.NET Core (Web API) que expõe os endpoints REST. Contém controllers, DTOs e validação (FluentValidation).

- **Phonebook.Application**
  Camada de aplicação com handlers de comandos e queries (CQRS) usando MediatR. Orquestra fluxos de negócio e implementa contratos de serviços.

- **Phonebook.Domain**
  Entidades de domínio e regras fundamentais. Define `Contact` e objetos de valor.

- **Phonebook.Infrastructure**
  Implementação de persistência com Entity Framework Core (SQL Server). Inclui:
  - `PhonebookDbContext`
  - Repositórios (`IContactRepository`, `ContactRepository`)
  - `DataSeeder` para popular dados iniciais

- **Phonebook.Tests**
  Projetos de teste de unidade para Domain e Application, usando xUnit e EF Core In-Memory.

## Stack de tecnologias

- **.NET 8.0** (SDK & Runtime)
- **C# 12**
- **ASP.NET Core Web API**
- **Entity Framework Core 9** (SQL Server)
- **MediatR** (CQRS)
- **FluentValidation** (validações de DTO)
- **Swashbuckle/Swagger** (documentação de API)
- **xUnit** (testes de unidade)

## Estrutura de pastas

```
/backend
├── Phonebook.API             # Projeto Web API
│   ├── Controllers           # Endpoints HTTP
│   ├── DTO                   # Data Transfer Objects
│   ├── Validators            # FluentValidation
│   └── Program.cs            # Configuração de serviços
│
├── Phonebook.Application     # Handlers de comandos/queries
│   └── Contacts
│       ├── Commands          # Criar, editar, deletar
│       └── Queries           # Listar, obter por ID
│
├── Phonebook.Domain          # Entidades de domínio
│   └── Entities              # Contact.cs
│
├── Phonebook.Infrastructure  # Persistência e repositórios
│   ├── Data                  # DbContext & Seeder
│   └── Repositories
│
└── Phonebook.Tests           # Testes de unidade
    ├── Domain
    └── Application
```

## Configuração e variáveis de ambiente

A _connection string_ padrão está em `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=PhonebookDb;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;"
  }
}
```

Em produção, ajuste via variáveis de ambiente ou `appsettings.{Environment}.json`.

## Endpoints Principais

| Método | Rota                   | Descrição                       |
| ------ | ---------------------- | ------------------------------- |
| GET    | `/api/contacts`        | Lista todos os contatos         |
| GET    | `/api/contacts/{id}`   | Obtém contato por ID            |
| POST   | `/api/contacts`        | Cria novo contato               |
| PUT    | `/api/contacts/{id}`   | Atualiza contato existente      |
| DELETE | `/api/contacts/{id}`   | Remove contato                  |

A documentação interativa está disponível em **/swagger**.

## Comandos Makefile

No root do projeto (Makefile):

```makefile
# Build da API
build-backend:
	docker-compose build backend

# Sobe API + DB (em background)
up-backend:
	docker-compose up -d backend

# Destrói containers e volumes
down-backend:
	docker-compose down -v

# Atualiza esquema do banco (rodando local)
migrate-backend:
	dotnet ef database update --project backend/Phonebook.Infrastructure --startup-project backend/Phonebook.API

# Executa testes de unidade
test-backend:
	dotnet test Phonebook.Tests/Phonebook.Tests.csproj
```

## Docker

- **Dockerfile** em `/backend/Dockerfile`: build multi-stage com SDK (build/publish) e ASP.NET Runtime.
- **docker-compose.yml** (na raiz): define serviços `db` e `backend`, compartilhando rede `app-network`.

Para rodar tudo:

```bash
# no diretório raiz do repositório
docker-compose up --build
```

## Testes

- **Domain**: validações de construtor de `Contact`.
- **Application**: handlers de `CreateContactCommand`, `EditContactCommand`, etc., usando banco In-Memory.

Para executar:

```bash
dotnet test Phonebook.Tests/Phonebook.Tests.csproj
```

## Decisões de design

- **CQRS** com MediatR: separação clara entre comandos (mutação) e queries (leitura).
- **Repository pattern**: abstração de acesso a dados via `IContactRepository`.
- **Validation**: DTOs validados com FluentValidation antes de chegar aos handlers.
- **Runtime seeding**: popula dados iniciais via `DataSeeder` no startup.

---
Este README fornece um guia completo para entender, executar e estender o backend do MyPhonebook.

# MyPhonebook

MyPhonebook é um projeto de exemplo de uma agenda de contatos, implementado com as tecnologias mais atuais do ecossistema .NET e Vue. Serve como referência de boas práticas de arquitetura (CQRS, Mediator, Repository Pattern), migrações automáticas, seed de dados, Docker, testes automatizados e frontend moderno.

Para informações mais detalhadas sobre o backend e o frontend, veja o [README.md](backend/README.md) e o [README.md](frontend/README.md) respectivamente.

## Tecnologias

- **Backend**: .NET 8, ASP.NET Core Web API, Entity Framework Core, MediatR, FluentValidation
- **Frontend**: Vue 3, Vite, PrimeVue, pnpm
- **Infraestrutura**: Docker, Docker Compose, SQL Server 2022
- **Testes**:
  - **Unitários** (backend): xUnit, Moq
  - **E2E** (frontend): Cypress

## Como iniciar

### Pré-requisitos

- Docker & Docker Compose
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [pnpm](https://pnpm.io/installation)

### 1. Clone este repositório

```bash
git clone https://github.com/renanbrayner/MyPhonebook.git
cd MyPhonebook
```

### 2. Com Docker (produção)

1. Construa e suba todos os serviços:
   ```bash
   make build
   make up
   ```
2. Acesse:
   - Frontend: http://localhost:5173
   - Swagger (API): http://localhost:5036/swagger/index.html
3. Para parar e limpar tudo:
   ```bash
   make down
   ```

### 3. Desenvolvimento local (sem Docker)

- **Backend** (com hot-reload):
  ```bash
  make run-backend
  ```
- **Frontend**:
  ```bash
  make run-frontend
  ```

Acesse o frontend em http://localhost:5173 e a API em http://localhost:5036.

## Comandos do Makefile

| Comando              | Descrição                                                                                   |
|----------------------|---------------------------------------------------------------------------------------------|
| `make build`         | Constrói as imagens Docker do backend e do frontend                                         |
| `make up`            | Sobe os containers em background (`docker-compose up -d`), aplica migrações e faz seed      |
| `make down`          | Para e remove containers e volumes                                                          |
| `make logs`          | Exibe os logs em tempo real (por padrão do backend)                                         |
| `make migrate`       | Executa manualmente as migrações do EF Core no container do backend                         |
| `make reset-db`      | Destroi containers e volumes, limpa o banco e sobe tudo de novo                             |
| `make run-backend`   | Roda o backend local com hot-reload (`dotnet watch --project backend/Phonebook.API run`)    |
| `make run-frontend`  | Roda o frontend local (`pnpm --prefix frontend dev`)                                        |

---

Sinta-se à vontade para explorar o código, criar novos seeds, adicionar mais testes ou estender a aplicação como desejar!

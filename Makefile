DB_SERVICE=db
BACKEND_SERVICE=backend
FRONTEND_SERVICE=frontend

.PHONY: build build-backend build-frontend up down reset-db migrate logs logs-backend logs-frontend status

# build de cada imagem
build-backend:
	docker-compose build $(BACKEND_SERVICE)

build-frontend:
	docker-compose build $(FRONTEND_SERVICE)

build: build-backend build-frontend

# sobe tudo em background
up:
	docker-compose up -d

# baixa tudo (inclui volumes)
down:
	docker-compose down -v

# aplica migrations dentro do container do backend
migrate:
	docker-compose exec $(BACKEND_SERVICE) \
	  dotnet ef database update \
	    --project /src/Phonebook.Infrastructure \
	    --startup-project /src/Phonebook.API

# reset completo: derruba, rebuild e up + migrate
reset-db: down build up migrate

# logs
logs:
	docker-compose logs -f

logs-backend:
	docker-compose logs -f $(BACKEND_SERVICE)

logs-frontend:
	docker-compose logs -f $(FRONTEND_SERVICE)

# só para checar status
status:
	docker ps --filter name=phonebook

# para rodar localmente sem Docker
run-backend:
	dotnet watch --project backend/Phonebook.API run

run-frontend:
	cd frontend && pnpm install && pnpm dev

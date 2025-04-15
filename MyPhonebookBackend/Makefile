PROJECT=Phonebook.API
INFRA=Phonebook.Infrastructure

run:
	dotnet run --project $(PROJECT)

build:
	docker-compose build

up:
	docker-compose up -d

down:
	docker-compose down -v

logs:
	docker-compose logs -f backend

migrate:
	dotnet ef database update --project $(INFRA) --startup-project $(PROJECT)

reset-db:
	docker-compose down -v && docker-compose up -d --build

rebuild:
	docker-compose down -v && docker-compose up --build

status:
	docker ps --filter name=phonebook

ps:
	docker-compose ps

using Phonebook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Phonebook.Application.Contacts;
using Phonebook.Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(Phonebook.Application.Contacts.Commands.CreateContactCommand).Assembly);
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Configuração da connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Database=PhonebookDb;Trusted_Connection=True;TrustServerCertificate=True;";

// Adiciona o DbContext ao container de injeção de dependência
/*
Utilização de padrões de projeto (repositórios, services,
controllers, interfaces, injeção de dependência, etc.) dentro do
contexto da aplicação
*/
builder.Services.AddDbContext<PhonebookDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Desativado já que este é um projeto de teste

app.Run();

using Phonebook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Phonebook.Application.Contacts;
using Phonebook.Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(Phonebook.Application.Contacts.Commands.CreateContactCommand).Assembly);
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Configuração da connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=db;Database=PhonebookDb;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";

// Adiciona o DbContext ao container de injeção de dependência
builder.Services.AddDbContext<PhonebookDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(); // Sempre ativa o Swagger já que este é um projeto de teste
app.UseSwaggerUI();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseAuthorization();

app.MapControllers(); // 👈 Isso mapeia os endpoints dos controllers

// app.UseHttpsRedirection(); // Desativado já que este é um projeto de teste

app.Run();

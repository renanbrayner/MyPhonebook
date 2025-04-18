// Phonebook.Infrastructure/Data/DataSeeder.cs
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(this PhonebookDbContext context)
        {
            if (context.Contacts.Any())
                return;

            var now = DateTime.UtcNow;
            var contacts = new[]
            {
                new Contact("Alice Silva", "(11) 90000-0000", "alice@example.com") { CreatedAt = now },
                new Contact("Bruno Souza", "(21) 98888-8888", "bruno@example.com") { CreatedAt = now },
                new Contact("Carla Mendes", "(31) 97777-7777", "carla@example.com") { CreatedAt = now },
                new Contact("Daniela Oliveira", "(41) 96666-6666", "daniela@example.com") { CreatedAt = now },
                new Contact("Eduardo Costa", "(51) 95555-5555", "eduardo@example.com") { CreatedAt = now },
                new Contact("Felipe Santos", "(61) 94444-4444", "felipe@example.com") { CreatedAt = now },
                new Contact("Gabriel Dias", "(71) 93333-3333", "gabriel@example.com") { CreatedAt = now },
                new Contact("Helena Costa", "(81) 92222-2222", "helena@example.com") { CreatedAt = now },
                new Contact("Isabella Pereira", "(91) 91111-1111", "isabella@example.com") { CreatedAt = now },
                new Contact("João da Silva", "(01) 90000-0000", "joao@example.com") { CreatedAt = now },
                new Contact("Luciana Souza", "(11) 98888-8888", "luciana@example.com") { CreatedAt = now },
                new Contact("Marcos Oliveira", "(21) 97777-7777", "marcos@example.com") { CreatedAt = now },
                new Contact("Maria Mendes", "(31) 96666-6666", "maria@example.com") { CreatedAt = now },
                new Contact("Natália Costa", "(41) 95555-5555", "natalia@example.com") { CreatedAt = now },
                new Contact("Pedro Mendes", "(51) 94444-4444", "pedro@example.com") { CreatedAt = now },
                new Contact("Rafael Santos", "(61) 93333-3333", "rafael@example.com") { CreatedAt = now },
                new Contact("Renata Dias", "(71) 92222-2222", "renata@example.com") { CreatedAt = now },
                new Contact("Sérgio Pereira", "(81) 91111-1111", "sergio@example.com") { CreatedAt = now },
                new Contact("Tatiana da Silva", "(91) 90000-0000", "tatiana@example.com") { CreatedAt = now },
                new Contact("Vicente da Silva", "(01) 98888-8888", "vicente@example.com") { CreatedAt = now },
            };

            context.Contacts.AddRange(contacts);
            context.SaveChanges();
        }
    }
}

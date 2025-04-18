using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phonebook.Application.Contacts.Commands;
using Phonebook.Infrastructure.Data;
using Phonebook.Infrastructure.Repositories;
using Xunit;

namespace Phonebook.Tests.Application
{
    public class CreateContactHandlerTests
    {
        private PhonebookDbContext GetInMemoryDb(string name)
        {
            var options = new DbContextOptionsBuilder<PhonebookDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
            return new PhonebookDbContext(options);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesAndReturnsGuid()
        {
            // Arrange
            var db = GetInMemoryDb("TestDb1");
            var repo = new ContactRepository(db);
            var handler = new CreateContactCommandHandler(db);
            var cmd = new CreateContactCommand("Ana", "(21) 98888-8888", "ana@mail.com");

            // Act
            var newId = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            var created = await db.Contacts.FindAsync(newId);
            Assert.NotNull(created);
            Assert.Equal("Ana", created.Name);
        }
    }
}

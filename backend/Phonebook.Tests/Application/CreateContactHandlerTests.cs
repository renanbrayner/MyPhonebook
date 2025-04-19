using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phonebook.Application.Contacts.Commands;
using Phonebook.Infrastructure.Data;
using Phonebook.Infrastructure.Repositories;
using Xunit;

public class CreateContactHandlerTests
{
    private PhonebookDbContext NewContext()
    {
        var opt = new DbContextOptionsBuilder<PhonebookDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new PhonebookDbContext(opt);
    }

    [Fact]
    public async Task Handle_ValidCommand_SavesContact()
    {
        await using var ctx = NewContext();
        var repo = new ContactRepository(ctx);
        var handler = new CreateContactCommandHandler(repo);

        var cmd = new CreateContactCommand("Ana", "(11) 98888-7777", "ana@test.com");
        var id = await handler.Handle(cmd, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, id);
        var saved = await ctx.Contacts.FindAsync(id);
        Assert.Equal("Ana", saved!.Name);
    }
}

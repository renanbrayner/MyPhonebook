using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phonebook.Application.Contacts.Commands;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Data;
using Phonebook.Infrastructure.Repositories;
using Xunit;

public class DeleteContactHandlerTests
{
    private PhonebookDbContext NewContext()
    {
        var opt = new DbContextOptionsBuilder<PhonebookDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new PhonebookDbContext(opt);
    }

    [Fact]
    public async Task Handle_ExistingContact_RemovesAndReturnsTrue()
    {
        await using var ctx = NewContext();
        var contact = new Contact("Zé", "(11) 90000-0000", "ze@x.com");
        ctx.Contacts.Add(contact);
        await ctx.SaveChangesAsync();

        var handler = new DeleteContactCommandHandler(ctx);
        var result = await handler.Handle(new DeleteContactCommand(contact.Id), CancellationToken.None);

        Assert.True(result);
        Assert.Null(await ctx.Contacts.FindAsync(contact.Id));
    }

    [Fact]
    public async Task Handle_NonExistingContact_ReturnsFalse()
    {
        await using var ctx = NewContext();
        var handler = new DeleteContactCommandHandler(ctx);
        var result = await handler.Handle(new DeleteContactCommand(Guid.NewGuid()), CancellationToken.None);
        Assert.False(result);
    }
}

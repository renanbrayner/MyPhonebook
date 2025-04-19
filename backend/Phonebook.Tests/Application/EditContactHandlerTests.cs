using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phonebook.Application.Contacts.Commands;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Data;
using Phonebook.Infrastructure.Repositories;
using Xunit;

public class EditContactHandlerTests
{
    private PhonebookDbContext NewContext()
    {
        var opt = new DbContextOptionsBuilder<PhonebookDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new PhonebookDbContext(opt);
    }

    [Fact]
    public async Task Handle_ExistingContact_UpdatesValues()
    {
        await using var ctx = NewContext();
        var original = new Contact("Beto", "(11) 90000-0000", "beto@x.com");
        ctx.Contacts.Add(original);
        await ctx.SaveChangesAsync();

        var handler = new EditContactCommandHandler(ctx);

        var cmd = new EditContactCommand(
            original.Id,
            "Roberto",
            "(11) 91111-1111",
            "roberto@x.com"
        );

        var result = await handler.Handle(cmd, CancellationToken.None);

        Assert.True(result);
        var updated = await ctx.Contacts.FindAsync(original.Id);
        Assert.Equal("Roberto", updated!.Name);
        Assert.Equal("(11) 91111-1111", updated.PhoneNumber);
        Assert.Equal("roberto@x.com", updated.Email);
    }

    [Fact]
    public async Task Handle_NonExistingContact_ReturnsFalse()
    {
        await using var ctx = NewContext();
        var handler = new EditContactCommandHandler(ctx);

        var cmd = new EditContactCommand(
            Guid.NewGuid(),
            "Qualquer",
            "(00) 00000-0000",
            "x@x.com"
        );

        var result = await handler.Handle(cmd, CancellationToken.None);
        Assert.False(result);
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Phonebook.Application.Contacts.Queries;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Repositories;
using Xunit;

public class GetContactsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsListFromRepository()
    {
        var fake = new List<Contact>
        {
            new Contact("X","(11)90000-0000","x@x.com"),
            new Contact("Y","(11)91111-1111","y@y.com")
        };

        var repoMock = new Mock<IContactRepository>();
        repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(fake);

        var handler = new GetContactsQueryHandler(repoMock.Object);
        var result = await handler.Handle(new GetContactsQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Name == "X");
    }
}

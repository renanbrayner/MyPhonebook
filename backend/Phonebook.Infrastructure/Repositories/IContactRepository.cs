using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Repositories
{
    public interface IContactRepository
    {
        Task<Guid> AddAsync(Contact contact, CancellationToken cancellationToken);
        Task<List<Contact>> GetAllAsync(CancellationToken cancellationToken);
    }
}

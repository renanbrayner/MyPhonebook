using Phonebook.Domain.Entities;

namespace Phonebook.Application.Contacts
{
    public interface IContactRepository
    {
        Task<Guid> AddAsync(Contact contact, CancellationToken cancellationToken);
        Task<List<Contact>> GetAllAsync(CancellationToken cancellationToken);
    }
}

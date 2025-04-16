using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Data;

namespace Phonebook.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhonebookDbContext _context;

        public ContactRepository(PhonebookDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Contact contact, CancellationToken cancellationToken)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(cancellationToken);
            return contact.Id;
        }

        public async Task<List<Contact>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult(_context.Contacts.ToList());
        }
    }
}

using MediatR;
using Phonebook.Infrastructure.Data;

namespace Phonebook.Application.Contacts.Commands;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, bool>
{
    private readonly PhonebookDbContext _context;

    public DeleteContactCommandHandler(PhonebookDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FindAsync(new object[] { request.Id }, cancellationToken);

        if (contact is null)
            return false;

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

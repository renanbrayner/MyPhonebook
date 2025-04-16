using MediatR;
using Phonebook.Infrastructure.Data;

namespace Phonebook.Application.Contacts.Commands;

public class EditContactCommandHandler : IRequestHandler<EditContactCommand, bool>
{
    private readonly PhonebookDbContext _context;

    public EditContactCommandHandler(PhonebookDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(EditContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FindAsync(new object[] { request.Id }, cancellationToken);

        if (contact is null)
            return false;

        contact.Name = request.Name;
        contact.Email = request.Email;
        contact.PhoneNumber = request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

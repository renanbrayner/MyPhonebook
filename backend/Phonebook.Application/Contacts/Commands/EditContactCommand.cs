using MediatR;

namespace Phonebook.Application.Contacts.Commands
{
    public class EditContactCommand : IRequest<bool>
    {
        public Guid Id { get; }
        public string? Name { get; }
        public string? PhoneNumber { get; }
        public string? Email { get; }

        public EditContactCommand(Guid id, string? name, string? phoneNumber, string? email)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}

using MediatR;

namespace Phonebook.Application.Contacts.Commands
{
    public class CreateContactCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public CreateContactCommand(string name, string phoneNumber, string email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}

using MediatR;
namespace Phonebook.Application.Contacts.Commands;

public class EditContactCommand : IRequest<bool>
{
    public Guid Id { get; set; } // ID do contato a ser editado
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}

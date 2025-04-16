using MediatR;
namespace Phonebook.Application.Contacts.Commands;

public class DeleteContactCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteContactCommand(int id)
    {
        Id = id;
    }
}

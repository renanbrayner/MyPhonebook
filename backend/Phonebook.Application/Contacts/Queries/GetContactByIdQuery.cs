using MediatR;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<Contact?>
    {
        public Guid Id { get; }

        public GetContactByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

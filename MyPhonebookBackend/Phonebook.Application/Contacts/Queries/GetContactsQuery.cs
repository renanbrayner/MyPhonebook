using MediatR;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Contacts.Queries
{
    public class GetContactsQuery : IRequest<List<Contact>>
    {
    }
}

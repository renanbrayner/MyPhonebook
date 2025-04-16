using MediatR;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Repositories;

namespace Phonebook.Application.Contacts.Queries
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<Contact>>
    {
        private readonly IContactRepository _repository;

        public GetContactsQueryHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Contact>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}

using MediatR;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Repositories;

namespace Phonebook.Application.Contacts.Queries
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contact?>
    {
        private readonly IContactRepository _repository;

        public GetContactByIdQueryHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Contact?> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}

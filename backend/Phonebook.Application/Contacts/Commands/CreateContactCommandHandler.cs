using MediatR;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Repositories;

namespace Phonebook.Application.Contacts.Commands
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
    {
        private readonly IContactRepository _repository;

        public CreateContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Contact(request.Name, request.PhoneNumber, request.Email);

            return await _repository.AddAsync(contact, cancellationToken);
        }
    }
}

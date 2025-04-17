using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Contacts.Commands;
using Phonebook.Application.Contacts.Queries;
using Phonebook.Domain.DTO;

namespace Phonebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _mediator.Send(new GetContactsQuery());

            ContactListDTO contactsListDTO = new ContactListDTO(contacts.Select(c => new ContactDTO(c.Id, c.Name!, c.PhoneNumber!, c.Email!)).ToList());

            return Ok(contactsListDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            var contact = await _mediator.Send(new GetContactByIdQuery(id));

            if (contact is null)
                return NotFound();

            ContactDTO contactDTO = new ContactDTO(contact.Id, contact.Name!, contact.PhoneNumber!, contact.Email!);
            return Ok(contactDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDTO createContactDTO)
        {
            var id = await _mediator.Send(new CreateContactCommand(createContactDTO.Name!, createContactDTO.PhoneNumber!, createContactDTO.Email!));
            return Ok(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] EditContactDTO dto)
        {
            var success = await _mediator.Send(
                new EditContactCommand(
                    id,
                    dto.Name!,
                    dto.PhoneNumber!,
                    dto.Email!
                )
            );

            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteContactCommand(id));
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

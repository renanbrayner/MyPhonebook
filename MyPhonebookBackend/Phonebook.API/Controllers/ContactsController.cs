using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Contacts.Commands;
using Phonebook.Application.Contacts.Queries;

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
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id });
        }
    }
}

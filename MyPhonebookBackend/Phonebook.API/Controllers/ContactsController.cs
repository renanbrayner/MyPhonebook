using Microsoft.AspNetCore.Mvc;
using Phonebook.Infrastructure.Data;
using Phonebook.Domain.Entities;

namespace Phonebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly PhonebookDbContext _context;

        public ContactsController(PhonebookDbContext context)
        {
            _context = context;
        }

        // GET: api/contacts
        [HttpGet]
        public IActionResult GetContacts()
        {
            var contacts = _context.Contacts.ToList();
            return Ok(contacts);
        }

        // POST: api/contacts
        [HttpPost]
        public IActionResult CreateContact([FromBody] ContactDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = new Contact
            {
                Name = contactDto.Name,
                PhoneNumber = contactDto.PhoneNumber,
                Email = contactDto.Email,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetContacts), new { id = contact.Id }, contact);
        }
    }

    // DTO para criação de contatos
    public class ContactDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

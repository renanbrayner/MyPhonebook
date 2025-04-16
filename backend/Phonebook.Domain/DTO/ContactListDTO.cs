namespace Phonebook.Domain.DTO
{
    public class ContactListDTO
    {
        public List<ContactDTO> Contacts { get; set; }

        public ContactListDTO(List<ContactDTO> contacts)
        {
            Contacts = contacts;
        }
    }
}

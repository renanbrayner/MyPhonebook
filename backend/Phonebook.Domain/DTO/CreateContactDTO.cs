namespace Phonebook.Domain.DTO
{
    public class CreateContactDTO
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public CreateContactDTO(string? name, string? phoneNumber, string? email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}

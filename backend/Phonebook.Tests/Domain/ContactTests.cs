using System;
using Phonebook.Domain.Entities;
using Xunit;

namespace Phonebook.Tests.Domain
{
    public class ContactTests
    {
        [Fact]
        public void Constructor_InvalidName_Throws()
        {
            // Arrange
            string invalidName = "";
            string phone = "(11) 99999-9999";
            string email = "joao@example.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Contact(invalidName, phone, email)
            );
        }

        [Fact]
        public void Constructor_ValidData_SetsProperties()
        {
            // Arrange
            string name = "João";
            string phone = "(11) 99999-9999";
            string email = "joao@example.com";

            // Act
            var c = new Contact(name, phone, email);

            // Assert
            Assert.Equal(name, c.Name);
            Assert.Equal(phone, c.PhoneNumber);
            Assert.Equal(email, c.Email);
        }
    }
}

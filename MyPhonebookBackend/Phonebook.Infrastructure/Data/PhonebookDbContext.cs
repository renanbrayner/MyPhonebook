using Microsoft.EntityFrameworkCore;
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Data
{
    public class PhonebookDbContext : DbContext
    {
        public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
            });
        }
    }
}

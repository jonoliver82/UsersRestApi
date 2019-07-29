using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            AddTestData();
        }

        public DbSet<User> Users { get; set; }

        private void AddTestData()
        {
            //var user = new User(new UserId("820E7478-D257-44DF-AB76-F286F1D1494C"), new Email("1@example.com"), new Password("password"));
            //Users.Add(user);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // The User type has readonly properties that will not be automatically mapped by EF Core,
            // so explicitly map the properties here
            modelBuilder.Entity<User>(
                builder =>
                {
                    builder.Property(e => e.Id);
                    builder.Property(e => e.Email);
                    builder.Property(e => e.Password);
                });
        }
    }
}

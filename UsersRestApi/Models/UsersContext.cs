using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Infrastructure;

namespace UsersRestApi.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<User>(b =>
            {
                // See https://github.com/aspnet/EntityFrameworkCore/issues/6872
                // Use own value generator for the Id property, as the InMemory provider does not
                // take into account seeded data values.
                b.Property(e => e.Id).HasValueGenerator<MaxPlusOneValueGenerator<User>>();

                // Note the values properties of owned types are not seeded here
                b.HasData(new User
                {
                    Id = 1,
                    Name = "test",
                });

                // Value Object types are not entities
                // Must use anonymous type to initialise as value object does not have foreign key UserId         
                // Seed with HasData
                b.OwnsOne(e => e.Email).HasData(new
                {
                    UserId = 1,
                    Address = "1@example.com",
                });

                b.OwnsOne(e => e.Password).HasData(new
                {
                    UserId = 1,
                    Value = "password",
                });
            });
        }
    }
}

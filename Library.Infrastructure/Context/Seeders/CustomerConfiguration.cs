using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Models;

namespace Library.Infrastructure.Context.Seeders
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasData
            (
                new Customer
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@123.com"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Harry Brodersen",
                    Email = "hary@123.com"

                },
                new Customer
                {
                    Id = 3,
                    Name = "Ashley Mitchell",
                    Email = "ashley@123.com"
                }
            );
        }
    }
}


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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {

            builder.HasData
            (
                new Book
                {
                    Id=1,
                    Title = "The Grass is Always Greener",
                    Author = "Jeffrey Archer",
                    Total = 2,
                    Available = true
                },
                new Book
                {
                    Id = 2,
                    Title = "Breaking Barriers: the Story of a Dalit Chief Secretary",
                    Author = "Kaki Madhava Rao",
                    Total = 1,
                    Available = true

                },
                new Book
                {
                    Id = 3,
                    Title = "Human Anatomy",
                    Author = "Dr. Ashvini Kumar Dwivedi",
                    Total = 2,
                    Available = true
                },
               new Book
               {
                   Id = 4,
                   Title = "Come! Let's Run",
                   Author = "Ma. Subramanian",
                   Total = 1,
                   Available = true
               },
               new Book
               {
                   Id = 5,
                   Title = "The Last Heroes",
                   Author = "Palagummi Sainath (P Sainath)",
                   Total = 1,
                   Available = true
               }
            );
        }
    }
}

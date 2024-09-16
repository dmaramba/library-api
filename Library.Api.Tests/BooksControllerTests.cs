using Library.Api.Controllers;
using Library.Domain.ViewModels;
using Library.Infrastructure.Repository;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Api.Tests
{
    public class BooksControllerTests
    {

        Mock<IBookService> BookService { get; }
        Mock<ICustomerService> CustomerService { get; }
        LibrarySetting LibrarySetting;

        public BooksControllerTests()
        {
            BookService = new Mock<IBookService>();
            CustomerService = new Mock<ICustomerService>();
            LibrarySetting = new LibrarySetting { BorrowMaxDay = 2, ReserveMaxHour = 24 };
        }

        [Fact()]
        public void AddBook_Return_Ok()
        {
            // Arrange
            var controller = new BooksController(BookService.Object, Options.Create(LibrarySetting) , CustomerService.Object);
            var book = new BookAddModel { Author = "Testing 123", Title = "Testing", Total = 5 };
            // Act
            var actionResult = controller.AddBook(book);
            var okResult = actionResult as OkResult;


            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }

    }
}

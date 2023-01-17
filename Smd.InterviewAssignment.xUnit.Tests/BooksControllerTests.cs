using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Smd.InterviewAssignment.WebApi.Controllers;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Entities;
using Smd.InterviewAssignment.WebApi.Services.Interfaces;
using Xunit;

namespace Smd.InterviewAssignment.xUnit.Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly Mock<IEmailService> _mailServiceMock;

        public BooksControllerTests()
        {
            _loggerMock = new Mock<ILogger>();
            _bookServiceMock = new Mock<IBookService>();
            _mailServiceMock = new Mock<IEmailService>();
        }

        [Fact]
        public async void GetAllBook()
        {
            // arrange
            var bookList = GetBookData();
            _bookServiceMock.Setup(a => a.GetAllBooks())
                .ReturnsAsync(bookList);
            
            var booksController = new BooksController(null, _bookServiceMock.Object, _mailServiceMock.Object);
            
            // act
            var actionResult = await booksController.GetBooks();

            // assert
            var objectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(objectResult.Value);
            Assert.Equal(bookList, objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
        }
        
        [Fact]
        public async void GetBookById()
        {
            // arrange
            var id = 1;
            
            var bookToReturn = GetBookData().First(a => a.Id == id);
            _bookServiceMock.Setup(a => a.GetBookById(1))
                .ReturnsAsync(bookToReturn);
            
            var booksController = new BooksController(null, _bookServiceMock.Object, _mailServiceMock.Object);
            
            //act
            var result = await booksController.GetBookById(1);
            
            //assert
            Assert.IsType<ActionResult<Book>>(result);
            var objectResult = result.Result as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
            var model = objectResult.Value as Book;
            Assert.Equal(bookToReturn.Id, model.Id);
            Assert.Equal(bookToReturn.Title, model.Title);
            Assert.Equal(bookToReturn.Author, model.Author);
        }
        
        [Fact]
        public async void GetBookById_BookNull()
        {
            // arrange
            var id = 5;
 
            Book? bookToReturn = null;
            _bookServiceMock.Setup(a => a.GetBookById(id))
                .ReturnsAsync(bookToReturn);
 
            var controller = new BooksController(null, _bookServiceMock.Object, null);
            
            // act
            var result = await controller.GetBookById(id);
            
            // assert
            Assert.IsType<ActionResult<Book>>(result);
            var objectResult = result.Result as OkObjectResult;
            Assert.Null(objectResult);
        }
        
        [Fact]
        public async void Delete()
        {
            // arrange
            var id = 1;
            var bookToDelete = GetBookData().First(a => a.Id == id);
            _bookServiceMock.Setup(a => a.DeleteBook(1));
            
            var booksController = new BooksController(null, _bookServiceMock.Object, null);
            
            //act
            await booksController.DeleteBook(1);
            
            //assert
            _bookServiceMock.Verify(a => a.DeleteBook(id));
        }
        
        private List<Book> GetBookData()
        {
            List<Book> booksData = new List<Book>()
            {
                new() { Id = 1, Title = "Moby Dick", Author = "Herman Melville" },
                new() { Id = 2, Title = "Ulysses", Author = "James Joyce" },
                new() { Id = 3, Title = "The Great Gatsby", Author = "Fitz" },
                new() { Id = 4, Title = "War and Peace", Author = "Leo Tolstoy" }
            };
            return booksData;
        }
    }
}
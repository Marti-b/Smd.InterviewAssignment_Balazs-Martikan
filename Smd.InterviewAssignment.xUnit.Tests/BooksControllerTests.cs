// using System.Threading.Tasks;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Logging.Abstractions;
// using Smd.InterviewAssignment.WebApi.Controllers;
// using Smd.InterviewAssignment.WebApi.Data;
// using Smd.InterviewAssignment.WebApi.Entities;
// using Xunit;
//
// namespace Smd.InterviewAssignment.xUnit.Tests
// {
//     public class BooksControllerTests 
//     {
//         [Fact]
//         public async Task GetBooksAsync()
//         {
//             // arrange
//              var controller = new BooksController(_logger, _bookContext);
//
//             // act
//             var expectedResult = await controller.GetBooks();
//             
//             // assert
//             Assert.Equal((int)System.Net.HttpStatusCode.OK, 200);
//         }
//     }
// }
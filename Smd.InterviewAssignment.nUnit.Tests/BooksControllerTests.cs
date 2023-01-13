// using Microsoft.Extensions.Logging.Abstractions;
// using NUnit.Framework;
// using Smd.InterviewAssignment.WebApi.Controllers;

// namespace Smd.InterviewAssignment.nUnit.Tests
// {
//     public class BooksControllerTests
//     {
//         [SetUp]
//         public void Setup()
//         {
//         }

//         [Test]
//         public void Get_NotEmpty()
//         {
//             var sut = new BooksController(new NullLogger<BooksController>());

//             var items = sut.Get();

//             Assert.That(items, Is.Not.Empty);
//         }
//     }
// }
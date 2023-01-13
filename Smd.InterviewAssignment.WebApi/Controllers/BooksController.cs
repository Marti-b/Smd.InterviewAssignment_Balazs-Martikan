using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Smd.InterviewAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            return new dynamic[4]
            {
                new {id = 1, title = "Moby Dick", author = "Herman Melville"},
                new {id = 2, title = "Ulysses", author = "James Joyce"},
                new {id = 3, tilte = "The Great Gatsby", author = "Fitz"},
                new {id = 4, title = "War and Peace", author = "Leo Tolstoy"}
            };
        }

        [HttpGet]
        [Route("{id}")]
        public object Get(int id)
        {
            foreach (var book in Get())
            {
                if (book.id == id)
                {
                    return book;
                }
            }

            return null;
        }

        [HttpGet]
        [Route("mail")]
        public void Mail(string recipient)
        {
            var emailClient = new SmtpClient("host");
            emailClient.Send("noreply@dba.dk", recipient, "New books today",
                "Here is a list of new books: TODO");
        }
    }
}
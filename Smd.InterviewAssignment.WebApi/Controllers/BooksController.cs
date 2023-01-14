using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Entities;

namespace Smd.InterviewAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly BookContext _bookContext;

        public BooksController(ILogger<BooksController> logger, BookContext bookContext)
        {
            _logger = logger;
            _bookContext = bookContext;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            var books = _bookContext.Books.ToList();

            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Book> Get(int id)
        {
            
            var book = _bookContext.Books.Find(id);
            if (book == null)
            {
                _logger.LogError("Book not found");
                return NotFound("The requested book is not found");
            }

            return Ok(book);
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
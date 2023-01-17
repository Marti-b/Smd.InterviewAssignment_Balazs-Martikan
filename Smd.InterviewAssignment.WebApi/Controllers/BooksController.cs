using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smd.InterviewAssignment.WebApi.Entities;
using Smd.InterviewAssignment.WebApi.Services.Interfaces;

namespace Smd.InterviewAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;

        public BooksController(Logger<BooksController> logger, IBookService bookService, IEmailService emailService)
        {
            _logger = logger;
            _bookService = bookService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                //_logger.LogError("Book is not found");
                return NotFound("The requested book is not found");
            }

            return Ok(book);
        }

        [HttpPost]
        
        public async Task<ActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
               _bookService.CreateNewBook(book);
            }

            return Created("/", _bookService.GetBookById(book.Id).Result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBook(Book book)
        {
            if (ModelState.IsValid)
		    {
                _bookService.UpdateBook(book);
                return Ok(_bookService.GetBookById(book.Id).Result);
            }

            return BadRequest("Book cannot be updated. Please check if the book parameters are correct");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            if (_bookService.GetBookById(id).Result == null)
            {
                return Ok(await _bookService.GetAllBooks());
            }
            
            //_logger.LogError("Book is not found");
            return NotFound("The requested book is not found");
        }
        
        [HttpPut]
        [Route("{id}/read")]
        public async Task<ActionResult> MakeBookRead(int id)
        {
            if (ModelState.IsValid)
            {
                var bookUpdated = await _bookService.MakeBookRead(id);
                if (bookUpdated == null)
                    return NotFound("Book was not found");
                
                return Ok(bookUpdated);
            }

            return BadRequest("Pleas check if input format is correct");
        }

        // Emails sent can be seen at : https://ethereal.email/messages
        // Login email:oleta.davis@ethereal.email; password:Bacq26PEvtnjVxxVwJ
        [HttpPost]
        [Route("mail")]
        public void Mail(string recipient)
        {
            _emailService.SendEmail(recipient);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
//using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
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
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _bookContext.Books.ToListAsync();

            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookContext.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogError("Book is not found");
                return NotFound("The requested book is not found");
            }

            return Ok(book);
        }

        [HttpPost]
        
        public async Task<ActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookContext.Books.Add(book);
                await _bookContext.SaveChangesAsync();
            }
            var newBook = await _bookContext.Books.FindAsync(book.Id);
            
            return Created("/", newBook);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBook(Book book)
        {
            if (ModelState.IsValid)
		    {
                var bookToUpdate = await _bookContext.Books.FindAsync(book.Id);
		        bookToUpdate.Title = book.Title;
		        bookToUpdate.Author = book.Author;
		        await _bookContext.SaveChangesAsync();
                return Ok(await _bookContext.Books.ToListAsync());
            }

            return BadRequest("Book cannot be updated. Please check if the book parameters are correct");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookContext.Books.FindAsync(id);
            if ( book != null)
            {
                _bookContext.Books.Remove(book);
                await _bookContext.SaveChangesAsync();
                
                return Ok(await _bookContext.Books.ToListAsync());
            }

            _logger.LogError("Book is not found");
            return NotFound("The requested book is not found");
        }
        
        [HttpPut]
        [Route("{id}/read")]
        public async Task<ActionResult> MakeBookRead(int id)
        {
            var bookToUpdate = await _bookContext.Books.FindAsync(id);
            if (bookToUpdate == null)
                return NotFound("Book was not found");

            if (ModelState.IsValid)
            {
                bookToUpdate.IsRead = true;
                await _bookContext.SaveChangesAsync();
            }
           
            return Ok(bookToUpdate);
        }

        // Emails sent can be seen at : https://ethereal.email/messages
        // Login email:oleta.davis@ethereal.email; password:Bacq26PEvtnjVxxVwJ
        [HttpPost]
        [Route("mail")]
        public void Mail(string recipient)
        {
            var listOfBookObjects = _bookContext.Books.ToList();
            string listOfBooks = JsonSerializer.Serialize(listOfBookObjects);
            
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("noreply@dba.dk"));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = ("New books today");
            email.Body = new TextPart(TextFormat.Text)
            {
                Text = $"Here is a list of new books: {listOfBooks}"
            };
            
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("oleta.davis@ethereal.email", "Bacq26PEvtnjVxxVwJ");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
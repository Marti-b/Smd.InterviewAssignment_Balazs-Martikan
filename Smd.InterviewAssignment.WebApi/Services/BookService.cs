using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Entities;
using Smd.InterviewAssignment.WebApi.Services.Interfaces;

namespace Smd.InterviewAssignment.WebApi.Services
{
    public class BookService : IBookService
    {
        private readonly BookContext _bookContext;
        
        public BookService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }
        
        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _bookContext.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _bookContext.Books.FindAsync(id);
            return book ?? null;
        }

        public async void AddNewBook(Book book)
        {
            _bookContext.Books.Add(book);
            await _bookContext.SaveChangesAsync();
        }

        public async void UpdateBook(Book book)
        {
            var bookUpdated = await _bookContext.Books.FindAsync(book.Id);
            if (bookUpdated == null)
                return;
            
            bookUpdated.Title = book.Title;
            bookUpdated.Author = book.Author;
            await _bookContext.SaveChangesAsync();
        }

        public void DeleteBook(int id)
        {
            var book = _bookContext.Books.Find(id);
            if (book == null)
                return;
            
            _bookContext.Books.Remove(book);
            _bookContext.SaveChangesAsync();
        }

        public async Task<Book> MakeBookRead(int id)
        {
            var bookUpdated = await _bookContext.Books.FindAsync(id);
            if (bookUpdated == null)
                return null;
            
            bookUpdated.IsRead = true;
            await _bookContext.SaveChangesAsync();

            return bookUpdated;
        }
    }
}
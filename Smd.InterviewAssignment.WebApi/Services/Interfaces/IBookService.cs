using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smd.InterviewAssignment.WebApi.Entities;

namespace Smd.InterviewAssignment.WebApi.Services.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        ///     Gets all the books
        /// </summary>
        /// <returns> Returns a list of books </returns>
        Task<List<Book>> GetAllBooks();
        
        /// <summary>
        ///     Gets a book by its Id
        /// </summary>
        /// <param name="id"> Id of the book </param>
        /// <returns> Returns a book object if found, null if not </returns>
        Task<Book> GetBookById(int id);
        
        /// <summary>
        ///     Creates and adds a new book to the db
        /// </summary>
        /// <param name="book"> The book to create </param>
        void CreateNewBook(Book book);
        
        /// <summary>
        ///     Updates the book entity with given parameter object
        /// </summary>
        /// <param name="book"> Entity to update Book entity with </param>
        void UpdateBook(Book book);
        
        /// <summary>
        ///     Deletes the book by Id
        /// </summary>
        /// <param name="id">Id of the book</param>
        void DeleteBook(int id);
        
        /// <summary>
        ///     Sets the book to be read by the user
        /// </summary>
        /// <param name="id"> Id of the book</param>
        /// <returns> Returns the updated book </returns>
        Task<Book> MakeBookRead(int id);
    }
}
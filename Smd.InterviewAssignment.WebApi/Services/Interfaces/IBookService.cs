using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smd.InterviewAssignment.WebApi.Entities;

namespace Smd.InterviewAssignment.WebApi.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        void AddNewBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        Task<Book> MakeBookRead(int id);
    }
}
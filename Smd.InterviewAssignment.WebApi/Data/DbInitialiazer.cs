using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smd.InterviewAssignment.WebApi.Entities;

namespace Smd.InterviewAssignment.WebApi.Data
{
    public static class DbInitialiazer
    {
        public static void Initialize(BookContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            var books = new List<Book>()
            {
                new Book {Id = 1, Title = "Moby Dick", Author = "Herman Melville"},
                new Book {Id = 2, Title = "Ulysses", Author = "James Joyce"},
                new Book {Id = 3, Title = "The Great Gatsby", Author = "Fitz"},
                new Book {Id = 4, Title = "War and Peace", Author = "Leo Tolstoy"}
            };

            foreach (var book in books)
            { 
                context.Books.Add(book);
            }

            context.SaveChanges();
        }
    }
}
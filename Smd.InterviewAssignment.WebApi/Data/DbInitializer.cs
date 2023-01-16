using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smd.InterviewAssignment.WebApi.Entities;

namespace Smd.InterviewAssignment.WebApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BookContext context)
        {
            if (context.Books.Any())
                return;
            
            var books = new List<Book>
            {
                new() {Id = 1, Title = "Moby Dick", Author = "Herman Melville"},
                new() {Id = 2, Title = "Ulysses", Author = "James Joyce"},
                new() {Id = 3, Title = "The Great Gatsby", Author = "Fitz"},
                new() {Id = 4, Title = "War and Peace", Author = "Leo Tolstoy"}
            };

            foreach (var book in books)
            { 
                context.Books.Add(book);
            }

            context.SaveChanges();
        }
    }
}
using Smd.InterviewAssignment.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Smd.InterviewAssignment.WebApi.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
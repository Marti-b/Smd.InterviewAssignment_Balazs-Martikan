using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Moq;
using Smd.InterviewAssignment.WebApi.Controllers;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Entities;
using Smd.InterviewAssignment.WebApi.Services;
using Smd.InterviewAssignment.WebApi.Services.Interfaces;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] 
namespace Smd.InterviewAssignment.xUnit.Tests;
using Xunit;

public class BookServiceTests
{
    private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();
        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(s => sourceList.Add(s));
        return dbSet;
    }
    
    [Fact]
    public async void GetBookById()
    {
        // arrange

        var bookEntity = new Book()
        {
            Id = 1,
            Title = "TestTitle",
            Author = "Testauthor",
            IsRead = true
        };
        
        var bookList = new List<Book> { bookEntity };
        var bookContextMock = new Mock<BookContext>(null);
        var dbSetMock = GetQueryableMockDbSet(bookList);
        bookContextMock.Setup(a => a.Set<Book>()).Returns(dbSetMock.Object);

        var bookService = new BookService(bookContextMock.Object);

        // act
        var result = await bookService.GetBookById(1);

        // assert
        Assert.Equal(bookList.FirstOrDefault(), result);
    }
}
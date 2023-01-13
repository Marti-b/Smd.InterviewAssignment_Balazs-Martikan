# Smd.InterviewAssignment

This project is a web api that contains:

* A `BooksController` for a Book API that can return book information.
* The book information is stored in memory.
* A unit test project with one unit test.

Prerequisites for working on this:
* Built on .NET 6
* SDK version 6.0.201
* Download SDK here: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
* For IDE you can use Visual Studio, JetBrains Rider, or Visual Studio Code

Run the web api to see the result.


## Tasks:

Schedule 4-6 hours for the tasks.

4-6 hours will probably not be enough for all of the tasks.

If you do not do all of the tasks,
you are welcome to pseudo code or explain in a comment what you would intend to do,
or to break down into a task list of steps to take.

Tasks:

* Improve the code for readability and maintainability, for example using the SOLID principles.
* You may change the code in any way you desire, as long as the API returns data in the same format on the same URL, for example http://localhost:5001/books
* For example you may change to another type than `dynamic` in `BooksController`.
* Create CRUD endpoints for the Books API, for example using the REST api architecture. 
* Create unit tests for all endpoints, you can use eiter xUnit or nUnit.
* Fix bug: A user reports that the title is missing for at least one of the books.
* Create an endpoint for marking books as read.
* Create an endpoint for sending an email with a list of books (started but incomplete).

When working on the tasks you can just assume that information about books, etc. can be stored in memory (like it already is in `BooksController`).

using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStrore.Tests.Data
{
    internal static class TestDbContextFactory
    {
        public static BookstoreDbContext Create()
        {
            // Define options for an in-memory database using a unique name
            var options = new DbContextOptionsBuilder<BookstoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Create and seed the in-memory database
            var context = new BookstoreDbContext(options);

            context.Database.EnsureCreated();

            var author1 = new Author { FirstName = "John", LastName = "Doe" };
            var author2 = new Author { FirstName = "Jane", LastName = "Smith" };

            var genre1 = new Genre { Name = "Science Fiction" };
            var genre2 = new Genre { Name = "Mystery" };

            var book1 = new Book { Title = "Book 1", Genre = genre1, Author = author1 };
            var book2 = new Book { Title = "Book 2", Genre = genre2, Author = author2 };

            context.Authors.Add(author1);
            context.Authors.Add(author2);

            context.Genres.Add(genre1);
            context.Genres.Add(genre2);

            context.Books.Add(book1);
            context.Books.Add(book2);

            context.SaveChanges();

            return context;
        }
    }
}

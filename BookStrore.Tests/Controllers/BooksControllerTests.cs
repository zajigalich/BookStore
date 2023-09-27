using AutoMapper;
using BookStore.API.Controllers;
using BookStore.API.DTOs;
using BookStrore.Tests.Data;
using BookStrore.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BookStrore.Tests.Controllers
{
	public class BooksControllerTests
	{
		private IMapper _mapper;

		[SetUp]
		public void Setup()
		{
			_mapper = TestHelper.CreateMap(); // Use TestHelper method to create IMapper instance
		}

		// Example test: Get all books
		[Test]
		public void GetAllBooks_ReturnsAllBooksAsync()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var booksController = new BooksController(context, _mapper);

			// Act
			var result = booksController.GetBooks();
			
			// Assert
			Assert.AreEqual(2, result.Value.Count());
		}

		// Example test: Get book by id
		[Test]
		public void GetBookById_ReturnsBook()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var booksController = new BooksController(context, _mapper);
			int bookId = 1; // Set this to a valid book id in the seeded data

			// Act
			var result = booksController.GetBookById(bookId);

			// Assert
			Assert.NotNull(result.Value);
			Assert.AreEqual(bookId, result.Value.Id);
		}

		// Example test: Create book with valid data
		[Test]
		public void CreateBook_WithValidData_ReturnsCreatedAtAction()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var booksController = new BooksController(context, _mapper);

			var newBookDto = new CreateBookDto
			{
				Title = "New Book Title",
				AuthorId = 1,   // Set this to a valid author id in the seeded data
				GenreId = 1,    // Set this to a valid genre id in the seeded data
			};

			// Act
			var result = booksController.CreateBook(newBookDto);

			// Assert
			Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>()); // Expecting 201 Created HTTP response

			var createdAtResult = result;
			var createdBookDto = createdAtResult.Value;

			Assert.That(createdBookDto, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(createdBookDto.Title, Is.EqualTo(newBookDto.Title));
				Assert.That(createdBookDto.AuthorName, Is.EqualTo("John Doe"));
				Assert.That(createdBookDto.GenreName, Is.EqualTo("Science Fiction"));
			});
		}

		// Example test: Update book with valid data
		[Test]
		public void UpdateBook_WithValidData_UpdatesBook()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var booksController = new BooksController(context, _mapper);
			int bookId = 1; // Set this to a valid book id in the seeded data

			var updatedBookDto = new UpdateBookDto
			{
				Title = "Updated Book Title",
				AuthorId = 2,    // Set this to a valid author id
				GenreId = 2      // Set this to a valid genre id
			};

			// Act
			var result = booksController.UpdateBook(bookId, updatedBookDto);

			// Assert
			Assert.That(result, Is.InstanceOf<NoContentResult>()); // Expecting 204 No Content HTTP response

			var updatedBook = context.Books.Find(bookId);
			Assert.That(updatedBook, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(updatedBook.Title, Is.EqualTo(updatedBookDto.Title));
				Assert.That(updatedBook.AuthorId, Is.EqualTo(updatedBookDto.AuthorId));
				Assert.That(updatedBook.GenreId, Is.EqualTo(updatedBookDto.GenreId));
			});
		}

		// Example test: Update book with invalid id
		[Test]
		public void UpdateBook_WithInvalidId_ReturnsNotFoundResult()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var booksController = new BooksController(context, _mapper);
			int invalidBookId = 999;

			var updatedBookDto = new UpdateBookDto
			{
				Title = "Updated Book Title",
				AuthorId = 2,    // Set this to a valid author id
				GenreId = 2      // Set this to a valid genre id
			};

			// Act
			var result = booksController.UpdateBook(invalidBookId, updatedBookDto);

			// Assert
			Assert.That(result, Is.InstanceOf<NotFoundResult>()); // Expecting 404 Not Found HTTP response
		}
	}
}

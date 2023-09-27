using AutoMapper;
using BookStore.API.Controllers;
using BookStore.API.DTOs;
using BookStrore.Tests.Data;
using BookStrore.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BookStrore.Tests.Controllers
{
	public class AuthorsControllerTests
	{
		private IMapper _mapper;

		[SetUp]
		public void Setup()
		{
			_mapper = TestHelper.CreateMap(); // Use TestHelper method to create IMapper instance
		}

		// Example test: Get all authors
		[Test]
		public void GetAllAuthors_ReturnsAllAuthors()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var authorsController = new AuthorsController(context, _mapper);

			// Act
			var result = authorsController.GetAuthors();

			// Assert
			Assert.That(result.Value.Count(), Is.EqualTo(2));
		}

		// Example test: Get author by id
		[Test]
		public void GetAuthorById_ReturnsAuthor()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var authorsController = new AuthorsController(context, _mapper);
			int authorId = 1; // Set this to a valid author id in the seeded data

			// Act
			var result = authorsController.GetAuthorById(authorId);

			// Assert
			Assert.That(result.Value, Is.Not.Null);
			Assert.That(result.Value.Id, Is.EqualTo(authorId));
		}

		// Example test: Create author with valid data
		[Test]
		public void CreateAuthor_WithValidData_ReturnsCreatedAtAction()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var authorsController = new AuthorsController(context, _mapper);

			var newAuthorDto = new CreateAuthorDto
			{
				FirstName = "New",
				LastName = "Author"
			};

			// Act
			var result = authorsController.CreateAuthor(newAuthorDto);


			// Assert
			var createdAuthorDto = result.Value;

			Assert.That(result, Is.InstanceOf<CreatedAtActionResult>()); // Expecting 201 Created HTTP response

			Assert.Multiple(() =>
			{
				Assert.That(createdAuthorDto, Is.Not.Null);
				Assert.That(createdAuthorDto.FirstName, Is.EqualTo(newAuthorDto.FirstName));
				Assert.That(createdAuthorDto.LastName, Is.EqualTo(newAuthorDto.LastName));
			});
		}

		// Example test: Update author with valid data
		[Test]
		public void UpdateAuthor_WithValidData_UpdatesAuthor()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var authorsController = new AuthorsController(context, _mapper);
			int authorId = 1; // Set this to a valid author id in the seeded data

			var updatedAuthorDto = new UpdateAuthorDto
			{
				FirstName = "Updated",
				LastName = "Author"
			};

			// Act
			var result = authorsController.UpdateAuthor(authorId, updatedAuthorDto);

			// Assert
			Assert.That(result, Is.InstanceOf<NoContentResult>()); // Expecting 204 No Content HTTP response

			var updatedAuthor = context.Authors.Find(authorId);
			Assert.That(updatedAuthor, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(updatedAuthor.FirstName, Is.EqualTo(updatedAuthorDto.FirstName));
				Assert.That(updatedAuthor.LastName, Is.EqualTo(updatedAuthorDto.LastName));
			});
		}

		// Example test: Update author with invalid id
		[Test]
		public void UpdateAuthor_WithInvalidId_ReturnsNotFoundResult()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var authorsController = new AuthorsController(context, _mapper);
			int invalidAuthorId = 999;

			var updatedAuthorDto = new UpdateAuthorDto
			{
				FirstName = "Updated",
				LastName = "Author"
			};

			// Act
			var result = authorsController.UpdateAuthor(invalidAuthorId, updatedAuthorDto);

			// Assert
			Assert.That(result, Is.InstanceOf<NotFoundResult>()); // Expecting 404 Not Found HTTP response
		}

		// Example test: Delete author with valid id
		[Test]
		public void DeleteAuthor_WithValidId_DeletesAuthor()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var authorsController = new AuthorsController(context, _mapper);
			int existingAuthorId = 1; // Set this to a valid author id in the seeded data

			// Act
			var result = authorsController.DeleteAuthor(existingAuthorId);

			// Assert
			Assert.That(result, Is.InstanceOf<NoContentResult>()); // Expecting 204 No Content HTTP response

			var deletedAuthor = context.Authors.Find(existingAuthorId);
			Assert.That(deletedAuthor, Is.Null);
		}

		// Example test: Delete author with invalid id
		[Test]
		public void DeleteAuthor_WithInvalidId_ReturnsNotFoundResult()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var authorsController = new AuthorsController(context, _mapper);
			int invalidAuthorId = 999;

			// Act
			var result = authorsController.DeleteAuthor(invalidAuthorId);

			// Assert
			Assert.That(result, Is.InstanceOf<NotFoundResult>()); // Expecting 404 Not Found HTTP response
		}

	}
}

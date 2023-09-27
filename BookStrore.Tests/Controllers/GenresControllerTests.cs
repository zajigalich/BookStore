using AutoMapper;
using BookStrore.Tests.Data;
using BookStrore.Tests.Helpers;
using BookStore.API.Controllers;
using BookStore.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStrore.Tests.Controllers
{
	public class GenresControllerTests
	{
		private IMapper _mapper;

		[SetUp]
		public void Setup()
		{
			_mapper = TestHelper.CreateMap(); // Use TestHelper method to create IMapper instance
		}

		// Example test: Get all genres
		[Test]
		public void GetAllGenres_ReturnsAllGenres()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var genresController = new GenresController(context, _mapper);

			// Act
			var result = genresController.GetGenres();

			// Assert
			Assert.That(result.Value.Count(), Is.EqualTo(2));
		}

		// Example test: Get genre by id
		[Test]
		public void GetGenreById_ReturnsGenre()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var genresController = new GenresController(context, _mapper);
			int genreId = 1; // Set this to a valid genre id in the seeded data

			// Act
			var result = genresController.GetGenreById(genreId);

			// Assert
			Assert.That(result.Value, Is.Not.Null);
			Assert.That(result.Value.Id, Is.EqualTo(genreId));
		}

		// Example test: Create genre with valid data
		[Test]
		public void CreateGenre_WithValidData_ReturnsCreatedAtAction()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var genresController = new GenresController(context, _mapper);

			var newGenreDto = new CreateGenreDto
			{
				Name = "New Genre"
			};

			// Act
			var result = genresController.CreateGenre(newGenreDto);

			// Assert
			Assert.That(result, Is.InstanceOf<CreatedAtActionResult>()); // Expecting 201 Created HTTP response

			var createdAtResult = result;
			var createdGenreDto = createdAtResult.Value;

			Assert.That(createdGenreDto, Is.Not.Null);
			Assert.That(createdGenreDto.Name, Is.EqualTo(newGenreDto.Name));
		}

		// Example test: Update genre with valid data
		[Test]
		public void UpdateGenre_WithValidData_UpdatesGenre()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var genresController = new GenresController(context, _mapper);
			int genreId = 1; // Set this to a valid genre id in the seeded data

			var updatedGenreDto = new UpdateGenreDto
			{
				Name = "Updated Genre"
			};

			// Act
			var result = genresController.UpdateGenre(genreId, updatedGenreDto);

			// Assert
			Assert.That(result, Is.InstanceOf<NoContentResult>()); // Expecting 204 No Content HTTP response

			var updatedGenre = context.Genres.Find(genreId);
			Assert.That(updatedGenre, Is.Not.Null);
			Assert.That(updatedGenre.Name, Is.EqualTo(updatedGenreDto.Name));
		}

		// Example test: Update genre with invalid id
		[Test]
		public void UpdateGenre_WithInvalidId_ReturnsNotFoundResult()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var genresController = new GenresController(context, _mapper);
			int invalidGenreId = 999;

			var updatedGenreDto = new UpdateGenreDto
			{
				Name = "Updated Genre"
			};

			// Act
			var result = genresController.UpdateGenre(invalidGenreId, updatedGenreDto);

			// Assert
			Assert.That(result, Is.InstanceOf<NotFoundResult>()); // Expecting 404 Not Found HTTP response
		}

		// Example test: Delete genre with valid id
		[Test]
		public void DeleteGenre_WithValidId_DeletesGenre()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var genresController = new GenresController(context, _mapper);
			int existingGenreId = 1; // Set this to a valid genre id in the seeded data

			// Act
			var result = genresController.DeleteGenre(existingGenreId);

			// Assert
			Assert.That(result, Is.InstanceOf<NoContentResult>()); // Expecting 204 No Content HTTP response

			var deletedGenre = context.Genres.Find(existingGenreId);
			Assert.That(deletedGenre, Is.Null);
		}

		// Example test: Delete genre with invalid id
		[Test]
		public void DeleteGenre_WithInvalidId_ReturnsNotFoundResult()
		{
			// Arrange
			using var context = TestDbContextFactory.Create();
			var genresController = new GenresController(context, _mapper);
			int invalidGenreId = 999;

			// Act
			var result = genresController.DeleteGenre(invalidGenreId);

			// Assert
			Assert.That(result, Is.InstanceOf<NotFoundResult>()); // Expecting 404 Not Found HTTP response
		}
	}
}

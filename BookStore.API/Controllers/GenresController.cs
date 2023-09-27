using AutoMapper;
using BookStore.API.Data;
using BookStore.API.DTOs;
using BookStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GenresController : ControllerBase
	{
		private readonly BookstoreDbContext _context;
		private readonly IMapper _mapper; // Remove this line if you're not using AutoMapper

		public GenresController(BookstoreDbContext context, IMapper mapper) // Remove IMapper from constructor if not using AutoMapper
		{
			_context = context;
			_mapper = mapper; // Remove this line if you're not using AutoMapper
		}

		[HttpGet]
		public ActionResult<IEnumerable<GenreDto>> GetGenres()
		{
			var genres = _context.Genres.Find();

			// If you're not using AutoMapper, remove the following line
			// and change the return type of this method to IEnumerable<Genre>
			var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genres);

			return Ok(genresDto); // Replace genresDto with genres if not using AutoMapper
		}

		[HttpGet("{id}")]
		public ActionResult<GenreDto> GetGenreById(int id)
		{
			var genre = _context.Genres.FirstOrDefault(g => g.Id == id);

			if (genre == null)
			{
				return NotFound();
			}

			// If you're not using AutoMapper, remove the following line
			// and change the return type of this method to Genre
			var genreDto = _mapper.Map<GenreDto>(genre);

			return Ok(genreDto); // Replace genreDto with genre if not using AutoMapper
		}

		[HttpPost]
		public ActionResult<GenreDto> CreateGenre(CreateGenreDto createGenreDto)
		{
			// Map the input DTO to the Genre model (skip this line if not using DTOs and AutoMapper)
			var genre = _mapper.Map<Genre>(createGenreDto);

			// Add the new genre to the Genres DbSet in the context
			_context.Genres.Add(genre);
			_context.SaveChanges();

			// Map the genre model to the output DTO for the response (skip this line if not using DTOs and AutoMapper)
			var genreDto = _mapper.Map<GenreDto>(genre);

			// Return the created genre and its URI
			return CreatedAtAction(nameof(GetGenreById), new { id = genreDto.Id }, genreDto);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateGenre(int id, UpdateGenreDto updateGenreDto)
		{
			// Retrieve the existing genre from the database
			var genre = _context.Genres.Find(id);

			// If the genre is not found, return a 404 Not Found response
			if (genre == null)
			{
				return NotFound();
			}

			// Update the genre's properties from the input DTO (skip this line if not using DTOs and AutoMapper)
			_mapper.Map(updateGenreDto, genre);

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Conflict("The genre was already updated by another request.");
			}

			return NoContent(); // Return a 204 No Content response as there is no need to send the updated genre back to the client
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteGenre(int id)
		{
			var genre = _context.Genres.Find(id);

			// If the genre is not found, return a 404 Not Found response
			if (genre == null)
			{
				return NotFound();
			}

			// Remove the genre from the Genres DbSet in the context
			_context.Genres.Remove(genre);
			_context.SaveChanges();

			return NoContent(); // Return a 204 No Content response, as there is no need to send the deleted genre back to the client
		}

	}
}
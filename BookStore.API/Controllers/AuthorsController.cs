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
	public class AuthorsController : ControllerBase
	{
		private readonly BookstoreDbContext _context;
		private readonly IMapper _mapper; // Remove this line if you're not using AutoMapper

		public AuthorsController(BookstoreDbContext context, IMapper mapper) // Remove IMapper from constructor if not using AutoMapper
		{
			_context = context;
			_mapper = mapper; // Remove this line if you're not using AutoMapper
		}

		[HttpGet]
		public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
		{
			var authors = _context.Authors.ToList();

			// If you're not using AutoMapper, remove the following line
			// and change the return type of this method to IEnumerable<Author>
			var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);

			return Ok(authorsDto); // Replace authorsDto with authors if not using AutoMapper
		}

		[HttpGet("{id}")]
		public ActionResult<AuthorDto> GetAuthorById(int id)
		{
			var author = _context.Authors.FirstOrDefault(a => a.Id == id);

			if (author == null)
			{
				return NotFound();
			}

			// If you're not using AutoMapper, remove the following line
			// and change the return type of this method to Author
			var authorDto = _mapper.Map<AuthorDto>(author);

			return Ok(authorDto); // Replace authorDto with author if not using AutoMapper
		}

		[HttpPost]
		public ActionResult<AuthorDto> CreateAuthor(CreateAuthorDto createAuthorDto)
		{
			// Map the input DTO to the Author model (skip this line if not using DTOs and AutoMapper)
			var author = _mapper.Map<Author>(createAuthorDto);

			// Add the new author to the Authors DbSet in the context
			_context.Authors.Add(author);
			_context.SaveChanges();

			// Map the author model to the output DTO for the response (skip this line if not using DTOs and AutoMapper)
			var authorDto = _mapper.Map<AuthorDto>(author);

			// Return the created author along with its URI
			return CreatedAtAction(nameof(GetAuthorById), new { id = authorDto.Id }, authorDto);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto)
		{
			// Retrieve the existing author from the database
			var author = _context.Authors.Find(id);

			// If the author is not found, return a 404 Not Found response
			if (author == null)
			{
				return NotFound();
			}

			// Update the author's properties from the input DTO (skip this line if not using DTOs and AutoMapper)
			_mapper.Map(updateAuthorDto, author);

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Conflict("The author was already updated by another request.");
			}

			return NoContent(); // Return a 204 No Content response as there is no need to send the updated author back to the client
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteAuthor(int id)
		{
			var author = _context.Authors.Find(id);

			// If the author is not found, return a 404 Not Found response
			if (author == null)
			{
				return NotFound();
			}

			// Remove the author from the Authors DbSet in the context
			_context.Authors.Remove(author);
			_context.SaveChanges();

			return NoContent(); // Return a 204 No Content response, as there is no need to send the deleted author back to the client
		}
	}
}
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
	public class BooksController : ControllerBase
	{
		private readonly BookstoreDbContext _context;
		private readonly IMapper _mapper; // Remove this line if you're not using AutoMapper

		public BooksController(BookstoreDbContext context, IMapper mapper) // Remove IMapper from constructor if not using AutoMapper
		{
			_context = context;
			_mapper = mapper; // Remove this line if you're not using AutoMapper
		}

		[HttpGet]
		public ActionResult<IEnumerable<BookDto>> GetBooks()
		{
			var books = _context.Books.Include(b => b.Author).Include(b => b.Genre).ToList();

			// If you're not using AutoMapper, remove the following line
			// and change the return type of this method to IEnumerable<Book>
			var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

			return Ok(booksDto); // Replace booksDto with books if not using AutoMapper
		}

		[HttpGet("{id}")]
		public ActionResult<BookDto> GetBookById(int id)
		{
			// Retrieve the book including its Author and Genre
			var book = _context.Books
				.Include(b => b.Author)
				.Include(b => b.Genre)
				.FirstOrDefault(b => b.Id == id);

			if (book == null)
			{
				return NotFound();
			}

			// If you're not using AutoMapper, remove the following line
			// and change the return type of this method to Book
			var bookDto = _mapper.Map<BookDto>(book);

			return Ok(bookDto); // Replace bookDto with book if not using AutoMapper
		}

		[HttpGet("search")]
		public ActionResult<IEnumerable<BookDto>> SearchBooks(string title, string author, string genre)
		{
			// Fetch the books from the database, including their authors and genres
			IQueryable<Book> query = _context.Books.Include(b => b.Author).Include(b => b.Genre);

			// Apply filters based on the search terms and make the search case-insensitive
			if (!string.IsNullOrWhiteSpace(title))
			{
				title = title.ToLowerInvariant();
				query = query.Where(b => b.Title.ToLowerInvariant().Contains(title));
			}

			if (!string.IsNullOrWhiteSpace(author))
			{
				author = author.ToLowerInvariant();
				query = query.Where(b => b.Author.FirstName.ToLowerInvariant().Contains(author)
									|| b.Author.LastName.ToLowerInvariant().Contains(author));
			}

			if (!string.IsNullOrWhiteSpace(genre))
			{
				genre = genre.ToLowerInvariant();
				query = query.Where(b => b.Genre.Name.ToLowerInvariant().Contains(genre));
			}

			// Fetch the filtered list of books
			var books = query.ToList();

			// If you're not using AutoMapper, remove the following line and change the return type of this method to IEnumerable<Book>
			var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

			return Ok(booksDto); // Replace booksDto with books if not using AutoMapper
		}

		[HttpPost]
		public ActionResult<BookDto> CreateBook(CreateBookDto createBookDto)
		{
			// Map the input DTO to the Book model (skip this line if not using DTOs and AutoMapper)
			var book = _mapper.Map<Book>(createBookDto);

			// Add the new book to the Books DbSet in the context
			_context.Books.Add(book);
			_context.SaveChanges();

			// Map the book model to the output DTO for the response (skip this line if not using DTOs and AutoMapper)
			var bookDto = _mapper.Map<BookDto>(book);

			// Return the created book along with its URI
			return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateBook(int id, UpdateBookDto updateBookDto)
		{
			// Retrieve the existing book from the database
			var book = _context.Books.Find(id);

			// If the book is not found, return a 404 Not Found response
			if (book == null)
			{
				return NotFound();
			}

			// Update the book's properties from the input DTO (skip this line if not using DTOs and AutoMapper)
			_mapper.Map(updateBookDto, book);

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Conflict("The book was already updated by another request.");
			}

			return NoContent(); // Return a 204 No Content response as there is no need to send the updated book back to the client
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBook(int id)
		{
			var book = _context.Books.Find(id);

			// If the book is not found, return a 404 Not Found response
			if (book == null)
			{
				return NotFound();
			}

			// Remove the book from the Books DbSet in the context
			_context.Books.Remove(book);
			_context.SaveChanges();

			return NoContent(); // Return a 204 No Content response, as there is no need to send the deleted book back to the client
		}
	}
}
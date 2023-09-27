using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.API.Models
{
	public class Book
	{
		[Key] // Indicates that the property is a primary key
		public int Id { get; set; }

		[Required] // Indicates that the property is required
		[MaxLength(255)] // Sets the maximum length of the string property
		public string Title { get; set; }

		[ForeignKey("Author")] // Sets up a foreign key relationship with the Author model
		public int AuthorId { get; set; }
		public virtual Author Author { get; set; } // Navigation property for the related Author

		[ForeignKey("Genre")] // Sets up a foreign key relationship with the Genre model
		public int GenreId { get; set; }
		public virtual Genre Genre { get; set; } // Navigation property for the related Genre

		[Required]
		[Range(0, double.MaxValue)] // Sets a range constraint for the decimal property
		public decimal Price { get; set; }

		[Required]
		[Range(0, int.MaxValue)] // Sets a range constraint for the integer property
		public int QuantityAvailable { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
	public class Genre
	{
		[Key] // Indicates that the property is a primary key
		public int Id { get; set; }

		[Required] // Indicates that the property is required
		[MaxLength(255)] // Sets the maximum length of the string property
		public string Name { get; set; }

		// A one-to-many relationship, as a genre can have multiple books
		public virtual ICollection<Book> Books { get; set; }
	}
}
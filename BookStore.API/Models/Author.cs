using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
	public class Author
	{
		[Key] // Indicates that the property is a primary key
		public int Id { get; set; }

		[Required] // Indicates that the property is required
		[MaxLength(255)] // Sets the maximum length of the string property
		public string FirstName { get; set; }

		[Required] // Indicates that the property is required
		[MaxLength(255)] // Sets the maximum length of the string property
		public string LastName { get; set; }

		// A one-to-many relationship, as an author can have multiple books
		public virtual ICollection<Book> Books { get; set; }
	}
}
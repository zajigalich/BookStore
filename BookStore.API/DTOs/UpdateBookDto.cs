using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
	public class UpdateBookDto
	{
		[Required]
		[MaxLength(255)]
		public string Title { get; set; }

		[Required]
		public int AuthorId { get; set; }

		[Required]
		public int GenreId { get; set; }

		[Required]
		public int PublicationYear { get; set; }
	}
}

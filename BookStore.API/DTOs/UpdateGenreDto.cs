using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
	public class UpdateGenreDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
	}
}

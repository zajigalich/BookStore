using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
	public class CreateGenreDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
	}
}

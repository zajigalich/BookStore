using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
	public class UpdateAuthorDto
	{
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }
	}
}

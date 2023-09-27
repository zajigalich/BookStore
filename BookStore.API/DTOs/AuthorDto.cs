namespace BookStore.API.DTOs
{
	public class AuthorDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		// Optional: include a fully constructed name
		public string FullName => $"{FirstName} {LastName}";
	}
}

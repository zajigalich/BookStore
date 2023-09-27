namespace BookStore.API.DTOs
{
	public class BookDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string AuthorName { get; set; }
		public string GenreName { get; set; }
		public int PublicationYear { get; set; }
	}
}

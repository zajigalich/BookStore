using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
	public class BookstoreDbContext : DbContext
	{
		public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
		{
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Genre> Genres { get; set; }

		// Optionally, you can also override the OnModelCreating method to further configure your models
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Example: Adding a unique constraint on the Genre Name
			modelBuilder.Entity<Genre>()
				.HasIndex(g => g.Name)
				.IsUnique();
		}
	}
}
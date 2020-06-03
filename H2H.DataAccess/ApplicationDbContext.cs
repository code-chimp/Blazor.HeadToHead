using H2H.DataAccess.Config;
using H2H.Models;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        // Views
        public DbSet<BookDetailsFromView> BookDetailsFromViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new PublisherConfig());

            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookDetailConfig());
            modelBuilder.ApplyConfiguration(new BookAuthorConfig());

            // Views
            modelBuilder.Entity<BookDetailsFromView>()
                .HasNoKey()
                .ToView("GetOnlyBookDetails");
        }
    }
}

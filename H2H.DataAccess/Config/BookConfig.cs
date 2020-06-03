using H2H.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace H2H.DataAccess.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book
                {
                    Id = 1,
                    Title = "Book #1",
                    ISBN = "999-88-3456-000",
                    Price = 22.15,
                    BookDetailId = 1,
                    PublisherId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "Book #2",
                    ISBN = "999-88-3456-018",
                    Price = 18.33,
                    PublisherId = 2
                },
                new Book
                {
                    Id = 3,
                    Title = "Book #3",
                    ISBN = "999-88-3456-120",
                    Price = 14.12,
                    PublisherId = 3
                }
            );
        }
    }
}

using H2H.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace H2H.DataAccess.Config
{
    public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder
                .HasKey(_ => new {_.BookId, _.AuthorId});

            builder
                .HasOne(_ => _.Book)
                .WithMany(_ => _.BookAuthors)
                .HasForeignKey(_ => _.BookId);

            builder
                .HasOne(_ => _.Author)
                .WithMany(_ => _.BookAuthors)
                .HasForeignKey(_ => _.AuthorId);

            builder.HasData(
                new BookAuthor
                {
                    AuthorId = 3,
                    BookId = 1
                },
                new BookAuthor
                {
                    AuthorId = 1,
                    BookId = 3
                },
                new BookAuthor
                {
                    AuthorId = 2,
                    BookId = 3
                }
            );
        }
    }
}

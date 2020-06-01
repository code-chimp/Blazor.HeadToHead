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
        }
    }
}
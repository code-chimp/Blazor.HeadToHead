using H2H.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace H2H.DataAccess.Config
{
    public class BookDetailConfig : IEntityTypeConfiguration<BookDetail
    >
    {
        public void Configure(EntityTypeBuilder<BookDetail> builder)
        {
            builder.HasData(
                new BookDetail
                {
                    Id = 1,
                    NumberOfChapters = 12,
                    NumberOfPages = 218,
                    Weight = 0.73f,
                    Description = "Lovely tome about the thing you wanted to read"
                }
            );
        }
    }
}

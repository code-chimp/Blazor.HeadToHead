using H2H.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace H2H.DataAccess.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Science Fiction"
                },
                new Category
                {
                    Id = 2,
                    Name = "Fantasy"
                },
                new Category
                {
                    Id = 3,
                    Name = "Young Adult (YA)"
                }
            );
        }
    }
}

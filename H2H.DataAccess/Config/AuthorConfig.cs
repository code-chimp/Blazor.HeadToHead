using H2H.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace H2H.DataAccess.Config
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "J.R.R.",
                    LastName = "Tolkien",
                    Location = "London, GB"
                },
                new Author
                {
                    Id = 2,
                    FirstName = "George R.R.",
                    LastName = "Martin",
                    Location = "Not Writing"
                },
                new Author
                {
                    Id = 3,
                    FirstName = "Stephen",
                    LastName = "King",
                    Location = "Derry, ME"
                }
            );
        }
    }
}

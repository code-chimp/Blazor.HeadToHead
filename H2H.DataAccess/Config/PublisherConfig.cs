using H2H.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace H2H.DataAccess.Config
{
    public class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasData(
                new Publisher
                {
                    Id = 1,
                    Name = "Wackt"
                },
                new Publisher
                {
                    Id = 2,
                    Name = "Cyman and Feister"
                },
                new Publisher
                {
                    Id = 3,
                    Name = "O'Really"
                }
            );
        }
    }
}

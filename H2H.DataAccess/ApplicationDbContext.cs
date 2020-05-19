using H2H.Models;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
    }
}

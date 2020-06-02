using System.Linq;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Author entity)
        {
            var author = _context
                .Authors
                .FirstOrDefault(_ => _.Id == entity.Id);

            if (author != null)
            {
                author.FirstName = entity.FirstName;
                author.LastName = entity.LastName;
                author.Location = entity.Location;

                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Author entity)
        {
            var author = await _context
                .Authors
                .FirstOrDefaultAsync(_ => _.Id == entity.Id);

            if (author != null)
            {
                author.FirstName = entity.FirstName;
                author.LastName = entity.LastName;
                author.Location = entity.Location;

                await _context.SaveChangesAsync();
            }
        }
    }
}

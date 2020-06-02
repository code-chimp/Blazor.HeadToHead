using System.Linq;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess.Repository
{
    public class BookDetailRepository : Repository<BookDetail>, IBookDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public BookDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(BookDetail entity)
        {
            var bookDetail = _context
                .BookDetails
                .FirstOrDefault(_ => _.Id == entity.Id);

            if (bookDetail != null)
            {
                bookDetail.Description = entity.Description;

                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(BookDetail entity)
        {
            var bookDetail = await _context
                .BookDetails
                .FirstOrDefaultAsync(_ => _.Id == entity.Id);

            if (bookDetail != null)
            {
                bookDetail.Description = entity.Description;

                await _context.SaveChangesAsync();
            }
        }
    }
}

using H2H.DataAccess.Repository.Contracts;
using H2H.Models;

namespace H2H.DataAccess.Repository
{
    public class BookAuthorRepository : Repository<BookAuthor>, IBookAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public BookAuthorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.EntityFrameworkCore;

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

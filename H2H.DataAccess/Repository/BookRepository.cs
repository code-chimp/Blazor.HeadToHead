using System.Linq;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Book entity)
        {
            var book = _context
                .Books
                .FirstOrDefault(_ => _.Id == entity.Id);

            if (book != null)
            {
                book.Title = entity.Title;
                book.ISBN = entity.ISBN;
                book.Price = entity.Price;
                book.PublisherId = entity.PublisherId;
                book.BookDetailId = entity.BookDetailId;

                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Book entity)
        {
            var book = await _context
                .Books
                .FirstOrDefaultAsync(_ => _.Id == entity.Id);

            if (book != null)
            {
                book.Title = entity.Title;
                book.ISBN = entity.ISBN;
                book.Price = entity.Price;
                book.PublisherId = entity.PublisherId;
                book.BookDetailId = entity.BookDetailId;

                await _context.SaveChangesAsync();
            }
        }
    }
}

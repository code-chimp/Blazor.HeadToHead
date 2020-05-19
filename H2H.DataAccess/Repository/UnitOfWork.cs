using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;

namespace H2H.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBookRepository Books { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            
            Books = new BookRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

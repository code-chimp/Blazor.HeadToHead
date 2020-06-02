using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;

namespace H2H.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAuthorRepository Authors { get; }
        public IBookAuthorRepository BookAuthors { get; }
        public IBookDetailRepository BookDetails { get; }
        public IBookRepository Books { get; }
        public ICategoryRepository Categories { get; }
        public IPublisherRepository Publishers { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Authors = new AuthorRepository(_context);
            BookAuthors = new BookAuthorRepository(_context);
            BookDetails = new BookDetailRepository(_context);
            Books = new BookRepository(_context);
            Categories = new CategoryRepository(_context);
            Publishers = new PublisherRepository(_context);
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

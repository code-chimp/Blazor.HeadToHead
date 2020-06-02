using System.Linq;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext _context;

        public PublisherRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Publisher entity)
        {
            var publisher = _context
                .Publishers
                .FirstOrDefault(_ => _.Id == entity.Id);

            if (publisher != null)
            {
                publisher.Name = entity.Name;

                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Publisher entity)
        {
            var publisher = await _context
                .Publishers
                .FirstOrDefaultAsync(_ => _.Id == entity.Id);

            if (publisher != null)
            {
                publisher.Name = entity.Name;

                await _context.SaveChangesAsync();
            }
        }
    }
}

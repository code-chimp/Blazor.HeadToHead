using System.Linq;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category entity)
        {
            var category = _context
                .Categories
                .FirstOrDefault(_ => _.Id == entity.Id);

            if (category != null)
            {
                category.Name = entity.Name;

                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Category entity)
        {
            var category = await _context
                .Categories
                .FirstOrDefaultAsync(_ => _.Id == entity.Id);

            if (category != null)
            {
                category.Name = entity.Name;

                await _context.SaveChangesAsync();
            }
        }
    }
}

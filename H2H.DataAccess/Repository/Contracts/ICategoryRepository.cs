using System.Threading.Tasks;
using H2H.Models;

namespace H2H.DataAccess.Repository.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category entity);

        Task UpdateAsync(Category entity);
    }
}

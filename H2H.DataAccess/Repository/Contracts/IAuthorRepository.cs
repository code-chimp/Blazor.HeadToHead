using System.Threading.Tasks;
using H2H.Models;

namespace H2H.DataAccess.Repository.Contracts
{
    public interface IAuthorRepository : IRepository<Author>
    {
        void Update(Author entity);

        Task UpdateAsync(Author entity);
    }
}

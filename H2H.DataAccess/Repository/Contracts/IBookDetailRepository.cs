using System.Threading.Tasks;
using H2H.Models;

namespace H2H.DataAccess.Repository.Contracts
{
    public interface IBookDetailRepository : IRepository<BookDetail>
    {
        void Update(BookDetail entity);

        Task UpdateAsync(BookDetail entity);
    }
}

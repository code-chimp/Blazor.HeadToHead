using System.Threading.Tasks;
using H2H.Models;

namespace H2H.DataAccess.Repository.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book book);

        Task UpdateAsync(Book book);
    }
}

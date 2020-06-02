using System.Threading.Tasks;
using H2H.Models;

namespace H2H.DataAccess.Repository.Contracts
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        void Update(Publisher entity);

        Task UpdateAsync(Publisher entity);
    }
}

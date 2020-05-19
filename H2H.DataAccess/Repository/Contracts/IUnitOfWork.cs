using System;
using System.Threading.Tasks;

namespace H2H.DataAccess.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        
        void Save();

        Task SaveAsync();
    }
}

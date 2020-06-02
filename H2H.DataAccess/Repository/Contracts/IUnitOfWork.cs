using System;
using System.Threading.Tasks;

namespace H2H.DataAccess.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IBookAuthorRepository BookAuthors { get; }
        IBookDetailRepository BookDetails { get; }
        ICategoryRepository Categories { get; }
        IPublisherRepository Publishers { get; }

        void Save();

        Task SaveAsync();
    }
}

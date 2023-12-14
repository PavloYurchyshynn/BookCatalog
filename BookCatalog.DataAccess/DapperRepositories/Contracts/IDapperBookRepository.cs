using BookCatalog.Core.Entities;

namespace BookCatalog.DataAccess.DapperRepositories.Contracts
{
    public interface IDapperBookRepository
    {
        Task<IEnumerable<Book>> GetByName(string name);
    }
}

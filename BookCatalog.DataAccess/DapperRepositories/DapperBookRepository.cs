using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.DapperRepositories.Contracts;
using BookCatalog.DataAccess.Persistence;
using Dapper;
using System.Data;

namespace BookCatalog.DataAccess.DapperRepositories
{
    public class DapperBookRepository : IDapperBookRepository
    {
        private readonly DapperBookCatalogContext _context;
        private string _query = "SELECT *" +
            "FROM dbo.Books" +
            "   WHERE [Name] LIKE '%' + @name + '%'";

        public DapperBookRepository(DapperBookCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetByNameAsync(string name)
        {
            var parameters = new DynamicParameters();
            parameters.Add("name", name, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var books = await connection.QueryAsync<Book>(_query, parameters);

                return books.ToList();
            }
        }
    }
}

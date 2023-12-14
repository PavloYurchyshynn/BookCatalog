using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BookCatalog.DataAccess.Persistence
{
    public class DapperBookCatalogContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DapperBookCatalogContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}

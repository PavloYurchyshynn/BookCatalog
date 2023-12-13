using BookCatalog.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.DependencyInjection
{
    public static class DIConfig
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BookCatalogContext>(options => options.UseSqlServer(connection));
        }
    }
}

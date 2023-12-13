using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories;
using BookCatalog.DataAccess.Repositories.Contracts;
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

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
        }
    }
}

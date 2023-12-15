using BookCatalog.Application.MappingProfiles;
using BookCatalog.Application.Services;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories;
using BookCatalog.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.DependencyInjection
{
    public static class DIConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICommentService, CommentService>();
        }

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

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BookProfile).Assembly);
            services.AddAutoMapper(typeof(CommentProfile).Assembly);
        }
    }
}

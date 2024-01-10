using BookCatalog.Application.MappingProfiles;
using BookCatalog.Application.Services;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.DataAccess.DapperRepositories;
using BookCatalog.DataAccess.DapperRepositories.Contracts;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories;
using BookCatalog.DataAccess.Repositories.Contracts;
using BookCatalog.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookCatalog.API.DependencyInjection
{
    public static class DIConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BookCatalogContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<DapperBookCatalogContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddScoped<IDapperBookRepository, DapperBookRepository>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BookProfile).Assembly);
            services.AddAutoMapper(typeof(CommentProfile).Assembly);
            services.AddAutoMapper(typeof(BasketProfile).Assembly);
            services.AddAutoMapper(typeof(OrderProfile).Assembly);
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                };
            });
        }

        public static void AddAuthSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }
    }
}

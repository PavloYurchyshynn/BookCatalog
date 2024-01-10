using BookCatalog.API.DependencyInjection;
using BookCatalog.Application.SyncDataServices.Http;
using BookCatalog.DataAccess.Persistence;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<IBookCatalogServiceClient, HttpBookCatalogServiceClient>();
builder.Services.AddAutoMapper();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BookCatalogContext>()
    .AddDefaultTokenProviders();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthSwagger();
builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

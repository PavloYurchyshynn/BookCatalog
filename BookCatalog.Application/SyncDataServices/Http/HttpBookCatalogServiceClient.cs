using BookCatalog.Application.Models.Book;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace BookCatalog.Application.SyncDataServices.Http
{
    public class HttpBookCatalogServiceClient : IBookCatalogServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpBookCatalogServiceClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendBookToBookService(BookModel model)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_configuration["BookCatalogService"]}/api/c/BookCatalog", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post to Service is success");
            }
            else
            {
                Console.WriteLine("--> Sync Post to Service is not success");
            }
        }
    }
}

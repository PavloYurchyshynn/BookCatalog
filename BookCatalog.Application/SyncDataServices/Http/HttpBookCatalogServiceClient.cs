using BookCatalog.Application.Models.Book;
using BookCatalog.Shared.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BookCatalog.Application.SyncDataServices.Http
{
    public class HttpBookCatalogServiceClient : IBookCatalogServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _jwtTokenService;

        public HttpBookCatalogServiceClient(HttpClient httpClient, IConfiguration configuration, IJwtTokenService jwtTokenService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }

        public async Task SendBookToBookService(BookModel model)
        {
            var token = _configuration["JWT:Token"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;

namespace WEB_253503_MINICH.UI.Services.ApiCupService
{
    public class ApiCupService : IApiCupService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiCupService> _logger;
        private readonly string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiCupService(HttpClient httpClient,
                             IConfiguration configuration,
                             ILogger<ApiCupService> logger)
        {
            _pageSize = configuration.GetSection("ItemsPerPage").Value!;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<ResponseData<int>> CreateCupAsync(Cup product, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress!.AbsoluteUri + "Cups");
            var response = await _httpClient.PostAsJsonAsync(uri,
                                                            product,
                                                            _serializerOptions);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                        .Content
                        .ReadFromJsonAsync<ResponseData<int>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<int>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Объект не создан. Error: {response.StatusCode.ToString()}");
            return ResponseData<int>
                .Error($"Объект не добавлен. Error: {response.StatusCode.ToString()}");
        }

        public Task DeleteCupAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Cup>> GetCupByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ProductListModel<Cup>>> GetCupListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // подготовка URL запроса
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Cups");

            // добавить категорию в маршрут 
            if (categoryNormalizedName != null)
            {
                urlString.Append($"{categoryNormalizedName}/");
            }

            // добавить номер страницы в маршрут 
            if (pageNo > 1)
            {
                urlString.Append($"page{pageNo}");
            };

            // добавить размер страницы в строку запроса 
            if (!_pageSize.Equals("3"))
            {
                urlString.Append(QueryString.Create("pageSize", _pageSize));
            }

            // отправить запрос к API 
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                        .Content
                        .ReadFromJsonAsync<ResponseData<ProductListModel<Cup>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");

                    return ResponseData<ProductListModel<Cup>>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
            return ResponseData<ProductListModel<Cup>>
                .Error($"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        }

        public Task UpdateCupAsync(int id, Cup product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.WebUtilities;
using System.Data.SqlTypes;
using System.Text;
using System.Text.Json;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.UI.Services.FileService;

namespace WEB_253503_MINICH.UI.Services.ApiCupService
{
    public class ApiCupService : ICupService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ApiCupService> _logger;
        private readonly string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IFileService _fileService;

        public ApiCupService(IHttpClientFactory httpClientFactory,
                             IConfiguration configuration,
                             ILogger<ApiCupService> logger,
                             IFileService fileService)
        {
            _pageSize = configuration.GetSection("ItemsPerPage").Value!;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiClient");
            _fileService = fileService;
        }

        public async Task<ResponseData<int>> CreateCupAsync(Cup product, IFormFile? formFile)
        {
            // Первоначально использовать картинку по умолчанию 
            product.ImgPath = "Images/cup1.jpg";
            // Сохранить файл изображения 
            if (formFile != null)
            {
                var imageUrl = await _fileService.SaveFileAsync(formFile);
                // Добавить в объект Url изображения 
                if (!string.IsNullOrEmpty(imageUrl))
                    product.ImgPath = imageUrl;
            }

            var url = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}cups/");
            var response = await _httpClient.PostAsJsonAsync(new Uri(url.ToString()),
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

        public async Task<ResponseData<bool>> DeleteCupAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}cups/");
            urlString.Append($"{id}");

            var response = await _httpClient.DeleteAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<bool>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    return ResponseData<bool>.Error($"Error: {ex.Message}");
                }
            }
            return ResponseData<bool>.Error($"Error: {response.StatusCode.ToString()}");
        }

        public async Task<ResponseData<Cup>> GetCupByIdAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}cups/");
            urlString.Append($"{id}");

            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<Cup>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    return ResponseData<Cup>.Error($"Error: {ex.Message}");
                }
            }
            return ResponseData<Cup>.Error($"Error: {response.StatusCode.ToString()}");

        }

        public async Task<ResponseData<ProductListModel<Cup>>> GetCupListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // подготовка URL запроса
            var urlString = new StringBuilder($"{_httpClient.BaseAddress}Сups");

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

        public async Task<ResponseData<bool>> UpdateCupAsync(int id, Cup product, IFormFile? formFile)
        {
            // Первоначально использовать картинку по умолчанию 
            product.ImgPath = "Images/cup1.jpg";
            // Сохранить файл изображения 
            if (formFile != null)
            {
                var imageUrl = await _fileService.SaveFileAsync(formFile);
                // Добавить в объект Url изображения 
                if (!string.IsNullOrEmpty(imageUrl))
                    product.ImgPath = imageUrl;
            }
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}cups/");
            urlString.Append($"{id}");

            var response = await _httpClient.PutAsJsonAsync(new Uri(urlString.ToString()), product, _serializerOptions);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<bool>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                    return ResponseData<bool>.Error($"Error: {ex.Message}");
                }
            }
            _logger.LogError($"Object not updated. Error: {response.StatusCode.ToString()}");
            return ResponseData<bool>
                .Error($"Object not updated. Error: {response.StatusCode.ToString()}");

        }
    }
}

using System.Text;
using System.Text.Json;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
/*using WEB_253503_MINICH.UI.Services.FileService;*/

namespace WEB_253503_MINICH.UI.Services.ApiCupService
{
    public class ApiCupService : ICupService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiCupService> _logger;
        private readonly string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        /*private readonly IFileService _fileService;*/

        public ApiCupService(HttpClient httpClient, 
                                IConfiguration configuration, 
                                ILogger<ApiCupService> logger, 
                                IHttpClientFactory httpClientFactory)
        {
            _pageSize = configuration.GetSection("ItemsPerPage").Value!;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            _httpClient = _httpClientFactory.CreateClient("MyApiClient"); ;
            /*_fileService = fileService;*/
        }

        public async Task<ResponseData<Cup>> CreateCupAsync(Cup product, IFormFile? formFile)
        {
            /*// Первоначально использовать картинку по умолчанию 
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
                .Error($"Объект не добавлен. Error: {response.StatusCode.ToString()}");*/
            throw new NotImplementedException();
        }

        public async Task DeleteCupAsync(int id)
        {
            /*var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}cups/");
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
            return ResponseData<bool>.Error($"Error: {response.StatusCode.ToString()}");*/
            throw new NotImplementedException();
        }

        public async Task<ResponseData<Cup>> GetCupByIdAsync(int id)
        {
            /*var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}cups/");
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
            return ResponseData<Cup>.Error($"Error: {response.StatusCode.ToString()}");*/
            throw new NotImplementedException();

        }

        public async Task<ResponseData<ProductListModel<Cup>>> GetCupListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            await Console.Out.WriteLineAsync(_httpClient.BaseAddress.ToString());
            // подготовка URL запроса
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}Cups/");

            urlString.Append($"{categoryNormalizedName}?");
            urlString.Append($"pageNo={pageNo}");
            urlString.Append($"&pageSize={_pageSize}");

            await Console.Out.WriteLineAsync(urlString.ToString());

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

        public async Task UpdateCupAsync(int id, Cup product, IFormFile? formFile)
        {
            /*// Первоначально использовать картинку по умолчанию 
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
                .Error($"Object not updated. Error: {response.StatusCode.ToString()}");*/
            throw new NotImplementedException();

        }
    }
}

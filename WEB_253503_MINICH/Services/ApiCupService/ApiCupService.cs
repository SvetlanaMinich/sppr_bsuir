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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiCupService> _logger;
        private readonly string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiCupService(IConfiguration configuration,
            ILogger<ApiCupService> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _pageSize = _configuration.GetSection("ItemsPerPage").Value!;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<ResponseData<int>> CreateCupAsync(Cup product, IFormFile? formFile)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Cups/");
            var response = await _httpClient.PostAsJsonAsync(new Uri(urlString.ToString()), product, _serializerOptions);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<int>>(_serializerOptions);
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
            //preparing url request
            var urlString = new StringBuilder($"{_httpClient.BaseAddress}Cups");

            var queryParams = new Dictionary<string, string>();

            //add category in urlstring
            if (categoryNormalizedName != null)
            {
                queryParams.Add("category", categoryNormalizedName);
            }

            queryParams.Add("pageNo", pageNo.ToString());

            //add page size in urlstring
            if (!_pageSize.Equals("3"))
            {
                queryParams.Add("pageSize", _pageSize);
            }

            var fullUrl = QueryHelpers.AddQueryString(urlString.ToString(), queryParams);

            //send request to api
            var response = await _httpClient.GetAsync(fullUrl);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Cup>>>(_serializerOptions);
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

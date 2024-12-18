﻿using System.Text;
using WEB_253503_MINICH.UI.Services.Authentication;

namespace WEB_253503_MINICH.UI.Services.FileService
{
    public class ApiFileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ITokenAccessor _tokenAccessor;

        public ApiFileService(IHttpClientFactory httpClientFactory, ITokenAccessor tokenAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("MyApiClient");
            _tokenAccessor = tokenAccessor;
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Files/");
            urlString.Append($"{fileName}");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(urlString.ToString())
            };

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {
            // Создать объект запроса 
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };

            // Сформировать случайное имя файла, сохранив расширение 
            var extension = Path.GetExtension(formFile.FileName);
            var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

            // Создать контент, содержащий поток загруженного файла 
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(formFile.OpenReadStream());
            content.Add(streamContent, "file", newName);

            // Поместить контент в запрос 
            request.Content = content;

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            // Отправить запрос к API 
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // Вернуть полученный Url сохраненного файла 
                return await response.Content.ReadAsStringAsync();
            }
            return String.Empty;
        }
    }
}

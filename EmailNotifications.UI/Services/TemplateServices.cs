using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.Response;
using EmailNotifications.UI.Services;
using System.Net.Http.Json;

namespace Services
{
    public class TemplatesService : ITemplateService
    {
        private readonly HttpClient _httpClient;

        public TemplatesService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<List<TemplateDto>> GetAllTemplatesAsync()
        {
            var result = await _httpClient.GetAsync("api/templates");
            if (!result.IsSuccessStatusCode)
            {
                return new List<TemplateDto>();
            }

            var templates = await result.Content.ReadFromJsonAsync<List<TemplateDto>>();
            return templates ?? new List<TemplateDto>();
        }

        public async Task<TemplateDto?> GetTemplateAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/templates/{id}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<TemplateDto>();
        }

        public async Task<TemplateDto?> SaveTemplateAsync(TemplateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/templates", request);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<TemplateDto>();
        }
    }
}

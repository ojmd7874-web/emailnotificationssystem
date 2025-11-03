using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.Response;
using EmailNotifications.UI.Services;
using System.Net.Http.Json;

namespace Services
{
    public class TemplatesService : ITemplateService
    {
        private readonly HttpClient _http;

        public TemplatesService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TemplateDto>> GetAllTemplatesAsync()
        {
            var resp = await _http.GetAsync("api/templates");
            if (!resp.IsSuccessStatusCode) return new List<TemplateDto>();
            var list = await resp.Content.ReadFromJsonAsync<List<TemplateDto>>();
            return list ?? new List<TemplateDto>();
        }

        public async Task<TemplateDto?> GetTemplateAsync(int id)
        {
            var resp = await _http.GetAsync($"api/templates/{id}");
            if (!resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<TemplateDto>();
        }

        public async Task<TemplateDto?> SaveTemplateAsync(TemplateRequest request)
        {
            var resp = await _http.PostAsJsonAsync("api/templates", request);
            if (!resp.IsSuccessStatusCode)
            {
                // try to parse message
                try
                {
                    return await resp.Content.ReadFromJsonAsync<TemplateDto>();
                }
                catch
                {
                    return null;
                }
            }

            return await resp.Content.ReadFromJsonAsync<TemplateDto>();
        }
    }
}

using EmailNotifications.Application.Dtos;
using EmailNotifications.Application.Interfaces;
using EmailNotifications.SharedLibrary.Response;
using System.Net.Http.Json;

namespace Services
{
    public class ClientService : EmailNotifications.UI.Services.IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<Response> SaveClientConfigAsync(ClientConfigDto model)
        {
            var result = await _httpClient.PostAsJsonAsync("api/clients/config", model);
            return await result.Content.ReadFromJsonAsync<Response>();
        }

        public async Task<ClientDto?> GetClientConfigAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/clients/{id}");

            var dto = await result.Content.ReadFromJsonAsync<ClientDto>();
            if (dto == null) return null;

            return new ClientDto(
                dto.Id,
                dto.Name,
                dto.Email
            );
        }

        // New: fetch all clients (with full info) once on page load.
        // The API should expose GET /api/clients returning the full client objects.
        public async Task<List<ClientConfigDto>> GetAllClientsAsync()
        {
            var resp = await _httpClient.GetAsync("api/clients");
            if (!resp.IsSuccessStatusCode)
            {
                return new List<ClientConfigDto>();
            }

            var list = await resp.Content.ReadFromJsonAsync<List<ClientConfigDto>>();
            return list ?? new List<ClientConfigDto>();
        }
    }
}
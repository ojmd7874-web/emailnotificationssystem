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
            if (!result.IsSuccessStatusCode)
            {
                return new Response { Success = false };
            }

            return await result.Content.ReadFromJsonAsync<Response>();
        }

        public async Task<ClientDto?> GetClientConfigAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/clients/{id}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var client = await result.Content.ReadFromJsonAsync<ClientDto>();
            if (client == null)
            {
                return null;
            }

            return client;
        }

        public async Task<List<ClientConfigDto>> GetAllClientsAsync()
        {
            var result = await _httpClient.GetAsync("api/clients");
            if (!result.IsSuccessStatusCode)
            {
                return new List<ClientConfigDto>();
            }

            var clients = await result.Content.ReadFromJsonAsync<List<ClientConfigDto>>();
            return clients ?? new List<ClientConfigDto>();
        }
    }
}
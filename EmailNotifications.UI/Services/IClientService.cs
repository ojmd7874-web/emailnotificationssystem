using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.Response;

namespace EmailNotifications.UI.Services
{
    public interface IClientService
    {
        Task<Response> SaveClientConfigAsync(ClientConfigDto model);
        Task<List<ClientConfigDto>> GetAllClientsAsync();
    }
}

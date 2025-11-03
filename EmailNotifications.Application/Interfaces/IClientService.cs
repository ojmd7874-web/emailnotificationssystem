using EmailNotifications.Application.Dtos;
using EmailNotifications.Domain.Entities;
using EmailNotifications.SharedLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto?> CreateClientAsync(ClientRequest request);
        Task<List<ClientDto>> GetAllClientsAsync();
        Task<ClientDto?> GetClientAsync(int id);
        Task<Response> SaveClientConfigAsync(ClientConfigDto request);
    }
}

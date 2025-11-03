using EmailNotifications.Application.Dtos;
using EmailNotifications.Application.Interfaces;
using EmailNotifications.Domain.Entities;
using EmailNotifications.SharedLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientDto?> CreateClientAsync(ClientRequest request)
        {
            var client = await _clientRepository.GetByNameAsync(request.Name);
            if (client != null)
            {
                return null;
            }

            client = new Client
            {
                Name = request.Name,
                Email = request.Email
            };

            await _clientRepository.AddAsync(client);
            await _clientRepository.SaveChangesAsync();

            return MapEntityToDto(client);
        }

        public async Task<ClientDto?> GetClientAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return null;
            }
            return MapEntityToDto(client);
        }

        private static ClientDto MapEntityToDto(Client entity)
        {
            return new ClientDto(entity.Id, entity.Name, entity.Email);
        }

        public async Task<Response> SaveClientConfigAsync(ClientConfigDto request)
        {
            Client client = null;
            bool newClient = false;
            if (!request.Id.HasValue)
            {
                client = new Client
                {
                    Name = request.Name,
                    Email = request.Email
                };
                await _clientRepository.AddAsync(client); 
                newClient = true;
            }
            else
            {
                // Update existing client config
                client = await _clientRepository.GetByIdAsync(request.Id.Value);
                client.Name = request.Name;
                client.Email = request.Email;
            }

            await _clientRepository.SaveChangesAsync();
            return new Response
            {
                Success = true,
                Message = newClient ? "Client created successfully." : "Client updated successfully."
            };
        }

        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return clients.Select(MapEntityToDto).ToList();
        }
    }
}

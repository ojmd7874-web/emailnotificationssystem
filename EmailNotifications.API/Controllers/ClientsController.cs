using EmailNotifications.Application.Dtos;
using EmailNotifications.Application.Interfaces;
using EmailNotifications.Domain.Entities;
using EmailNotifications.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailNotifications.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientRequest request)
        {
            var client = await _clientService.CreateClientAsync(request);
            if (client == null)
            {
                return BadRequest($"Client: {request.Name} exist");
            }
            return Ok(client);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientService.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost("config")]
        public async Task<IActionResult> SaveClientConfig([FromBody] ClientConfigDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var result = await _clientService.SaveClientConfigAsync(request);
            if(result.Success == false)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}

using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.RabbitMQ;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailNotifications.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailNotificationsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EmailNotificationsController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailNotification([FromBody] EmailNotificationModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not valid");
            }

            await _publishEndpoint.Publish<IEmailNotification>(new
            {
                ClientId = request.ClientId,
                TemplateId = request.TemplateId,
                Recipients = request.Recipients,
                MarketingData = request.MarketingData
            });

            return Ok();
        }

    }
}
